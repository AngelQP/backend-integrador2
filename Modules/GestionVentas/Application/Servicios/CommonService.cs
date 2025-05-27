using Ferreteria.Modules.GestionVentas.Domain.DTO.Seguridad;
using Ferreteria.Modules.GestionVentas.Domain.Servicios;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Servicios
{
    public class CommonService : ICommonService
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string enteredPassword, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHash);
        }

        public bool IsValidPassword(string password)
        {
            return password.Length >= 8 &&
                   password.Any(char.IsDigit) &&
                   password.Any(char.IsLetter) &&
                   password.Any(ch => "!@#$%^&*()".Contains(ch));
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public Token GenerateToken(UsuarioDTO usuario, string key, string tokenDuration, string issuer, string audience)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(int.Parse(tokenDuration));
            var expiresIn = (int)(expiration - DateTime.UtcNow).TotalSeconds;

            var claims = CreateTokenClaims(usuario);

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: expiration,
                signingCredentials: credentials);

            return new Token
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresIn = expiresIn,
                TokenType = "Bearer"
            };
        }

        private List<Claim> CreateTokenClaims(UsuarioDTO usuario)
        {
            return new List<Claim>
            {
                new Claim("userName", usuario.Usuario),
                new Claim("email", usuario.Correo),
                new Claim("full_name", $"{usuario.Nombre} {usuario.ApellidoPaterno} {usuario.ApellidoMaterno}"),
                new Claim("iat", ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim("nbf", ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim("sub", usuario.IdUsuario.ToString()),
                new Claim("oid", usuario.IdUsuario.ToString()),
                new Claim("business", usuario.Sociedad)
            };
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
