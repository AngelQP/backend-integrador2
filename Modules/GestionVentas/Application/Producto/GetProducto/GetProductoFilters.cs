using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Producto.GetProducto
{
    public class GetProductoFilters
    {
        public GetProductoFilters(string nombre, string categoria, string proveedor)
        {
            Nombre = nombre;
            Categoria = categoria;
            Proveedor = proveedor;

        }

        public string Nombre { get; }
        public string Categoria { get; }
        public string Proveedor { get; }


    }
}
