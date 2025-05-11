using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Ferreteria.GestionVentas.API.Configuration.Extensions
{
    public static class MvcExtensions
    {
        public static void AddMvcForGestionVentas(this IServiceCollection services, IConfiguration configuration)
        {
            var scope = configuration.GetSection("Authentication:Audience").Get<string>();

            services.AddMvc(opt =>
            {
                var policy = ScopePolicy.Create(scope);
                opt.Filters.Add(new AuthorizeFilter(policy));
            })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new IsoDateTimeConverter
                    {
                        DateTimeStyles = DateTimeStyles.AdjustToUniversal
                    });
                });
        }
    }
}
