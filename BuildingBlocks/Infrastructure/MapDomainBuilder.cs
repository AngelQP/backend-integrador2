using Bigstick.BuildingBlocks.Infrastructure.Converters;
using Bigstick.BuildingBlocks.ObjectStick;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Infrastructure
{
    public static class MapDomainBuilder
    {
        public static DomainBuilder CreateBuilder() 
        {
            return new(new IValueConverter[] { new ValueConverterTypedId() });
        }
        public static IEnumerable<T> MapToDomain<T>(this IDataReader dataReader)where T:class
        {
            return CreateBuilder().CreateCollection<T>(dataReader);
        }

        public static IEnumerable<T> MapToDomain<T>(this IDataReader dataReader, Action<PropertyBuilder<T>> builder) where T : class
        {
            return CreateBuilder().CreateCollection<T>(dataReader, builder);
        }

        public static async Task<IEnumerable<T>> MapToDomainAsync<T>(this Task<IDataReader> dataReader) where T : class
        {
            return CreateBuilder().CreateCollection<T>(await dataReader);
        }

        public static async Task<IEnumerable<T>> MapToDomainAsync<T>(this Task<IDataReader> dataReader, Action<PropertyBuilder<T>> builder) where T : class
        {
            return CreateBuilder().CreateCollection(await dataReader, builder);
        }
    }
}
