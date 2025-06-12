using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Domain.DTO.Producto
{
    public class CrearLoteRequest
    {
        public int IdProducto { get; set; }
        public string NumeroLote { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaFabricacion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal CantidadInicial { get; set; }
        public decimal CantidadDisponible { get; set; }
        public decimal CostoUnitario { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdProveedor { get; set; }
        public List<string>? UsuariosNotificados { get; set; }
    }
}
