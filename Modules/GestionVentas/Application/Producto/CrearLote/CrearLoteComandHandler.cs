using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Configuration.Command;
using Ferreteria.Modules.GestionVentas.Application.Producto.CrearProducto;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Producto;
using Ferreteria.Modules.GestionVentas.Domain.Repository;
using Ferreteria.Modules.GestionVentas.Domain.Servicios;
using Ferreteria.Modules.GestionVentas.Domain.Users;
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
            var producto = await _productoRepository.ProductGetById(request.IdProducto);
            if (producto == null)
            {
                return RequestResult.WithError("Producto no encontrado.");
            }
            producto.Stock += (int)request.CantidadInicial;

            // 4. Actualizar el producto con el nuevo stock
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
            return RequestResult.Success();
        }
    }
}
