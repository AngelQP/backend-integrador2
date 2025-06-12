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

namespace Ferreteria.Modules.GestionVentas.Application.Producto.GetUsuarioLite
{
    public class GetUsuariosLiteQueryHandler : IQueryHandler<GetUsuariosLiteFilters, RequestResult>
    {
        private readonly IProductoRepository _productoRepository;

        public GetUsuariosLiteQueryHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<RequestResult> Handle(GetUsuariosLiteFilters request, CancellationToken cancellationToken)
        {
            var result = await _productoRepository.GetUsuarioLite();
            return RequestResult.Success(result);
        }
    }
}
