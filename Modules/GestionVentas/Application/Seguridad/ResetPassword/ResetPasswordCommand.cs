using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Seguridad.ResetPassword
{
    public class ResetPasswordCommand : CommandBase<RequestResult>
    {
        public ResetPasswordCommand(string correo, string oTP, string nuevaContrasenia, string confirmarContrasenia)
        {
            Correo = correo;
            OTP = oTP;
            NuevaContrasenia = nuevaContrasenia;
            ConfirmarContrasenia = confirmarContrasenia;
        }

        public string Correo { get; set; }
        public string OTP { get; set; }
        public string NuevaContrasenia { get; set; }
        public string ConfirmarContrasenia { get; set; }
    }
}
