using Ferreteria.Modules.GestionVentas.Domain.DTO.Seguridad;
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
        Task<string> ValidarUsuarioOCorreo(string usuario, string correo);
        Task<UsuarioDTO> ObtenerUsuarioAsync(string usuario);
        Task<int> GuardarOTP(GuardarOTPRequest request);
        Task<ObtenerCodigoVerificacionDTO> ObtenerCodigoVerificacion(string correo);
        Task<int> ActualizarContraseniaUsuario(int id, string contrasenia, string usuario);
        Task<int> ActualizarCodigoVerificacion(int id, string usuario);
        Task<(IEnumerable<UserDTO>, int)> UsersGet(string nombre, int startAt, int maxResult);
        Task<UserDTO> UserGetById(int idUsuario);
    }
}
