using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Configuration.Command;
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

namespace Ferreteria.Modules.GestionVentas.Application.Seguridad.Login
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, RequestResult>
    {
        private readonly ISeguridadRepository _seguridadRepository;
        private readonly ICommonService _commonService;
        private readonly IUserContext _userContext;

        public LoginCommandHandler(ISeguridadRepository seguridadRepository, ICommonService commonService, IUserContext userContext)
        {
            _seguridadRepository = seguridadRepository;
            _commonService = commonService;
            _userContext = userContext;
        }

        public async Task<RequestResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _seguridadRepository.ObtenerUsuarioAsync(request.Usuario);

            if (usuario == null || !_commonService.VerifyPassword(request.Contrasenia, usuario.Contrasenia))
                return RequestResult.WithError("El nombre de usuario o la contraseña son incorrectos.");

            var token = _commonService.GenerateToken(
                                        usuario,
                                        request.Configuration["Authentication:Key"],
                                        request.Configuration["Authentication:TokenDuration"],
                                        request.Configuration["Authentication:Issuer"],
                                        request.Configuration["Authentication:Audiencee"]);

            var refreshToken = _commonService.GenerateRefreshToken();

            DateTime refreshExpiration;

            if (request.Recordarme)
                refreshExpiration = DateTime.UtcNow.AddDays(30);
            else
                refreshExpiration = DateTime.UtcNow.AddDays(7);

            //await _commonService.SaveRefreshTokenAsync(usuario.id, refreshToken, refreshExpiration);

            token.RefreshToken = refreshToken;
            var userDto = MapToUserDto(usuario);

            return RequestResult.Success(CreateResponse(userDto, token));
        }

        private Data CreateResponse(UsuarioLogin userDto, Token token)
        {
            return new Data
            {
                Usuario = userDto,
                Token = token,
                ResetToken = null,
                ResetPasswordUrl = null
            };
        }

        private UsuarioLogin MapToUserDto(UsuarioDTO usuario)
        {
            return new UsuarioLogin
            {
                Usuario = usuario.Usuario,
                Correo = usuario.Correo,
                Nombre = usuario.Nombre,
                ApellidoPaterno = usuario.ApellidoPaterno,
                ApellidoMaterno = usuario.ApellidoMaterno,
                Telefono = usuario.Telefono,
                Sociedad = usuario.Sociedad,
                Rol = usuario.Rol,
            };
        }
    }
}
