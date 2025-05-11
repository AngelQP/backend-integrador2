using Autofac;
using Bigstick.BuildingBlocks.HttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferreteria.Comunications.Infrastructure.Core;
//using Ferreteria.Modules.GestionVentas.GestionVentas.API.Modules.GestionVentas;
//using Ferreteria.Modules.GestionVentas.Infrastructure.Domain.FilesBlob;

namespace Ferreteria.Modules.GestionVentas.Infrastructure.Configuration.ExternalService
{
    internal class ExternalServiceModule : Module
    {
        private readonly string _urlBillLandingService;
        private readonly string _urlMasterService;
        private readonly string _urlClientService;
        private readonly string _urlItinerarioService;
        private readonly string _urlCoreService;
        private readonly string _urlNaveService;
        private readonly string _urlFileService;
        private readonly string _urlMailService;
        private readonly string _azureBlobConnection;
        private readonly string _azureContainerBase;

        public ExternalServiceModule(string urlBillLandignService, string urlMasterService, string urlClientService, string urlItinerarioService, string urlCoreService, string urlNaveService, string urlFileService, string urlMailService, string azureBlobConnection,
            string azureContainerBase)
        {
            _urlBillLandingService = urlBillLandignService;
            _urlMasterService = urlMasterService;
            _urlClientService = urlClientService;
            _urlItinerarioService = urlItinerarioService;
            _urlCoreService = urlCoreService;
            _urlNaveService = urlNaveService;
            _urlFileService = urlFileService;
            _urlMailService = urlMailService;
            _azureBlobConnection = azureBlobConnection;
            _azureContainerBase = azureContainerBase;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpClientService>()
                   .As<IHttpClientService>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<MasterService>()
               .AsSelf()
               .AsImplementedInterfaces()
               .WithParameter("urlBase", _urlMasterService)
                .WithParameter("version", "v1");


            builder.RegisterType<CoreService>()
               .AsSelf()
               .AsImplementedInterfaces()
               .WithParameter("urlBase", _urlCoreService)
                .WithParameter("version", "v1");

            builder.RegisterType<FileService>()
               .AsSelf()
               .AsImplementedInterfaces()
               .WithParameter("urlBase", _urlFileService)
                .WithParameter("version", "v1");

            builder.RegisterType<MailService>()
               .AsSelf()
               .AsImplementedInterfaces()
               .WithParameter("urlBase", _urlMailService)
                .WithParameter("version", "v1");


            //builder.RegisterType<FileServiceAzureBlob>()
            //   .AsSelf()
            //   .AsImplementedInterfaces()
            //   .WithParameter("azureBlobConnection", _azureBlobConnection)
            //    .WithParameter("baseContainerApp", _azureContainerBase);

        }
    }
}
