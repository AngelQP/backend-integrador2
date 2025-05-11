using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Contract
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
        Guid Id { get; }
    }
}
