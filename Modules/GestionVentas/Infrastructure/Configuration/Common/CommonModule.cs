using Autofac;
using Bigstick.BuildingBlocks.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ferreteria.Modules.GestionVentas.Infrastructure.Configuration.Common
{
   public class CommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Reporte>()
                .As<IReporte>()
                .InstancePerLifetimeScope();
        }
    }
}
