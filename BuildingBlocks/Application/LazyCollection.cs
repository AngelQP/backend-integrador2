using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Application
{
    public class LazyCollection<TKey, TValue>
    {
        Dictionary<TKey, TValue> counts;

        Func<IList<TKey>, IDictionary<TKey, TValue>> collect;

        IEqualityComparer<TKey> equalityComparer;
        public LazyCollection(Func<IList<TKey>, IDictionary<TKey, TValue>> collect, IEqualityComparer<TKey> equalityComparer) 
        {
            this.collect = collect;

            counts = new(equalityComparer);

            this.equalityComparer = equalityComparer;
        }

        public TValue GetValue(TKey key) 
        {
            var values =  GetValues(new TKey[] { key });
            if (values.Any()) return values.FirstOrDefault().Value;
            return default;
        }
        public Dictionary<TKey, TValue> GetValues(TKey[] keys)
        {
            Dictionary<TKey, TValue> result = new (equalityComparer);

            IList<TKey> outresult = new List<TKey>();

            foreach (var item in keys)
            {
                if (counts.ContainsKey(item))
                    result.Add(item, counts[item]);
                else
                {
                    if (!result.ContainsKey(item)) 
                    {
                        result.Add(item, default);
                        outresult.Add(item);
                    }
                    
                }
            }
            if (outresult.Any()) 
            {
                var resolvedValues = collect.Invoke(outresult);

                foreach (var v in resolvedValues)
                {
                    result[v.Key] = v.Value;
                    counts.Add(v.Key, v.Value);
                }
            }

            return result;
        }
    }
}
