using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Configuration.Command;
using Ferreteria.Modules.GestionVentas.Application.Seguridad.CrearUsuario;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Producto;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Seguridad;
using Ferreteria.Modules.GestionVentas.Domain.Repository;
using Ferreteria.Modules.GestionVentas.Domain.Servicios;
using Ferreteria.Modules.GestionVentas.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Producto.CrearProducto
{
    public class CrearProductoComandHandler : ICommandHandler<CrearProductoComand, RequestResult>
    {
        private readonly IProductoRepository _productoRepository;
        private readonly ICommonService _commonService;
        private readonly IUserContext _userContext;

        public CrearProductoComandHandler(IProductoRepository productoRepository, ICommonService commonService, IUserContext userContext)
        {
            _productoRepository = productoRepository;
            _commonService = commonService;
            _userContext = userContext;
        }

        public async Task<RequestResult> Handle(CrearProductoComand request, CancellationToken cancellationToken)
        {
            var usuario = _userContext.UserId.Value;

            var result = await _productoRepository.CrearProducto(new CrearProductoRequest
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                Sku = request.Sku,
                Marca = request.Marca,
                Modelo = request.Modelo,
                Unidad = request.Unidad,
                Categoria = request.Categoria,
                Subcategoria = request.Subcategoria,
                ImpuestoTipo = request.ImpuestoTipo,
                Precio = request.Precio,
                Cantidad = request.Cantidad,
                Costo = request.Costo,
                Proveedor = request.Proveedor,
                CodigoBarras = request.CodigoBarras,
                UsuarioCreacion = usuario,
                FechaCreacion = DateTime.UtcNow
            });

            return RequestResult.Success();
        }
    }
}
