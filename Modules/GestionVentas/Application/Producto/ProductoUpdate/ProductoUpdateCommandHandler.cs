using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Configuration.Command;
using Ferreteria.Modules.GestionVentas.Application.Seguridad.UserUpdate;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Producto;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Seguridad;
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

namespace Ferreteria.Modules.GestionVentas.Application.Producto.ProductoUpdate
{
    public class ProductoUpdateCommandHandler : ICommandHandler<ProductoUpdateCommand, RequestResult>
    {
        private readonly IProductoRepository _productoRepository;
        private readonly ICommonService _commonService;
        private readonly IUserContext _userContext;

        public ProductoUpdateCommandHandler(IProductoRepository productoRepository, ICommonService commonService, IUserContext userContext)
        {
            _productoRepository = productoRepository;
            _commonService = commonService;
            _userContext = userContext;
        }

        public async Task<RequestResult> Handle(ProductoUpdateCommand request, CancellationToken cancellationToken)
        {
            // Validaciones básicas (puedes agregar más según tu lógica)
            if (string.IsNullOrWhiteSpace(request.Nombre))
                return RequestResult.WithError("El nombre del producto no puede estar vacío.");

            if (request.PrecioUnitario < 0)
                return RequestResult.WithError("El precio unitario no puede ser negativo.");

            if (request.Costo < 0)
                return RequestResult.WithError("El costo no puede ser negativo.");

            var resultado = await _productoRepository.ActualizarProducto(new ActualizarProductoRequest
            {
                IdProducto = request.IdProducto,
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                PrecioUnitario = request.PrecioUnitario,
                Stock = request.Stock,
                Categoria = request.Categoria,
                CodigoBarra = request.CodigoBarra,
                UnidadMedida = request.UnidadMedida,
                EstadoRegistro = request.EstadoRegistro,
                Sku = request.Sku,
                Marca = request.Marca,
                Modelo = request.Modelo,
                Subcategoria = request.Subcategoria,
                ImpuestoTipo = request.ImpuestoTipo,
                Costo = request.Costo,
                Proveedor = request.Proveedor,
            });

            return new RequestResult();
        }
    }
}
