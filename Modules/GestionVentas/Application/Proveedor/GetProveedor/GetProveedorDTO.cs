using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Proveedor.GetProveedor
{
    public class GetProveedorDTO
    {
        public class ProveedorItem
        {
            public int IdProveedor { get; set; }
            public string? Nombre { get; set; }
            public string? Ruc { get; set; }
            public string? Direccion { get; set; }
            public string? Telefono { get; set; }
            public string? Correo { get; set; }
            public string? Contacto { get; set; }
            public bool? Estado { get; set; }
            public DateTime Fecha_Registro { get; set; }
        }
        public IEnumerable<ProveedorItem> Items { get; set; }
        public int StartAt { get; set; }
        public int MaxResult { get; set; }
        public int Total { get; set; }
    }
}
