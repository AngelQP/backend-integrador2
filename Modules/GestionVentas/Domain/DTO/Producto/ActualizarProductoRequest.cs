using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Domain.DTO.Producto
{
    public class ActualizarProductoRequest
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Stock { get; set; }
        public string Categoria { get; set; }
        public string CodigoBarra { get; set; }
        public string UnidadMedida { get; set; }
        public bool EstadoRegistro { get; set; }
        public string Sku { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Subcategoria { get; set; }
        public string ImpuestoTipo { get; set; }
        public decimal Costo { get; set; }
        public string Proveedor { get; set; }
        public string UsuarioActualizacion { get; set; }
    }
}
