using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.ObjectStick
{
    public class DomainBuilder
    {
        private readonly IEnumerable<IValueConverter> valueConverters;

        public DomainBuilder(IEnumerable<IValueConverter> valueConverters)
        {
            this.valueConverters = valueConverters;
        }
        public PropertyBuilder<TDomain> Create<TDomain>(IDataReader _reader) where TDomain : class
        {
            var _domain = ObjectExtensions.Construct<TDomain>(Array.Empty<Type>(), Array.Empty<object>());

            return new PropertyBuilder<TDomain>(this, _reader, _domain, valueConverters);
        }
        //, Func<PropertyBuilder<TDomain>> factory
        public IEnumerable<TDomain> CreateCollection<TDomain>(IDataReader _reader) where TDomain : class
        {
            List<TDomain> data = new();
            while (_reader.Read())
            {
                var propertyBuilder = Create<TDomain>(_reader);
                propertyBuilder.AutoMap(null, StringComparison.OrdinalIgnoreCase);
                data.Add(propertyBuilder.Domain);
            }
            return data;
        }
        public IEnumerable<TDomain> CreateCollection<TDomain>(IDataReader _reader, Action<PropertyBuilder<TDomain>> builder) where TDomain : class
        {
            List<TDomain> data = new ();
            while (_reader.Read()) 
            {
                var propertyBuilder = Create<TDomain>(_reader);
                propertyBuilder.AutoMap(null, StringComparison.OrdinalIgnoreCase);
                builder.Invoke(propertyBuilder);
                data.Add(propertyBuilder.Domain);
            }
            return data;
        }

        public PropertyBuilder<TDomain> Create<TDomain>(IDataReader _reader, TDomain domain) where TDomain : class
        {
            return new PropertyBuilder<TDomain>(this, _reader, domain, valueConverters);
        }

        public IEnumerable<TDomain> CreateCollection<TDomain>(IDataReader _reader, Func<IDataReader, TDomain> createrDomain, Action<PropertyBuilder<TDomain>> builder) where TDomain : class
        {
            List<TDomain> data = new();
            while (_reader.Read())
            {
                var propertyBuilder = Create(_reader, createrDomain.Invoke(_reader));
                builder.Invoke(propertyBuilder);
                data.Add(propertyBuilder.Domain);
            }
            return data;
        }


    }
}
