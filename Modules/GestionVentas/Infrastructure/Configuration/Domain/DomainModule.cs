using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferreteria.Modules.GestionVentas.Application.Servicios;
using Ferreteria.Modules.GestionVentas.Domain.Servicios;

namespace Ferreteria.Modules.GestionVentas.Infrastructure.Configuration.Domain
{
    public class DomainModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<KNService>()
            //    .As<IKNService>()
            //    .InstancePerLifetimeScope();

            builder.RegisterType<CommonService>()
                .As<ICommonService>()
                .InstancePerLifetimeScope();
        }
    }
}
