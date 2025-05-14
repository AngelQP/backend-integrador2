using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Seguridad.UsersGet
{
    public class UsersGetQuery : IQuery<RequestResult>
    {
        public UsersGetQuery(string nombre, int startAt, int maxResult)
        {
            Nombre = nombre;
            StartAt = startAt;
            MaxResult = maxResult;
        }

        public string Nombre { get; }
        public int StartAt { get; }
        public int MaxResult { get; }
        public int Total { get; }
    }
}
