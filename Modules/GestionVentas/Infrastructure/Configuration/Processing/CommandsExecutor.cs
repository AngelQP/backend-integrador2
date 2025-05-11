using Autofac;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferreteria.Modules.GestionVentas.Application.Contract;

namespace Ferreteria.Modules.GestionVentas.Infrastructure.Configuration.Processing
{
    internal static class CommandsExecutor
    {
        internal static async Task<TResult> Execute<TResult>(ICommand<TResult> command)
        {
            using (var scope = GestionVentasCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                return await mediator.Send(command);
            }
        }
    }
}
