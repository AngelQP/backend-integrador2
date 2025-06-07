using Bigstick.BuildingBlocks.HttpClient.OData;
using Ferreteria.Modules.GestionVentas.Application.Contract;
using Ferreteria.Modules.GestionVentas.Application.Proveedor.CrearProveedor;
using Ferreteria.Modules.GestionVentas.Application.Proveedor.GetProveedor;
using Ferreteria.Modules.GestionVentas.Application.Seguridad.UsersGet;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Proveedor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NPOI.SS.Formula.Functions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ferreteria.GestionVentas.API.Modules.Proveedores
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedoresController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly IGestionVentasModule _proveedores;

        public ProveedoresController(IConfiguration configuration, IWebHostEnvironment env, IGestionVentasModule proveedores)
        {
            _configuration = configuration;
            _env = env;
            _proveedores = proveedores;
        }
        [AllowAnonymous]
        [HttpGet("version")]
        public IActionResult Version()
        {
            return Ok($"Proveedores Service. version: {_configuration["version"]} - Environment: {_env.EnvironmentName}");
        }
        [AllowAnonymous]
        [HttpPost("Proveedor")]
        public async Task<ActionResult> ProveedorCreate(ProveedoresRequest request)
        {
            var command = new CrearProveedorCommand(
                request.Nombre,
                request.Ruc,
                request.Direccion,
                request.Telefono,
                request.Correo,
                request.FechaRegistro
            );
            return Ok(await _proveedores.ExecuteCommandAsync(command));
        }
        [HttpGet("Proveedor")]
        [Produces(typeof(ProveedorGetDTO))]
        public async Task<IActionResult> ProveedorGet([FromQuery] string? nombre, [FromQuery] string? ruc, [FromQuery] string? correo, [FromQuery] string? contacto, [FromQuery] int startAt, [FromQuery] int? maxResult, CancellationToken cancellationToken)
        {
            var filtro = new GetProveedorFilters(nombre, ruc, correo, contacto);
            var query = new QueryPagination<ProveedorGetDTO, GetProveedorFilters>(startAt, maxResult, filtro);

            return Ok(await _proveedores.ExecuteQueryAsync(query));
        }
    }
}
