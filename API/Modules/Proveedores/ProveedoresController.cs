using Bigstick.BuildingBlocks.HttpClient.OData;
//using Ferreteria.Modules.GestionVentas.Application.Contract;
//using Ferreteria.Modules.GestionVentas.Application.Proveedores.CrearProveedor;
//using Ferreteria.Modules.GestionVentas.Application.Proveedores.GetProveedor;
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

//namespace Ferreteria.GestionVentas.API.Modules.Proveedores
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class ProveedoresController : ControllerBase
//    {
//        private readonly IConfiguration _configuration;
//        private readonly IWebHostEnvironment _env;
//        private readonly IGestionVentasModule _proveedores;

//        public ProveedoresController(IConfiguration configuration, IWebHostEnvironment env, IGestionVentasModule proveedores)
//        {
//            _configuration = configuration;
//            _env = env;
//            _proveedores = proveedores;
//        }

//        [AllowAnonymous]
//        [HttpGet("version")]
//        public IActionResult Version()
//        {
//            return Ok($"Proveedores Service. version: {_configuration["version"]} - Environment: {_env.EnvironmentName}");
//        }

//        [AllowAnonymous]
//        [HttpPost("Proveedor")]
//        public async Task<ActionResult> ProveedorCreate(ProveedoresRequest request)
//        {
//            var command = new CrearProveedorComand(
//                request.Nombre,
//                request.Ruc,
//                request.Direccion,
//                request.Telefono,
//                request.Correo,
//                request.FechaRegistro
//            );
//            return Ok(await _proveedores.ExecuteCommandAsync(command));
//        }
//        [HttpGet("Proveedor")]
//        [Produces(typeof(ProveedorGetDTO))]
//        public async Task<IActionResult> ProveedorGet([FromQuery] string? nombre, [FromQuery] string? ruc, [FromQuery] int startAt, [FromQuery] int? maxResult, CancellationToken cancellationToken)
//        {
//            int resultLimit = maxResult ?? 10;
//            var filters = new GetProveedorFilter(nombre, ruc, startAt, resultLimit);
            

//            return Ok(await _proveedores.ExecuteQueryAsync(filters));
//        }
//    }
//}
