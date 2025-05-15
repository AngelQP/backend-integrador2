using Bigstick.BuildingBlocks.Application.Data;
using Dapper;
using Ferreteria.Modules.GestionVentas.Application.Configuration.Query;
using Ferreteria.Modules.GestionVentas.Application.Contract;
using Microsoft.Azure.Amqp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ferreteria.Modules.GestionVentas.Application.Contract;
using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Comunications.Application.Core.Mail;
using Ferreteria.Modules.GestionVentas.Application.Seguridad.UsersGet;
using Ferreteria.Modules.GestionVentas.Domain.Repository;
using Ferreteria.Modules.GestionVentas.Domain.Servicios;
using Ferreteria.Modules.GestionVentas.Domain.Users;

namespace Ferreteria.Modules.GestionVentas.Application.Producto.GetProducto
{
    public class GetProductoQueryHandler : IQueryHandler<GetProductoFilters, RequestResult>
    {
        private readonly IProductoRepository _productoRepository;
        private readonly ICommonService _commonService;
        private readonly IMailService _mailService;
        private readonly IUserContext _userContext;

        public GetProductoQueryHandler(IProductoRepository productoRepository, ICommonService commonService, IMailService mailService, IUserContext userContext)
        {
            _productoRepository = productoRepository;
            _commonService = commonService;
            _mailService = mailService;
            _userContext = userContext;
        }

        public async Task<RequestResult> Handle(GetProductoFilters request, CancellationToken cancellationToken)
        {
            var result = await _productoRepository.ProductoGet(request.Nombre, request.Categoria, request.Proveedor, request.StartAt, request.MaxResult);

            var response = new GetProductoDTO(result.Item1, request.StartAt, request.MaxResult, result.Item2);

            return RequestResult.Success(response);
        }
    }
}



