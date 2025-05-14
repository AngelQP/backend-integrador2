using Ferreteria.Modules.GestionVentas.Application.Contract;
using Ferreteria.Modules.GestionVentas.Application.Cliente.CrearCliente;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Cliente;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NPOI.SS.Formula.Functions;
using System;
using System.Threading.Tasks;
using Ferreteria.Modules.GestionVentas.Application.Cliente.GetCliente;
using System.Threading;

namespace Ferreteria.GestionVentas.API.Modules.Clientes
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly IGestionVentasModule _cliente;


        public ClientesController(IConfiguration configuration, IWebHostEnvironment env, IGestionVentasModule cliente)
        {
            _configuration = configuration;
            _env = env;
            _cliente = cliente;
        }

        [AllowAnonymous]
        [HttpGet("version")]
        public IActionResult Version()
        {
            return Ok($"Clientes Service. version: {_configuration["version"]} - Environment: {_env.EnvironmentName}");
        }

        [AllowAnonymous]
        [HttpPost("Cliente")]
        public async Task<ActionResult> ClienteCreate(ClienteRequest request)
        {
            
            var command = new CrearClienteComand(
                request.Nombre,
                request.Apellidos,
                request.Dni,
                request.Ruc,
                request.Direccion,
                request.Telefono,
                request.Correo,
                request.EsEmpresa,
                request.FechaRegistro
            );

            return Ok(await _cliente.ExecuteCommandAsync(command));
        }
        [HttpGet("cliente")]
        [Produces(typeof(GetClienteDTO))]
        public async Task<IActionResult> ClienteGet([FromQuery] string? nombre, [FromQuery] string? apellidos, [FromQuery] string? dni,[FromQuery] string? ruc, [FromQuery] int startAt, [FromQuery] int? maxResult, CancellationToken cancellationToken)
        {
            var filtro = new GetClienteFilters(nombre, apellidos, dni, ruc);
            var query = new QueryPagination<GetClienteDTO, GetClienteFilters>(startAt, maxResult, filtro);

            return Ok(await _cliente.ExecuteQueryAsync(query));
        }
    }
}
