using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.ObjectStick.Hub
{
    public class EntityHub : IEntityHub, IDisposable
    {

        private readonly FactoryProvider factoryProvider;

        private bool _disposed = false;


        private Lazy<EntityReferenceMap> instanceEntityReferenceMap;
        public EntityHub(FactoryProvider factoryProvider)
        {

            this.factoryProvider = factoryProvider;
            instanceEntityReferenceMap = new Lazy<EntityReferenceMap>(() => new(this.factoryProvider));
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var typeEntity in instanceEntityReferenceMap.Value.GetEntities().GroupBy(x => x.Entity.GetType()))
            {
                var type = typeEntity.Key;
                await ExecuteAction(() => ExecuteAction<object>, type, typeEntity.Select(x => x), cancellationToken);
            }
            return 1;
        }

        private delegate Task Execute(IEnumerable<EntityEntry> entities,
                            CancellationToken cancellationToken = default);

        private async Task ExecuteAction<T>(IEnumerable<EntityEntry> entities,
                            CancellationToken cancellationToken = default) where T : class
        {
            var storageAction = factoryProvider(typeof(IStorageAction<T>)) as IStorageAction<T>;
            if (storageAction != null)

                await storageAction.Execute(entities.Cast<EntityEntry<T>>(), cancellationToken);

        }

        private async Task ExecuteAction(
                            Expression<Func<Execute>> expression,
                            Type type,
                            IEnumerable<EntityEntry> entities,
                            CancellationToken cancellationToken = default)
        {


            var methodexpression = (expression.Body as UnaryExpression).Operand as MethodCallExpression;

            var cExpression = methodexpression.Object as ConstantExpression;

            var methodinfo = cExpression.Value as MethodInfo;

            var methodInfoGeneric = methodinfo.GetGenericMethodDefinition();

            var oMehotdInfo = methodInfoGeneric.MakeGenericMethod(type);

            var result = (Task)oMehotdInfo.Invoke(this, new object[] { entities, cancellationToken });

            await result.ConfigureAwait(false);
        }



        private IEnumerable<EntityEntry<TEntity>> SetEntityState<TEntity>(
            TEntity[] entities,
            string mark,
            EntityState entityState) where TEntity : class
        {
            List<EntityEntry<TEntity>> entries = new List<EntityEntry<TEntity>>();
            foreach (var entity in entities)
            {
                SetEntityState(entity, mark, entityState);
            }
            return entries;
        }


        public EntityEntry<TEntity> SetEntityState<TEntity>(
            TEntity entity,
            string mark,
            EntityState entityState) where TEntity : class
        {
            CheckDisposed();

            var entry = EntryWithoutDetectChanges(entity, mark);

            SetEntityState(entry, entityState);

            return entry;
        }

        private void SetEntityState<TEntity>(EntityEntry<TEntity> entity, EntityState entityState) where TEntity : class
        {
            entity.SetState(entityState);
        }

        private EntityEntry<TEntity> EntryWithoutDetectChanges<TEntity>(TEntity entity, string mark) where TEntity : class
        {

            var data = this.instanceEntityReferenceMap.Value.Data;

            var internalEntry = new InternalEntityEntry(entity, EntityState.Unchanged, mark);

            var entry = this.instanceEntityReferenceMap.Value.GetOrAddEntry(internalEntry);

            return new EntityEntry<TEntity>(entry);

        }
        public void Dispose()
        {

            _disposed = true;
            instanceEntityReferenceMap.Value.Dispose();
        }

        private void CheckDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name, "Disposed Context");
            }
        }



        public IEnumerable<EntityEntry<TEntity>> Add<TEntity>(string mark, params TEntity[] entities) where TEntity : class
        {
            return SetEntityState(entities, mark, EntityState.Added);
        }

        public IEnumerable<EntityEntry<TEntity>> Attach<TEntity>(string mark, params TEntity[] entities) where TEntity : class
        {
            return SetEntityState(entities, mark, EntityState.Unchanged);
        }

        public IEnumerable<EntityEntry<TEntity>> Update<TEntity>(string mark, params TEntity[] entities) where TEntity : class
        {
            return SetEntityState(entities, mark, EntityState.Modified);
        }

        public IEnumerable<EntityEntry<TEntity>> Delete<TEntity>(string mark, params TEntity[] entities) where TEntity : class
        {
            return SetEntityState(entities, mark, EntityState.Deleted);
        }

        public IEnumerable<EntityEntry<TEntity>> ChangeTrackEntities<TEntity>() where TEntity : class
        {
            return instanceEntityReferenceMap.Value.GetEntities<TEntity>();
        }

        public bool HasEntries()
        {
            return instanceEntityReferenceMap.Value.GetEntities().Any();
        }

        public void Clear()
        {
            instanceEntityReferenceMap.Value.Data.Clear();
        }
    }
}
