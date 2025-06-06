using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Producto.CrearProducto
{
    public class CrearProductoComand : CommandBase<RequestResult>
    {
        public CrearProductoComand(
            string nombre,
            string descripcion,
            string sku,
            string marca,
            string modelo,
            string unidadMedida,
            string categoria,
            string subcategoria,
            string impuestoTipo,
            decimal precioUnitario,
            int stock,
            decimal costo,
            string proveedor,
            string codigoBarra,
            string usuarioCreacion,
            DateTime fechaCreacion,
            bool estado)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Sku = sku;
            Marca = marca;
            Modelo = modelo;
            UnidadMedida = unidadMedida;
            Categoria = categoria;
            Subcategoria = subcategoria;
            ImpuestoTipo = impuestoTipo;
            PrecioUnitario = precioUnitario;
            Stock = stock;
            Costo = costo;
            Proveedor = proveedor;
            CodigoBarra = codigoBarra;
            UsuarioCreacion = usuarioCreacion;
            FechaCreacion = fechaCreacion;
            Estado = estado;
        }

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
