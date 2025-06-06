using System;

namespace Ferreteria.GestionVentas.API.Modules.Productos
{
    public class ProductoRequest
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Sku { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string UnidadMedida { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public string ImpuestoTipo { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Stock { get; set; }
        public decimal Costo { get; set; }
        public string Proveedor { get; set; }
        public string CodigoBarra { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Estado { get; set; }
    }
}
