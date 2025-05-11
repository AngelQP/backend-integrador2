using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.ObjectStick.Hub
{
    public interface IEntityHub
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        IEnumerable<EntityEntry<TEntity>> Add<TEntity>(string mark, params TEntity[] entities) where TEntity : class;
        IEnumerable<EntityEntry<TEntity>> Attach<TEntity>(string mark, params TEntity[] entities) where TEntity : class;
        IEnumerable<EntityEntry<TEntity>> Update<TEntity>(string mark, params TEntity[] entities) where TEntity : class;
        IEnumerable<EntityEntry<TEntity>> Delete<TEntity>(string mark, params TEntity[] entities) where TEntity : class;
        IEnumerable<EntityEntry<TEntity>> ChangeTrackEntities<TEntity>() where TEntity : class;

        bool HasEntries();

        void Clear();
    }
}
