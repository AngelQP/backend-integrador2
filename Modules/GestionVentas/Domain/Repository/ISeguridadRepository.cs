﻿using Ferreteria.Modules.GestionVentas.Domain.DTO.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Domain.Repository
{
    public interface ISeguridadRepository
    {
        Task<int> CrearUsuario(CrearUsuarioRequest request);
        Task<string> ValidarUsuarioOCorreo(string usuario, string correo, int? idUsuario = null);
        Task<UsuarioDTO> ObtenerUsuarioAsync(string usuario);
        Task<int> GuardarOTP(GuardarOTPRequest request);
        Task<ObtenerCodigoVerificacionDTO> ObtenerCodigoVerificacion(string correo);
        Task<int> ActualizarContraseniaUsuario(int id, string contrasenia, string usuario);
        Task<int> ActualizarCodigoVerificacion(int id, string usuario);
        Task<(IEnumerable<UserDTO>, int)> UsersGet(string sociedad, string nombre, string rol, int? estado, int startAt, int maxResult);
        Task<UserDTO> UserGetById(int idUsuario);
        Task<int> CambiarEstadoUsuario(int idUsuario, int estado, string usuario);
        Task<int> ActualizarUsuario(ActualizarUsuarioRequest request);
    }
}
