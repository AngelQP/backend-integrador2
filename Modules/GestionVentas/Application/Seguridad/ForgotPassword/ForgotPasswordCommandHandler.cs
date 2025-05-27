using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Comunications.Application.Core.Mail;
using Ferreteria.Comunications.Application.Core.Mail.DTO;
using Ferreteria.Comunications.Application.Core.Master.DTO;
using Ferreteria.Modules.GestionVentas.Application.Configuration.Command;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Seguridad;
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

namespace Ferreteria.Modules.GestionVentas.Application.Seguridad.ForgotPassword
{
    public class ForgotPasswordCommandHandler : ICommandHandler<ForgotPasswordCommand, RequestResult>
    {
        private readonly ISeguridadRepository _seguridadRepository;
        private readonly ICommonService _commonService;
        private readonly IMailService _mailService;
        private readonly IUserContext _userContext;

        public ForgotPasswordCommandHandler(ISeguridadRepository seguridadRepository, ICommonService commonService, IMailService mailService, IUserContext userContext)
        {
            _seguridadRepository = seguridadRepository;
            _commonService = commonService;
            _mailService = mailService;
            _userContext = userContext;
        }

        public async Task<RequestResult> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            if (!_commonService.IsValidEmail(request.Correo))
                return RequestResult.WithError("El correo electrónico no es válido.");

            var usuario = await _seguridadRepository.ObtenerUsuarioAsync(request.Correo);

            if (usuario == null)
                return RequestResult.WithError("Hubo un problema al enviar el código. Verifica tu correo e inténtalo nuevamente.");

            var otp = GenerateOtp();
            await _seguridadRepository.GuardarOTP(new GuardarOTPRequest { IdUsuario = usuario.IdUsuario, Correo = request.Correo, Codigo = otp, Expiracion = DateTime.UtcNow.AddMinutes(10) });

            await EnviarEmail(request.Correo, PrepararNotificacion(otp, usuario));

            return new RequestResult();
        }

        private string PrepararNotificacion(string otp, UsuarioDTO usuario)
        {
            return PlantillasHtml.ObtenerCorreoOTP()
                .Replace("{{NOMBRE}}", $"{usuario.Nombre}")
                .Replace("{{CODIGO}}", $"{otp}")
                .Replace("{{FECHA_HORA}}", $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}")
                .Replace("{{ANIO}}", $"{DateTime.Now.Year}");
        }

        private async Task<RequestResult> EnviarEmail(string destinatario, string body)
        {
            var emailRequest = new EmailRequest
            {
                TO = new List<string> { destinatario },
                Subject = "Ferretería - Tu código de verificación para cambiar la contraseña",
                Body = body,
                IsBodyHtml = true,
            };

            return await _mailService.Send(emailRequest);
        }

        public string GenerateOtp()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
