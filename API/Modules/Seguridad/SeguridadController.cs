using Ferreteria.GestionVentas.API.Modules.Seguridad.Requests;
using Ferreteria.Modules.GestionVentas.Application.Contract;
using Ferreteria.Modules.GestionVentas.Application.Seguridad.CrearUsuario;
using Ferreteria.Modules.GestionVentas.Application.Seguridad.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Ferreteria.GestionVentas.API.Modules.Seguridad
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeguridadController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly IGestionVentasModule _seguridad;

        public SeguridadController(IConfiguration configuration, IWebHostEnvironment env, IGestionVentasModule seguridad)
        {
            _configuration = configuration;
            _env = env;
            _seguridad = seguridad;
        }

        [AllowAnonymous]
        [HttpGet("version")]
        public IActionResult Version()
        {
            return Ok($"Gestion Ventas Service. version: {_configuration["version"]} - Environment: {_env.EnvironmentName}");
        }

        [AllowAnonymous]
        [HttpPost("usuario/login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            LoginCommand command = new LoginCommand(request.Usuario, request.Contrasenia, request.Recordarme, request.ReturnUrl, _configuration);

            return Ok(await _seguridad.ExecuteCommandAsync(command));
        }

        [HttpPost("usuario")]
        public async Task<ActionResult> UsuarioCreate(UserCreateRequest request)
        {
            CrearUsuarioCommand command = new CrearUsuarioCommand(request.Sociedad, request.Usuario, request.Correo, request.Nombre, request.ApellidoPaterno, request.ApellidoMaterno, request.Telefono, request.Contrasenia, request.ConfirmarContrasenia);

            return Ok(await _seguridad.ExecuteCommandAsync(command));
        }
    }
}
