using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Comunications.Application.Core.Mail;
using Ferreteria.Modules.GestionVentas.Application.Configuration.Query;
using Ferreteria.Modules.GestionVentas.Application.Seguridad.UserGetById;
using Ferreteria.Modules.GestionVentas.Domain.Enums;
using Ferreteria.Modules.GestionVentas.Domain.Repository;
using Ferreteria.Modules.GestionVentas.Domain.Servicios;
using Ferreteria.Modules.GestionVentas.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Producto.ProductGetById
{
    public class ProductGetByIdQueryHandler : IQueryHandler<ProductGetByIdQuery, RequestResult>
    {
        private readonly IProductoRepository _productoRepository;
        private readonly ICommonService _commonService;
        private readonly IMailService _mailService;
        private readonly IUserContext _userContext;

        public ProductGetByIdQueryHandler(IProductoRepository productoRepository, ICommonService commonService, IMailService mailService, IUserContext userContext)
        {
            _productoRepository = productoRepository;
            _commonService = commonService;
            _mailService = mailService;
            _userContext = userContext;
        }

        public async Task<RequestResult> Handle(ProductGetByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _productoRepository.ProductGetById(request.IdProducto);

            if (result == null)
                return RequestResult.WithError("El producto no se encontró.");

            return RequestResult.Success(result);
        }
    }
}
