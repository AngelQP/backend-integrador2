using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Bigstick.BuildingBlocks.Application;
using Bigstick.BuildingBlocks.EventBus.AzureServiceBus;
using HealthChecks.UI.Client;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Compact;
using Ferreteria.GestionVentas.API.Configuration.ApplicationInsights;
using Ferreteria.GestionVentas.API.Configuration.ExecutionContext;
using Ferreteria.GestionVentas.API.Configuration.Extensions;
using Ferreteria.Modules.GestionVentas.Infrastructure.Configuration;
using Ferreteria.GestionVentas.API.Modules;

namespace Ferreteria.GestionVentas.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private static ILogger _logger;

        private static ILogger _loggerForApi;

        private const string _version = "2207030108";
        private const string _environment = "Staging";
        private const string _connectionstringkey = "ConnectionStrings:GestionVentas";
        private const string _tramarsaConnectionstringkey = "ConnectionStrings:Tramarsa";
        private const string _masterServicekey = "service:urlMasterService";
        private const string _mailServicekey = "service:urlMailService";
        private const string _billLandingServiceKey = "service:urlBillLandingService";
        private const string _clientServiceKey = "service:urlClientService";
        private const string _itinerarioServiceKey = "service:urlItinerarioService";
        private const string _coreServiceKey = "service:urlCoreService";
        private const string _naveServiceKey = "service:urlNaveService";
        private const string _fileServiceKey = "service:urlFileService";
        private const string _serviceBusConnectionString = "busConnectionString:azureServiceBus";
        private const string _azureBlobConnectionKey = "service:azureBlobConnection";
        private const string _azureContainerBaseKey = "blobAzure:containerBase";

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _configuration = configuration;
            ConfigureLogger();

            configuration["version"] = _version;
            env.EnvironmentName = _environment;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcForGestionVentas(_configuration);

            services.AddSwaggerDocumentation();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();

            services.AddHealthChecksDesglose(_configuration, _connectionstringkey);

            services.AddIdentityAuthentication(_configuration);

            services.AddApplicationInsightsTelemetry();

            services.AddApplicationInsightsKubernetesEnricher();

            services.AddTransient<RequestBodyLoggingMiddleware>();

            services.AddTransient<ResponseBodyLoggingMiddleware>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TelemetryClient telemetryClient)
        {
            var container = app.ApplicationServices.GetAutofacRoot();

            InitializeModules(container);

            app.UseMiddleware<CorrelationMiddleware>();
            
            app.UseSwaggerDocumentation();


            if (env.IsDevelopment() || env.IsStaging() || env.IsProduction())
            {
                app.UseDesgloseExceptionHandler(telemetryClient, true);

                app.UseRequestBodyLogging();

                app.UseResponseBodyLogging();

                TelemetryConfiguration.Active.DisableTelemetry = true;

                TelemetryDebugWriter.IsTracingDisabled = true;
            }
            else
            {
                app.UseDesgloseExceptionHandler(telemetryClient, false);

                app.UseRequestBodyLogging();

                app.UseResponseBodyLogging();

                app.UseHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            }

            app.UseAuthentication();

            app.UseRouting();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());

        }

        private static void ConfigureLogger()
        {
            _logger = new LoggerConfiguration()
               .Enrich.FromLogContext()
               .CreateLogger();

            _loggerForApi = _logger.ForContext("Module", "API");

            _loggerForApi.Information("Logger configured");
        }

        private void InitializeModules(ILifetimeScope container)
        {
            var httpContextAccessor = container.Resolve<IHttpContextAccessor>();

            var executionContextAccessor = new ExecutionContextAccessor(httpContextAccessor);

            var connectionstring = _configuration[_connectionstringkey];
            var tramarsaConnectionstring = _configuration[_tramarsaConnectionstringkey];
            var urlMasterService = _configuration[_masterServicekey];
            var urlBillLandingService = _configuration[_billLandingServiceKey];
            var urlClientService = _configuration[_clientServiceKey];
            var urlItinerarioService = _configuration[_itinerarioServiceKey];
            var urlCoreService = _configuration[_coreServiceKey];
            var urlNaveService = _configuration[_naveServiceKey];
            var urlFileService = _configuration[_fileServiceKey];
            var urlMailService = _configuration[_mailServicekey];
            var serviceBusConnectionString = _configuration[_serviceBusConnectionString];
            var azureBlobConnection = _configuration[_azureBlobConnectionKey];
            var azureContainerBase = _configuration[_azureContainerBaseKey];

            GestionVentasStartup.Initialize(connectionstring, tramarsaConnectionstring, urlBillLandingService, urlMasterService, urlClientService, urlItinerarioService, urlCoreService, urlNaveService, urlFileService, urlMailService, serviceBusConnectionString, azureBlobConnection, azureContainerBase, executionContextAccessor, _logger);
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new GestionVentasAutofacModule());
        }
    }
}
