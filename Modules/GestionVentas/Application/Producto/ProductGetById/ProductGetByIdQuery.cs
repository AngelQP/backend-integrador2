using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Producto.ProductGetById
{
    public class ProductGetByIdQuery : IQuery<RequestResult>
    {
        public int IdProducto { get; set; }

        public ProductGetByIdQuery(int idProducto)
        {
            IdProducto = idProducto;
        }
    }
}
