using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
            _proveedores = Proveedores;
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
            var command = new CrearProveedorComand(
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
        [Produces(typeof(GetProveedorDTO))]
        public async Task<IActionResult> ProveedorGet([FromQuery] string? nombre, [FromQuery] string? ruc, [FromQuery] int startAt, [FromQuery] int? maxResult, CancellationToken cancellationToken)
        {
            var filtro = new GetProveedorFilter(nombre, ruc, startAt, maxResult);
            var query = new GetProveedorQuery<GetProveedorDTO, GetProveedorFilter>(startAt, maxResult, filtro);

            return Ok(await _proveedores.ExecuteQueryAsync(query));
        }
}
