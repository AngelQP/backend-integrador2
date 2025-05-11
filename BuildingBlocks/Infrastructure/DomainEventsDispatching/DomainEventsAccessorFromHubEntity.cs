using Bigstick.BuildingBlocks.Domain;
using Bigstick.BuildingBlocks.ObjectStick.Hub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    public class DomainEventsAccessorFromHubEntity : IDomainEventsAccessor
    {
        private readonly IEntityHub _entityhub;

        public DomainEventsAccessorFromHubEntity(IEntityHub entityhub) 
        {
            _entityhub = entityhub;
        }
        public void ClearAllDomainEvents()
        {
            var domainEntities = _entityhub.ChangeTrackEntities<Entity>()
               .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            domainEntities
                .ForEach(entity => entity.Entity.ClearDomainEvents());
        }

        public IReadOnlyCollection<IDomainEvent> GetAllDomainEvents()
        {
            var domainEntities = _entityhub.ChangeTrackEntities<Entity>()
                 .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();
            return domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();
        }
    }
}
