using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Configuration.Query;
using Ferreteria.Modules.GestionVentas.Application.Producto.GetCategorias;
using Ferreteria.Modules.GestionVentas.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Producto.GetProveedores
{
    public class GetProveedoresQueryHandler : IQueryHandler<GetProveedoresFilters, RequestResult>
    {
        private readonly IProductoRepository _productoRepository;

        public GetProveedoresQueryHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<RequestResult> Handle(GetProveedoresFilters request, CancellationToken cancellationToken)
        {
            var result = await _productoRepository.GetProveedorLite();
            return RequestResult.Success(result);
        }
    }
}
