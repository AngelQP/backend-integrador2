using Ferreteria.GestionVentas.API.Modules.Seguridad.Requests;
using Ferreteria.Modules.GestionVentas.Application.Contract;
using Ferreteria.Modules.GestionVentas.Application.Seguridad.CrearUsuario;
using Ferreteria.Modules.GestionVentas.Application.Seguridad.ForgotPassword;
using Ferreteria.Modules.GestionVentas.Application.Seguridad.Login;
using Ferreteria.Modules.GestionVentas.Application.Seguridad.ResetPassword;
using Ferreteria.Modules.GestionVentas.Application.Seguridad.UsersGet;
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

        #region APIS DE PRUEBA

        [HttpPost("usuario")]
        public async Task<ActionResult> UsuarioCreate(UserCreateRequest request)
        {
            CrearUsuarioCommand command = new CrearUsuarioCommand(request.Sociedad, request.Usuario, request.Correo, request.Nombre, request.ApellidoPaterno, request.ApellidoMaterno, request.Telefono, request.Contrasenia, request.Rol, request.ConfirmarContrasenia);

            return Ok(await _seguridad.ExecuteCommandAsync(command));
        }

        [HttpGet("usuarios")]
        public async Task<IActionResult> UsersGet([FromQuery] string nombre, int startAt, int maxResult)
        {
            return Ok(await _seguridad.ExecuteQueryAsync(new UsersGetQuery(nombre, startAt, maxResult)));
        }
        #endregion

        [HttpPost("forgot-password")]
        public async Task<ActionResult> AuthForgotPassword(ForgotPasswordRequest request)
        {
            return Ok(await _seguridad.ExecuteCommandAsync(new ForgotPasswordCommand(request.Correo)));
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> AuthResetPassword(ResetPasswordRequest request)
        {
            return Ok(await _seguridad.ExecuteCommandAsync(new ResetPasswordCommand(request.Correo, request.OTP, request.NuevaContrasenia, request.ConfirmarContrasenia)));
        }
    }
}
