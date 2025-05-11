using Autofac;
using Bigstick.BuildingBlocks.Application.Outbox;
using Bigstick.BuildingBlocks.Infrastructure;
using Bigstick.BuildingBlocks.Infrastructure.DomainEventsDispatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferreteria.Modules.GestionVentas.Infrastructure.Outbox;

namespace Ferreteria.Modules.GestionVentas.Infrastructure.Configuration.Processing.Outbox
{
    public class OutboxModule : Autofac.Module
    {
        public OutboxModule()
        {
            
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OutboxAccesorGestionVentas>().As<IOutbox>().InstancePerLifetimeScope();
            builder.RegisterType<DomainNotificationsMapper>()
                    .As<IDomainNotificationsMapper>()
                    .WithParameter("domainNotificationsMap", new BiDictionary<string, Type>())
                    .SingleInstance();
        }
    }
}
