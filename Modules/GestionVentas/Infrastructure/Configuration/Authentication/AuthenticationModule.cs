using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferreteria.Modules.GestionVentas.Domain.Users;
using Ferreteria.Modules.GestionVentas.Infrastructure.Configuration.Users;

namespace Ferreteria.Modules.GestionVentas.Infrastructure.Configuration.Authentication
{
    public class AuthenticationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BusinessUserContext>()
                .As<IUserContext>()
                .InstancePerLifetimeScope();

        }
    }
}
