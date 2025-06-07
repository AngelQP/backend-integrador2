using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Configuration.Command;
using Ferreteria.Modules.GestionVentas.Application.Producto.CrearProducto;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Producto;
using Ferreteria.Modules.GestionVentas.Domain.Repository;
using Ferreteria.Modules.GestionVentas.Domain.Servicios;
using Ferreteria.Modules.GestionVentas.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Producto.CrearLote
{
    public class CrearLoteComandHandler : ICommandHandler<CrearLoteComand, RequestResult>
    {
        private readonly IProductoRepository _productoRepository;
        private readonly ICommonService _commonService;
        private readonly IUserContext _userContext;

        public CrearLoteComandHandler(IProductoRepository productoRepository, ICommonService commonService, IUserContext userContext)
        {
            _productoRepository = productoRepository;
            _commonService = commonService;
            _userContext = userContext;
        }

        public async Task<RequestResult> Handle(CrearLoteComand request, CancellationToken cancellationToken)
        {
            var usuario = _userContext.UserId?.Value ?? "sistema";
            var result = await _productoRepository.CrearLote(new CrearLoteRequest
            {
                IdProducto = request.IdProducto,
                NumeroLote = request.NumeroLote,
                FechaIngreso = request.FechaIngreso,
                FechaFabricacion = request.FechaFabricacion,
                FechaVencimiento = request.FechaVencimiento,
                CantidadInicial = request.CantidadInicial,
                CantidadDisponible = request.CantidadDisponible,
                CostoUnitario = request.CostoUnitario,
                UsuarioCreacion = usuario,
                FechaCreacion = DateTime.UtcNow,
                IdProveedor = request.IdProveedor
            });

            return RequestResult.Success();
        }
    }
}
