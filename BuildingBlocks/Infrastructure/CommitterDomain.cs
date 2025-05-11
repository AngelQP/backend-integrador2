using Bigstick.BuildingBlocks.Application.Data;
using Bigstick.BuildingBlocks.ObjectStick.Hub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Infrastructure
{
    public class CommitterDomain : ICommitterDomain
    {

        private readonly IEntityHub _hubentity;
        private readonly ISqlTransaction _sqlTransaction;
        public CommitterDomain(
            IEntityHub hubentity
            , ISqlTransaction sqlTransaction)
        {
            this._hubentity = hubentity;
            
            this._sqlTransaction = sqlTransaction;
        }



        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await SaveChangesAsync(null, null, null, cancellationToken);
        }

        public async Task<int> SaveChangesAsync(Func<Task> commiting, CancellationToken cancellationToken = default)
        {
            return await SaveChangesAsync(commiting, null, null, cancellationToken);
        }

        public async Task<int> SaveChangesAsync(Func<Task> commiting, Func<Task> commited, CancellationToken cancellationToken = default)
        {
            return await SaveChangesAsync(commiting, commited, null, cancellationToken);
        }

        public async Task<int> SaveChangesAsync(Func<Task> commiting, Func<Task> commited, Func<Task> fail, CancellationToken cancellationToken = default)
        {
            if (_hubentity.HasEntries())
            {
                _sqlTransaction.BeginTransaction();

                try
                {
                    var _result = await _hubentity.SaveChangesAsync(cancellationToken);
                    
                    if (commiting != null) await commiting.Invoke();
                    
                    _sqlTransaction.DoTryCommit();

                    if (commited != null) await commited.Invoke();

                    _hubentity.Clear();
                    
                    return _result;
                }
                catch
                {
                    _sqlTransaction.DoTryRollback();
                    
                    if (fail != null) await fail.Invoke();

                    throw;
                }

            }
            return 0;
        }
    }
}
