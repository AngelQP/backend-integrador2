using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Comunications.Application.Core.Mail;
using Ferreteria.Modules.GestionVentas.Application.Configuration.Query;
using Ferreteria.Modules.GestionVentas.Application.Producto.GetProducto;
using Ferreteria.Modules.GestionVentas.Domain.Repository;
using Ferreteria.Modules.GestionVentas.Domain.Servicios;
using Ferreteria.Modules.GestionVentas.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Producto.GetCategorias
{
    public class GetCategoriasQueryHandler : IQueryHandler<GetCategoriasFilters, RequestResult>
    {
        private readonly IProductoRepository _productoRepository;

        public GetCategoriasQueryHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<RequestResult> Handle(GetCategoriasFilters request, CancellationToken cancellationToken)
        {
            var result = await _productoRepository.GetCategoriaLite();
            return RequestResult.Success(result);
        }
    }
}
