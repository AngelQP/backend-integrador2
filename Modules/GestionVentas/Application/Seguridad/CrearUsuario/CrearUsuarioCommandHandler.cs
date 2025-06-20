﻿using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Comunications.Application.Core.Mail;
using Ferreteria.Comunications.Application.Core.Mail.DTO;
using Ferreteria.Modules.GestionVentas.Application.Configuration.Command;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Seguridad;
using Ferreteria.Modules.GestionVentas.Domain.Enums;
using Ferreteria.Modules.GestionVentas.Domain.Plantillas;
using Ferreteria.Modules.GestionVentas.Domain.Repository;
using Ferreteria.Modules.GestionVentas.Domain.Servicios;
using Ferreteria.Modules.GestionVentas.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Seguridad.CrearUsuario
{
    public class CrearUsuarioCommandHandler : ICommandHandler<CrearUsuarioCommand, RequestResult>
    {
        private readonly ISeguridadRepository _seguridadRepository;
        private readonly ICommonService _commonService;
        private readonly IUserContext _userContext;
        private readonly IMailService _mailService;

        public CrearUsuarioCommandHandler(ISeguridadRepository seguridadRepository, ICommonService commonService, IUserContext userContext, IMailService mailService)
        {
            _seguridadRepository = seguridadRepository;
            _commonService = commonService;
            _userContext = userContext;
            _mailService = mailService;
        }

        public async Task<RequestResult> Handle(CrearUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = _userContext.UserId.Value;

            if (!_commonService.IsValidPassword(request.Contrasenia))
                return RequestResult.WithError("La contraseña debe tener al menos 8 caracteres, incluir un número, una letra y un carácter especial.");

            if (request.Contrasenia != request.ConfirmarContrasenia)
                return RequestResult.WithError("Las contraseñas no coinciden.");

            if (!_commonService.IsValidEmail(request.Correo))
                return RequestResult.WithError("El correo electrónico no es válido.");

            var resultado = await _seguridadRepository.ValidarUsuarioOCorreo(null, request.Correo);

            if (resultado == "Usuario")
                return RequestResult.WithError("El nombre de usuario ya está registrado.");
            else if (resultado == "Correo")
                return RequestResult.WithError("El correo electrónico ya está registrado.");

            string hashedPassword = _commonService.HashPassword(request.Contrasenia);

            var result = await _seguridadRepository.CrearUsuario(new CrearUsuarioRequest
            {
                Sociedad = request.Sociedad,
                Usuario = request.Correo,
                Correo = request.Correo,
                Nombre = request.Nombre,
                ApellidoPaterno = request.ApellidoPaterno,
                ApellidoMaterno = request.ApellidoMaterno,
                Telefono = request.Telefono,
                Contrasenia = hashedPassword,
                Rol = request.Rol,
                UsuarioCreacion = usuario
            });

            await EnviarCorreoBienvenida(request);

            return new RequestResult();
        }

        private async Task<RequestResult> EnviarCorreoBienvenida(CrearUsuarioCommand request)
        {
            string rolDescripcion = "Desconocido";
            string sociedadDescripcion = "Desconocido";

            if (Enum.TryParse<RolUsuario>(request.Rol, out var rolEnum))
                rolDescripcion = rolEnum.ToDescripcion();

            if (Enum.TryParse<Sociedad>(request.Sociedad, out var sociedadEnum))
                sociedadDescripcion = sociedadEnum.ToDescripcion();

            var html = PlantillasHtml.ObtenerCorreoRegistroUsuario()
                .Replace("{{NOMBRE}}", $"{request.Nombre}")
                .Replace("{{CORREO}}", request.Correo)
                .Replace("{{ROL}}", rolDescripcion)
                .Replace("{{SOCIEDAD}}", sociedadDescripcion)
                .Replace("{{FECHA_HORA}}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"))
                .Replace("{{ANIO}}", DateTime.Now.Year.ToString());

            var emailRequest = new EmailRequest
            {
                TO = new List<string> { request.Correo },
                Subject = "Ferretería - Registro de Usuario Exitoso",
                Body = html,
                IsBodyHtml = true
            };

            return await _mailService.Send(emailRequest);
        }
    }
}
