using Bigstick.BuildingBlocks.HttpClient.OData;
using Ferreteria.Modules.GestionVentas.Application.Contract;
using Ferreteria.Modules.GestionVentas.Application.Producto.CrearProducto;
using Ferreteria.Modules.GestionVentas.Application.Producto.GetCategorias;
using Ferreteria.Modules.GestionVentas.Application.Producto.GetProducto;
using Ferreteria.Modules.GestionVentas.Application.Producto.GetProveedores;
using Ferreteria.Modules.GestionVentas.Application.Producto.ProductGetById;
using Ferreteria.Modules.GestionVentas.Application.Producto.ProductoUpdate;
using Ferreteria.Modules.GestionVentas.Application.Seguridad.UserGetById;
using Ferreteria.Modules.GestionVentas.Application.Seguridad.UsersGet;
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
                request.UnidadMedida,
                request.Categoria,
                request.Subcategoria,
                request.ImpuestoTipo,
                request.PrecioUnitario,
                request.Stock,
                request.Costo,
                request.Proveedor,
                request.CodigoBarra,
                request.UsuarioCreacion,
                request.FechaCreacion,
                request.Estado
            );

            return Ok(await _producto.ExecuteCommandAsync(command));
        }
        [HttpGet("producto")]
        public async Task<IActionResult> ProductoGet(
            [FromQuery] string? nombre,
            [FromQuery] string? categoria,
            [FromQuery] string? proveedor,
            [FromQuery] int startAt,
            [FromQuery] int? maxResult,
            CancellationToken cancellationToken)
        {
            int resultLimit = maxResult ?? 10; // Valor por defecto si es null
            var filters = new GetProductoFilters(nombre, categoria, proveedor, startAt, resultLimit);
            return Ok(await _producto.ExecuteQueryAsync(filters));
        }
        [HttpGet("categoriasLite")]
        public async Task<IActionResult> GetCategoriasLite(CancellationToken cancellationToken)
        {
            var filters = new GetCategoriasFilters(); 
            var result = await _producto.ExecuteQueryAsync(filters);
            return Ok(result);
        }
        [HttpGet("ProveedoresLite")]
        public async Task<IActionResult> GetProveedorLite(CancellationToken cancellationToken)
        {
            var filters = new GetProveedoresFilters();
            var result = await _producto.ExecuteQueryAsync(filters);
            return Ok(result);
        }
        [HttpGet("productos/{id}")]
        public async Task<IActionResult> ProductGetById(int id)
        {
            return Ok(await _producto.ExecuteQueryAsync(new ProductGetByIdQuery(id)));
        }
        [HttpPut("productos/{id}")]
        public async Task<IActionResult> ProductoUpdate(int id, [FromBody] ProductoRequest request)
        {
            var command = new ProductoUpdateCommand(
                id,
                request.Nombre,
                request.Descripcion,
                request.PrecioUnitario,
                request.Stock,
                request.Categoria,
                request.CodigoBarra,
                request.UnidadMedida,
                request.Estado,
                request.Sku,
                request.Marca,
                request.Modelo,
                request.Subcategoria,
                request.ImpuestoTipo,
                request.Costo,
                request.Proveedor,
                request.UsuarioCreacion

            );

            return Ok(await _producto.ExecuteCommandAsync(command));
        }
    }
}
