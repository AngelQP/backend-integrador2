using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Configuration.Command;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Seguridad;
using Ferreteria.Modules.GestionVentas.Domain.Repository;
using Ferreteria.Modules.GestionVentas.Domain.Servicios;
using Ferreteria.Modules.GestionVentas.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Seguridad.UserUpdate
{
    public class UserUpdateCommandHandler : ICommandHandler<UserUpdateCommand, RequestResult>
    {
        private readonly ISeguridadRepository _seguridadRepository;
        private readonly ICommonService _commonService;
        private readonly IUserContext _userContext;

        public UserUpdateCommandHandler(ISeguridadRepository seguridadRepository, ICommonService commonService, IUserContext userContext)
        {
            _seguridadRepository = seguridadRepository;
            _commonService = commonService;
            _userContext = userContext;
        }

        public async Task<RequestResult> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            var usuario = _userContext.UserId.Value;

            if (!_commonService.IsValidEmail(request.Correo))
                return RequestResult.WithError("El correo electrónico no es válido.");

            var resultado = await _seguridadRepository.ValidarUsuarioOCorreo(null, request.Correo, request.IdUsuario);

            if (resultado == "Usuario")
                return RequestResult.WithError("El nombre de usuario ya está registrado.");
            else if (resultado == "Correo")
                return RequestResult.WithError("El correo electrónico ya está registrado.");

            string hashedPassword = null;

            if (request.ActualizarContrasenia)
            {
                if (!_commonService.IsValidPassword(request.Contrasenia))
                    return RequestResult.WithError("La contraseña debe tener al menos 8 caracteres, incluir un número, una letra y un carácter especial.");

                if (request.Contrasenia != request.ConfirmarContrasenia)
                    return RequestResult.WithError("Las contraseñas no coinciden.");

                hashedPassword = _commonService.HashPassword(request.Contrasenia);
            }

            var result = await _seguridadRepository.ActualizarUsuario(new ActualizarUsuarioRequest
            {
                IdUsuario = request.IdUsuario,
                Correo = request.Correo,
                Nombre = request.Nombre,
                ApellidoPaterno = request.ApellidoPaterno,
                ApellidoMaterno = request.ApellidoMaterno,
                Telefono = request.Telefono,
                ActualizarContrasenia = request.ActualizarContrasenia,
                Contrasenia = hashedPassword,
                Rol = request.Rol,
                Usuario = usuario
            });

            return new RequestResult();
        }
    }
}
