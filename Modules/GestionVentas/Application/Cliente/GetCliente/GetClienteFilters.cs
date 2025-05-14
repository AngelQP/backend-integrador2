using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Cliente.GetCliente
{
    public class GetClienteFilters
    {
        public GetClienteFilters(string nombre, string apellidos, string dni, string ruc)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Dni = dni;
            Ruc = ruc;

        }

        public string Nombre { get; }
        public string Apellidos { get; }
        public string Dni { get; }
        public string Ruc { get; }


    }
}
