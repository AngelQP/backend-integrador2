using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.ObjectStick.Hub
{
    public abstract class EntityEntry
    {
        internal InternalEntityEntry _internalEntityEntry;

        internal EntityEntry(InternalEntityEntry internalEntityEntry)
        {
            _internalEntityEntry = internalEntityEntry;
        }
        protected abstract object EntityObject { get; }

        public virtual object Entity => EntityObject;

        public EntityState State => _internalEntityEntry.State;

        public string Mark => _internalEntityEntry.Mark;

        public void SetState(EntityState state)
        {
            _internalEntityEntry.SetState(state);
        }

        internal static Type TypeEntityEntry(Type type)
        {
            var _type = typeof(EntityEntry<>);

            return _type.MakeGenericType(type);
        }
        internal static EntityEntry Create(InternalEntityEntry internalEntityEntry)
        {
            var entryType = TypeEntityEntry(internalEntityEntry.Entity.GetType());

            return (EntityEntry)ObjectExtensions.Construct(entryType, new Type[] { typeof(InternalEntityEntry) }, new object[] { internalEntityEntry });
        }

        internal static IEnumerable Create(IEnumerable<InternalEntityEntry> internalEntityEntry)
        {
            foreach (var item in internalEntityEntry)
            {
                yield return Create(item);
            }
        }

        internal static IEnumerable Create(Type type, IEnumerable<InternalEntityEntry> internalEntityEntry)
        {
            var castMethod = typeof(Enumerable).GetMethod("Cast", BindingFlags.Static | BindingFlags.Public);

            var caxtGenericMethod = castMethod.MakeGenericMethod(new Type[] { TypeEntityEntry(type) });

            return (IEnumerable)caxtGenericMethod.Invoke(null, new object[] { Create(internalEntityEntry) });

        }


    }
}
