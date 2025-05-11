using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Application.Data
{
    public interface ICommitterDomain
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);


        Task<int> SaveChangesAsync(Func<Task> commiting, CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(Func<Task> commiting, Func<Task> commited, CancellationToken cancellationToken = default);

        
        Task<int> SaveChangesAsync(Func<Task> commiting, Func<Task> commited, Func<Task> fail, CancellationToken cancellationToken = default);
    }
}
