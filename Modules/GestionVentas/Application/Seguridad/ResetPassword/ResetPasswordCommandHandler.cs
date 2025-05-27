using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Comunications.Application.Core.Mail;
using Ferreteria.Modules.GestionVentas.Application.Configuration.Command;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Seguridad;
using Ferreteria.Modules.GestionVentas.Domain.Repository;
using Ferreteria.Modules.GestionVentas.Domain.Servicios;
using Ferreteria.Modules.GestionVentas.Domain.Users;
using Microsoft.Azure.Amqp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Ferreteria.Modules.GestionVentas.Application.Seguridad.ResetPassword
{
    public class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand, RequestResult>
    {
        private readonly ISeguridadRepository _seguridadRepository;
        private readonly ICommonService _commonService;
        private readonly IUserContext _userContext;

        public ResetPasswordCommandHandler(ISeguridadRepository seguridadRepository, ICommonService commonService, IUserContext userContext)
        {
            _seguridadRepository = seguridadRepository;
            _commonService = commonService;
            _userContext = userContext;
        }

        public async Task<RequestResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var usuario = _userContext.UserId.UserName;

            if (!_commonService.IsValidEmail(request.Correo))
                return RequestResult.WithError("El correo electrónico no es válido.");

            if (!_commonService.IsValidPassword(request.NuevaContrasenia))
                return RequestResult.WithError("La contraseña debe tener al menos 8 caracteres, incluir un número, una letra y un carácter especial.");

            if (request.NuevaContrasenia != request.ConfirmarContrasenia)
                return RequestResult.WithError("Las contraseñas no coinciden.");

            var otpValidationResult = await ValidarOtp(request.Correo, request.OTP);
            if (!otpValidationResult.IsSuccess)
                return otpValidationResult;

            var usuarioObtenido = await _seguridadRepository.ObtenerUsuarioAsync(request.Correo);
            if (usuarioObtenido == null)
                return RequestResult.WithError("No existe un usuario con este correo electrónico.");

            usuarioObtenido.Contrasenia = _commonService.HashPassword(request.NuevaContrasenia);

            await _seguridadRepository.ActualizarContraseniaUsuario(usuarioObtenido.IdUsuario, usuarioObtenido.Contrasenia, usuario);
            await _seguridadRepository.ActualizarCodigoVerificacion(otpValidationResult.Data.IdUsuario, usuario);

            return new RequestResult();
        }

        public async Task<RequestResult<ObtenerCodigoVerificacionDTO>> ValidarOtp(string correo, string otp)
        {
            var otpResultado = await _seguridadRepository.ObtenerCodigoVerificacion(correo);

            if (otpResultado == null)
                return RequestResult.WithError<ObtenerCodigoVerificacionDTO>("El código de verificación es incorrecto o no existe.");

            if (otpResultado.Expiracion < DateTime.UtcNow)
                return RequestResult.WithError<ObtenerCodigoVerificacionDTO>("El código de verificación ha expirado.");

            if (otpResultado.Codigo != otp)
                return RequestResult.WithError<ObtenerCodigoVerificacionDTO>("El código de verificación es incorrecto.");

            return RequestResult.Success(otpResultado);
        }
    }
}
