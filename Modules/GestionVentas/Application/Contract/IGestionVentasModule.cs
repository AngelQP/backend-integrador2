using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ferreteria.Modules.GestionVentas.Application.Contract;

namespace Ferreteria.Modules.GestionVentas.Application.Contract
{
    public interface IGestionVentasModule
    {
        Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);

        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}
