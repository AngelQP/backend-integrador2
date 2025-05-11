using Autofac;
using Bigstick.BuildingBlocks.EventBus.AzureServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Infrastructure.Configuration.Queue
{
    public class AzureServiceBus : Autofac.Module
    {
        private readonly string _connectionString;

        public AzureServiceBus(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServiceBus>()
                    .As<IServiceBus>()
                    .InstancePerLifetimeScope()
                    .WithParameter("connectionString", _connectionString);
        }
    }
}
