using Bigstick.BuildingBlocks.Application;
using Bigstick.BuildingBlocks.Application.Response;
using Bigstick.BuildingBlocks.Domain;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ferreteria.GestionVentas.API.Configuration.Extensions
{
    public static class ExceptionHandlerExtensions
    {
        
        public static void UseDesgloseExceptionHandler(
            this IApplicationBuilder app,
            TelemetryClient telemetryClient,
            bool isDeveloment)
        {
            Dictionary<Type, Func<HttpContext, Exception, Task>> handlers = new Dictionary<Type, Func<HttpContext, Exception, Task>>();

            handlers.Add(typeof(BusinessRuleValidationException), BusinessRuleValidationExceptionHandle);

            handlers.Add(typeof(BusinessApplicationRuleValidationException), BusinessApplicationRuleValidationExceptionHandle);

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {

                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    if (!isDeveloment) 
                    {
                        TrackException(telemetryClient, exceptionHandlerPathFeature.Error);
                    }

                    if (handlers.ContainsKey(exceptionHandlerPathFeature.Error.GetType())) 
                    {
                        await handlers[exceptionHandlerPathFeature.Error.GetType()].Invoke(context, exceptionHandlerPathFeature.Error);
                    }
                    else
                    {
                        context.Response.StatusCode = 500;

                        context.Response.ContentType = "application/json";

                        Exception ex = isDeveloment ? exceptionHandlerPathFeature.Error : null;

                        var or =  new { message = "Internal server error.", exception=ex  };

                        await context.Response.WriteAsync(ToJson(or));
                    }

                    
                });
            });
        }
        private static async Task  BusinessRuleValidationExceptionHandle(HttpContext context, Exception ex) 
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;

            context.Response.ContentType = "application/json";

            var result  = RequestResult.WithError(((BusinessRuleValidationException)ex).Message);

            await context.Response.WriteAsync(ToJson(result));
        }
        private static async Task BusinessApplicationRuleValidationExceptionHandle(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;

            context.Response.ContentType = "application/json";

            var result = RequestResult.WithError(((BusinessApplicationRuleValidationException)ex).Message);

            await context.Response.WriteAsync(ToJson(result));
        }

        private static string ToJson(object value) 
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            return JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
        }

        private static void TrackException(TelemetryClient telemetryClient, Exception ex) 
        {
            try
            {
                telemetryClient.TrackException(ex);
            }
            catch (Exception) 
            {
            
            }
            
            
        }
    }
}
