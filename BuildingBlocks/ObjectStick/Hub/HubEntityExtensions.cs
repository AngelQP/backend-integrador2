using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.ObjectStick.Hub
{
    public static class HubEntityExtensions
    {
        public static IEnumerable<EntityEntry<TEntity>> Add<TEntity>(this IEntityHub db, params TEntity[] entities) where TEntity : class
        {
            return db.Add(null, entities);
        }
        public static IEnumerable<EntityEntry<TEntity>> Attach<TEntity>(this IEntityHub db, params TEntity[] entities) where TEntity : class
        {
            return db.Attach(null, entities);
        }
        public static IEnumerable<EntityEntry<TEntity>> Update<TEntity>(this IEntityHub db, params TEntity[] entities) where TEntity : class
        {
            return db.Update(null, entities);
        }
        public static IEnumerable<EntityEntry<TEntity>> Delete<TEntity>(this IEntityHub db, params TEntity[] entities) where TEntity : class
        {
            return db.Delete(null, entities);
        }
    }
}
