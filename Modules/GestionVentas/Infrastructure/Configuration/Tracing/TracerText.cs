using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Infrastructure.Configuration.Tracing
{
    public class TracerText : Autofac.Module
    {
        public TracerText()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<TracerText>()
            //        .As<ITraceText>()
            //        .InstancePerLifetimeScope()
            //        .WithParameter("connectionString", _connectionString);
        }
    }
}
