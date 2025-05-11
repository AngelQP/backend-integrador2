using Autofac;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Infrastructure.Configuration.Mediation
{
    internal class MediatRModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly)
              .AsImplementedInterfaces()
              .InstancePerLifetimeScope();

            var openHandlerTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(IRequestExceptionHandler<,,>),
                typeof(IRequestExceptionAction<,>),
                typeof(INotificationHandler<>),
                typeof(IValidator<>)
            };

            foreach (var i in openHandlerTypes)
            {
                builder.RegisterAssemblyTypes(Assemblies.Application)
                    .AsClosedTypesOf(i)
                    .AsImplementedInterfaces();
            }

            builder.Register<ServiceFactory>(outerContext =>
            {
                var innerContext = outerContext.Resolve<IComponentContext>();
                return serviceType => innerContext.Resolve(serviceType);
            }).InstancePerLifetimeScope();


        }
    }
}
