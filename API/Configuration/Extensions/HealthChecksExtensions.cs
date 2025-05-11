using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ferreteria.GestionVentas.API.Configuration.Extensions
{
    public static class HealthChecksExtensions
    {
        public static void AddHealthChecksDesglose(this IServiceCollection services, IConfiguration configuration, string connectionstringkey)
        {
            var connectionString = configuration[connectionstringkey];
            var authority = configuration["Authentication:Authority"];

            services.AddHealthChecks()
                    .AddSqlServer(connectionString);
                    //.AddIdentityServer(new Uri(authority));

        }
    }
}
