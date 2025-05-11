using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferreteria.Modules.GestionVentas.Application.Contract;

namespace Ferreteria.Modules.GestionVentas.Application.Configuration.Command
{
    public interface ICommandHandler<in TCommand, TResult> :
       IRequestHandler<TCommand, TResult>
       where TCommand : ICommand<TResult>
    {
    }
}
