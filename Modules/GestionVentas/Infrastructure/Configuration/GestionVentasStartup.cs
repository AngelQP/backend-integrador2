using Autofac;
using Bigstick.BuildingBlocks.Application;
using Bigstick.BuildingBlocks.EventBus.AzureServiceBus;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferreteria.Modules.GestionVentas.Infrastructure.Configuration.Authentication;
using Ferreteria.Modules.GestionVentas.Infrastructure.Configuration.Common;
using Ferreteria.Modules.GestionVentas.Infrastructure.Configuration.DataAccess;
using Ferreteria.Modules.GestionVentas.Infrastructure.Configuration.Domain;
using Ferreteria.Modules.GestionVentas.Infrastructure.Configuration.ExternalService;
using Ferreteria.Modules.GestionVentas.Infrastructure.Configuration.Mediation;
using Ferreteria.Modules.GestionVentas.Infrastructure.Configuration.Processing;
using Ferreteria.Modules.GestionVentas.Infrastructure.Configuration.Processing.Outbox;
using Ferreteria.Modules.GestionVentas.Infrastructure.Configuration.Queue;
using Ferreteria.Modules.GestionVentas.Infrastructure.Configuration.Tracing;

namespace Ferreteria.Modules.GestionVentas.Infrastructure.Configuration
{
    public static class GestionVentasStartup
    {
        public static IContainer _container;

        public static void Initialize(string connectionString, string tramarsaConnectionString, string urlBillLandingService, string urlMasterService, string urlClientService, string urlItinerarioService, string urlCoreService, string urlNaveService, string urlFileService, string urlMailService, string serviceBusConnectionString, string azureBlobConnection, string azureContainerBase,
            IExecutionContextAccessor executionContextAccessor, ILogger logger)
        {
            var moduleLogger = logger.ForContext("Module", "GestionVentas");
            ConfigureCompositionRoot( connectionString, tramarsaConnectionString, urlBillLandingService, urlMasterService, urlClientService, urlItinerarioService, urlCoreService, urlNaveService, urlFileService, urlMailService, serviceBusConnectionString, azureBlobConnection, azureContainerBase, executionContextAccessor, moduleLogger);
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            string tramarsaConnectionString,
            string urlBillLandingService,
            string urlMasterService,
            string urlClientService,
            string urlItinerarioService,
            string urlCoreService,
            string urlNaveService,
            string urlFileService,
            string urlMailService,
            string serviceBusConnectionString,
            string azureBlobConnection, 
            string azureContainerBase,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger)
        {
            
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new MediatRModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new DataAccessModule(connectionString, tramarsaConnectionString));
            containerBuilder.RegisterModule(new DomainModule());
            containerBuilder.RegisterModule(new AuthenticationModule());
            containerBuilder.RegisterModule(new OutboxModule());
            containerBuilder.RegisterModule(new CommonModule());
            containerBuilder.RegisterModule(new ExternalServiceModule(urlBillLandingService, urlMasterService, urlClientService, urlItinerarioService, urlCoreService, urlNaveService, urlFileService, urlMailService, azureBlobConnection, azureContainerBase));
            containerBuilder.RegisterModule(new AzureServiceBus(serviceBusConnectionString));
            //containerBuilder.RegisterModule(new TracerText()));
            containerBuilder.RegisterInstance(executionContextAccessor);
            _container = containerBuilder.Build();

            GestionVentasCompositionRoot.SetContainer(_container);

        }
    }
}
