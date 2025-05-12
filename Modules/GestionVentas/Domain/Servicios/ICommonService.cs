using Ferreteria.Modules.GestionVentas.Domain.DTO.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Domain.Servicios
{
    public interface ICommonService
    {
        string HashPassword(string password);
        bool VerifyPassword(string enteredPassword, string storedHash);
        bool IsValidPassword(string password);
        bool IsValidEmail(string email);
        Token GenerateToken(UsuarioDTO usuario, string key, string tokenDuration, string issuer, string audience);
        string GenerateRefreshToken();
    }
}
