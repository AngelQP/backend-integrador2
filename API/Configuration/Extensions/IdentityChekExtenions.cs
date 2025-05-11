using IdentityServer4.AccessTokenValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    options.Authority = authority;
                    options.Audience = audience;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        NameClaimType = "id",
                        RoleClaimType = "role"
                    };

                });
        }
    }
}
