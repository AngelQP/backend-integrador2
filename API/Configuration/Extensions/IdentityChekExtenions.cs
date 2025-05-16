using IdentityServer4.AccessTokenValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.GestionVentas.API.Configuration.Extensions
{
    public static class IdentityChekExtenions
    {
        public static void AddIdentityAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var authority = configuration.GetSection("Authentication:Authority").Get<string>();
            var audience = configuration.GetSection("Authentication:Audience").Get<string>();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    //options.TokenValidationParameters = new TokenValidationParameters
                    //{
                    //    ValidateIssuer = true,
                    //    ValidateAudience = true,
                    //    ValidateLifetime = true,
                    //    ValidateIssuerSigningKey = true,
                    //    ValidIssuer = configuration["Authentication:Issuer"],
                    //    ValidAudience = configuration["Authentication:Audiencee"],
                    //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:Key"]))
                    //};
                });
        }
    }
}
