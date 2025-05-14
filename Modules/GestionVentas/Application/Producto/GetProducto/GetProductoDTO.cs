using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Producto.GetProducto
{
    public class GetProductoDTO
    {
        public class ProductoItem
        {
            public int Id { get; set; }
            public string? Nombre { get; set; }
            public string? Descripcion { get; set; }
            public string? Sku { get; set; }
            public string? Marca { get; set; }
            public string? Modelo { get; set; }
            public string? Unidad { get; set; }
            public string? Categoria { get; set; }
            public string? Subcategoria { get; set; }
            public string? ImpuestoTipo { get; set; }
            public decimal Precio { get; set; }
            public int Cantidad { get; set; }
            public decimal Costo { get; set; }
            public string? Proveedor { get; set; }
            public string? CodigoBarras { get; set; }
            public string? UsuarioCreacion { get; set; }
            public DateTime FechaCreacion { get; set; }
        }
        public IEnumerable<ProductoItem> Items { get; set; }
        public int StartAt { get; set; }
        public int MaxResult { get; set; }
        public int Total { get; set; }
    }

}