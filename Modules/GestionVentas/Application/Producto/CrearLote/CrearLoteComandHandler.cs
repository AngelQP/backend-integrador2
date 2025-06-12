using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Configuration.Command;
using Ferreteria.Modules.GestionVentas.Application.Producto.CrearProducto;
using Ferreteria.Modules.GestionVentas.Application.Producto.Notification;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Producto;
using Ferreteria.Modules.GestionVentas.Domain.Repository;
using Ferreteria.Modules.GestionVentas.Domain.Servicios;
using Ferreteria.Modules.GestionVentas.Domain.Users;
using MediatR;
using NPOI.SS.Formula.Functions;
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
        private readonly IMediator _mediator;

        public CrearLoteComandHandler(IProductoRepository productoRepository, ICommonService commonService, IUserContext userContext, IMediator mediator)
        {
            _productoRepository = productoRepository;
            _commonService = commonService;
            _userContext = userContext;
            _mediator = mediator;
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
                IdProveedor = request.IdProveedor,
                UsuariosNotificados = request.UsuariosNotificados,
            });
            var producto = await _productoRepository.ProductGetById(request.IdProducto);
            if (producto == null)
            {
                return RequestResult.WithError("Producto no encontrado.");
            }
            producto.Stock += (int)request.CantidadInicial;

           

            await _productoRepository.ActualizarProducto(new ActualizarProductoRequest
            {
                IdProducto = producto.IdProducto,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                PrecioUnitario = producto.PrecioUnitario,
                Stock = producto.Stock,
                Categoria = producto.Categoria,
                CodigoBarra = producto.CodigoBarra,
                UnidadMedida = producto.UnidadMedida,
                EstadoRegistro = producto.Estado,
                Sku = producto.Sku,
                Marca = producto.Marca,
                Modelo = producto.Modelo,
                Subcategoria = producto.Subcategoria,
                ImpuestoTipo = producto.ImpuestoTipo,
                Costo = producto.Costo,
                Proveedor = producto.Proveedor,
            });
            if (request.UsuariosNotificados != null)
            {
                foreach (var correo in request.UsuariosNotificados)
                {
                    await _mediator.Send(new NotificationCommand(correo, request.IdProducto));
                }
            }
            return RequestResult.Success();
        }
    }
}
