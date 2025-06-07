using Ferreteria.Modules.GestionVentas.Domain.DTO.Proveedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Proveedor.GetProveedor
{
    public class ProveedorGetDTO
    {   
        public class ProveedorItem
        {
            public int Id { get; set; }
            public string? Nombre { get; set; }
            public string? Ruc { get; set; }
            public string? Correo { get; set; }
            public string? Contacto { get; set; }
            public DateTime FechaRegistro { get; set; }
        }    
        public IEnumerable<ProveedorItem> Items { get; set }
        public int StartAt { get; set; }
        public int MaxResult { get; set; }
        public int Total { get; set; }
    }
}
