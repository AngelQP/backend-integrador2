using Autofac;
using Ferreteria.Modules.GestionVentas.Application.Contract;
using Ferreteria.Modules.GestionVentas.Infrastructure;

namespace Ferreteria.GestionVentas.API.Modules
{
    public class GestionVentasAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GestionVentasModule>().As<IGestionVentasModule>().InstancePerLifetimeScope();
        }
    }
}
