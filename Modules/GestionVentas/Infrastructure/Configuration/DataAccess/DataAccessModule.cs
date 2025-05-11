using Autofac;
using Bigstick.BuildingBlocks.Application.Data;
using Bigstick.BuildingBlocks.Infrastructure;
using Bigstick.BuildingBlocks.ObjectStick.Hub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferreteria.Modules.GestionVentas.Application.Data;

namespace Ferreteria.Modules.GestionVentas.Infrastructure.Configuration.DataAccess
{
    internal class DataAccessModule : Autofac.Module
    {
        private string _connectionString;
        private string _tramarsaConnectionString;
        public DataAccessModule(string connectionString, string tramarsaConnectionString)
        {
            _connectionString = connectionString;
            _tramarsaConnectionString = tramarsaConnectionString;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MsSqlConnectionFactory>().AsSelf().As<ISqlConnectionFactory>().WithParameter("connectionString",_connectionString).InstancePerLifetimeScope();

            builder.RegisterType<MSqlTramarsaConnectionFactory>().AsSelf().As<ITramarsaConnectionFactory>().WithParameter("connectionString", _tramarsaConnectionString).InstancePerLifetimeScope();

            builder.RegisterType<EntityHub>().As<IEntityHub>().InstancePerLifetimeScope();
            builder.RegisterType<CommitterDomain>().As<ICommitterDomain>().InstancePerLifetimeScope();
            builder.RegisterType<MsSqlTransaction>().As<ISqlTransaction>().InstancePerLifetimeScope();
            builder.Register<FactoryProvider>(outerContext =>
            {
                var innerContext = outerContext.Resolve<IComponentContext>();
                return serviceType => innerContext.ResolveOptional(serviceType);
            });
            
            var infrastructureAssembly = typeof(DataAccessModule).Assembly;
            builder.RegisterAssemblyTypes(infrastructureAssembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(infrastructureAssembly)
                .Where(type => type.Name.EndsWith("StorageAction"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

        }

    }
}
