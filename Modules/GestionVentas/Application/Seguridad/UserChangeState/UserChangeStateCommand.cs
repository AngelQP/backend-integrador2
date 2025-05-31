using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Seguridad.UserChangeState
{
    public class UserChangeStateCommand : CommandBase<RequestResult>
    {
        public UserChangeStateCommand(int idUsuario, int estado)
        {
            IdUsuario = idUsuario;
            Estado = estado;
        }

        public int IdUsuario { get; set; }
        public int Estado { get; set; }
    }
}
