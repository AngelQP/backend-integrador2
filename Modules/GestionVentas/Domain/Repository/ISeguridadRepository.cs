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
        Task<UsuarioDTO> GetUsuarioAsync(string usuario);
    }
}
