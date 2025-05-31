using Ferreteria.Modules.GestionVentas.Application.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Seguridad.UsersExportGet
{
    public class UsersExportGetQuery : IQuery<byte[]>
    {
        public UsersExportGetQuery(string nombre, string rol, int? estado, int startAt, int maxResult)
        {
            Nombre = nombre;
            Rol = rol;
            Estado = estado;
            StartAt = startAt;
            MaxResult = maxResult;
        }

        public string Nombre { get; }
        public string Rol { get; }
        public int? Estado { get; }
        public int StartAt { get; }
        public int MaxResult { get; }
    }
}
