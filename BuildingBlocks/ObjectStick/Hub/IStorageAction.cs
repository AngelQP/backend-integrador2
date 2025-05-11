using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.ObjectStick.Hub
{
    public interface IStorageAction<TEntity>
        where TEntity : class
    {
        Task Execute(IEnumerable<EntityEntry<TEntity>> entities, CancellationToken cancellationToken = default);
    }
}
