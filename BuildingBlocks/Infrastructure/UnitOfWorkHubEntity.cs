using Bigstick.BuildingBlocks.Application.Data;
using Bigstick.BuildingBlocks.Infrastructure.DomainEventsDispatching;
using Bigstick.BuildingBlocks.ObjectStick.Hub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Infrastructure
{
    public class UnitOfWorkHubEntity : IUnitOfWork
    {
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;
        private readonly ICommitterDomain committerDomain;

        public UnitOfWorkHubEntity(
            IDomainEventsDispatcher domainEventsDispatcher,
            ICommitterDomain committerDomain)
        {
            this._domainEventsDispatcher = domainEventsDispatcher;
            this.committerDomain = committerDomain;
            
        }

        public async Task<int> CommitAsync(
            CancellationToken cancellationToken = default,
            Guid? internalCommandId = null)
        {
            await this._domainEventsDispatcher.DispatchEventsAsync();

            return await SaveChangesAsync();
        }
        private async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
        {
            return await committerDomain.SaveChangesAsync(cancellationToken);
        }
    }
}
