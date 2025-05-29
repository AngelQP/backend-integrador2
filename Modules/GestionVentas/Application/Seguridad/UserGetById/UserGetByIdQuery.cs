using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Seguridad.UserGetById
{
    public class UserGetByIdQuery : IQuery<RequestResult>
    {
        public int IdUsuario { get; set; }

        public UserGetByIdQuery(int idUsuario)
        {
            IdUsuario = idUsuario;
        }
    }
}
