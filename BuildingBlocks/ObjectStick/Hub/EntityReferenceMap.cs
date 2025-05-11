using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.ObjectStick.Hub
{
    internal class EntityReferenceMap : IDisposable
    {
        private readonly FactoryProvider _factoryProvider;

        private List<EntityEntry> collectData;

        private IDictionary<object, InternalEntityEntry> data;

        bool _isCollected;
        internal EntityReferenceMap(FactoryProvider factoryProvider)
        {
            _factoryProvider = factoryProvider;
            data = new Dictionary<object, InternalEntityEntry>();
            collectData = new List<EntityEntry>();
        }
        internal bool IsCollected
        {
            get => _isCollected;
            set
            {
                if (!_isCollected) collectData.Clear();
                _isCollected = value;
            }
        }
        internal IDictionary<object, InternalEntityEntry> Data => data;

        public InternalEntityEntry GetOrAddEntry(InternalEntityEntry internalEntry) 
        {
            InternalEntityEntry _entity;

            if (!data.TryGetValue(internalEntry.Entity, out _entity))
            {
                data.Add(internalEntry.Entity, internalEntry);
                
            }

            return _entity ?? internalEntry;
        }
        internal IEnumerable<EntityEntry<T>> GetEntities<T>() where T : class
        {
            foreach (var item in GetAllEntities().Where(x => x.Item1.Entity is T).GroupBy(x => x.Item1.Entity))
            {
                var internalEntry = item.Where(x => x.Item2).FirstOrDefault() ?? item.Where(x => !x.Item2).FirstOrDefault();

                yield return new EntityEntry<T>(internalEntry.Item1);
            }
        }
        internal IEnumerable<EntityEntry> GetEntities()
        {
            foreach (var item in GetAllEntities().GroupBy(x => x.Item1.Entity))
            {
                var internalEntry = item.Where(x => x.Item2).FirstOrDefault() ?? item.Where(x => !x.Item2).FirstOrDefault();
                yield return EntityEntry.Create(internalEntry.Item1);
            }
        }

        private IEnumerable<Tuple<InternalEntityEntry, bool>> GetAllEntities()
        {

            foreach (var item in data.GroupBy(x => x.Key.GetType()))
            {
                var typeGenericCollect = typeof(ICollectorEntity<>);
                var typeCollect = typeGenericCollect.MakeGenericType(item.Key);
                var collector = _factoryProvider.Invoke(typeCollect);
                if (collector != null)
                {
                    var collectMethod = typeCollect.GetMethod("Collect");
                    var dataitems = (IEnumerable)collectMethod.Invoke(collector, new object[] { item.Select(x => x) });
                    foreach (var ditem in dataitems) yield return new(new InternalEntityEntry(ditem, EntityState.Detached, null), false);
                }
                foreach (var entity in item) yield return new(entity.Value, true);
            }
        }

        public void Dispose()
        {
            data.Clear();
        }

    }
}
