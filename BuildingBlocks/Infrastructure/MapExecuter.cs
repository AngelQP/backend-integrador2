using Bigstick.BuildingBlocks.ObjectStick.Hub;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Infrastructure
{
    public sealed class MapExecuter<T> where T:class
    {
        Dictionary<KeyGroup, Func<IEnumerable<EntityEntry<T>>, CancellationToken, Task>> map;

        Dictionary<EntityState, Func<IEnumerable<EntityEntry<T>>, CancellationToken, Task>> mapWithOutMark;

        public MapExecuter() 
        {
            map = new(new KeyGroupEqualityCompare());
            mapWithOutMark = new();
        }
        public MapExecuter<T> Map(EntityState state, Func<IEnumerable<EntityEntry<T>>, CancellationToken, Task> executor) 
        {
            if (!mapWithOutMark.ContainsKey(state)) 
            {
                mapWithOutMark.Add(state, executor);
            }
            return this;
        }

        public MapExecuter<T> Map(EntityState state, string mark, Func<IEnumerable<EntityEntry<T>>, CancellationToken, Task> executor)
        {
            if (string.IsNullOrWhiteSpace(mark)) throw new ArgumentNullException(nameof(mark));

            var key = new KeyGroup(state, mark);
            if (!map.ContainsKey(key))
            {
                map.Add(key, executor);
            }
            return this;
        }

        public async Task ExecuteAsync(IEnumerable<EntityEntry<T>> values, CancellationToken cancellationToken = default) 
        {
            List<EntityEntry<T>> others = new();
            foreach (var group in values.Where(x => !string.IsNullOrWhiteSpace(x.Mark)).GroupBy(x => new KeyGroup(x.State, x.Mark), new KeyGroupEqualityCompare()))
            {
                if (map.ContainsKey(group.Key))
                    await map[group.Key].Invoke(group, cancellationToken);
                else
                    others.AddRange(group);
            }
            foreach (var group in values.Where(x=> string.IsNullOrWhiteSpace(x.Mark)).GroupBy(x => x.State))
            { 
                if (mapWithOutMark.ContainsKey(group.Key))
                    await mapWithOutMark[group.Key].Invoke(group, cancellationToken);
            }

        }

        private class KeyGroup: Domain.ValueObject
        {
            public KeyGroup(EntityState state, string mark) 
            {
                State = state;
                Mark = mark;
            }
            public EntityState State { get; set; }
            public string Mark { get; set; }
        }
        private class KeyGroupEqualityCompare : IEqualityComparer<KeyGroup>
        {
            public bool Equals(KeyGroup b2, KeyGroup b1)
            {
                if (b2 == null && b1 == null)
                    return true;
                else if (b1 == null || b2 == null)
                    return false;
                else if (b1.State == b2.State && b1.Mark == b2.Mark)
                    return true;
                else
                    return false;
            }

            public int GetHashCode([DisallowNull] KeyGroup bx)
            {
                int hCode = (bx.Mark.GetHashCode() * 23) + bx.State.GetHashCode(); ;
                return hCode.GetHashCode();
            }
        }
    }
}
