using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Seguridad.ForgotPassword
{
    public class ForgotPasswordCommand : CommandBase<RequestResult>
    {
        public ForgotPasswordCommand(string correo)
        {
            Correo = correo;
        }

        public string Correo { get; set; }
    }
}
