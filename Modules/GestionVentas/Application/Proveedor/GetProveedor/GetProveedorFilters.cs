using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Proveedor.GetProveedor
{
    public class GetProveedorFilters
    {
        public GetProveedorFilters(string nombre, string ruc, string correo, string contacto)
        {
            Nombre = nombre;
            Ruc = ruc;
            Correo = correo;
            Contacto = contacto;
        }

        public string Nombre { get; }
        public string Ruc { get; }
        public string Correo { get; }
        public string Contacto { get; }

    }
}
