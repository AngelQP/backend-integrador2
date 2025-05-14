using Bigstick.BuildingBlocks.HttpClient.OData;
using Ferreteria.Modules.GestionVentas.Application.Contract;
using Ferreteria.Modules.GestionVentas.Application.Producto.CrearProducto;
using Ferreteria.Modules.GestionVentas.Application.Producto.GetProducto;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Producto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NPOI.SS.Formula.Functions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ferreteria.GestionVentas.API.Modules.Productos
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly IGestionVentasModule _producto;


        public ProductosController(IConfiguration configuration, IWebHostEnvironment env, IGestionVentasModule producto)
        {
            _configuration = configuration;
            _env = env;
            _producto = producto;
        }

        [AllowAnonymous]
        [HttpGet("version")]
        public IActionResult Version()
        {
            return Ok($"Productos Service. version: {_configuration["version"]} - Environment: {_env.EnvironmentName}");
        }

        [AllowAnonymous]
        [HttpPost("producto")]
        public async Task<ActionResult> ProductoCreate(ProductoRequest request)
        {
            
            var command = new CrearProductoComand(
                request.Nombre,
                request.Descripcion,
                request.Sku,
                request.Marca,
                request.Modelo,
                request.Unidad,
                request.Categoria,
                request.Subcategoria,
                request.ImpuestoTipo,
                request.Precio,
                request.Cantidad,
                request.Costo,
                request.Proveedor,
                request.CodigoBarras,
                request.UsuarioCreacion,
                request.FechaCreacion
            );

            return Ok(await _producto.ExecuteCommandAsync(command));
        }
        [HttpGet("producto")]
        [Produces(typeof(GetProductoDTO))]
        public async Task<IActionResult> ProductoGet([FromQuery] string? nombre, [FromQuery] string? categoria, [FromQuery] string? proveedor, [FromQuery] int startAt, [FromQuery] int? maxResult, CancellationToken cancellationToken)
        {
            var filtro = new GetProductoFilters(nombre, categoria, proveedor);
            var query = new QueryPagination<GetProductoDTO, GetProductoFilters>(startAt, maxResult, filtro);

            return Ok(await _producto.ExecuteQueryAsync(query));
        }
    }
}
