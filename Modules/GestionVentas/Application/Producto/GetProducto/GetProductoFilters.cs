using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Producto.GetProducto
{
    public class GetProductoFilters : IQuery<RequestResult>
    {
        public GetProductoFilters(string nombre, string categoria, string proveedor, int startAt, int maxResult)
        {
            Nombre = nombre;
            Categoria = categoria;
            Proveedor = proveedor;
            StartAt = startAt;
            MaxResult = maxResult;
        }

        public string Nombre { get; }
        public string Categoria { get; }
        public string Proveedor { get; }
        public int StartAt { get; }
        public int MaxResult { get; }
        public int Total { get; }
    }
}
