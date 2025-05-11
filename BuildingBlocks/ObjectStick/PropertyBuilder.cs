using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.ObjectStick
{
    public class PropertyBuilder<TDomain> where TDomain : class
    {
        TDomain _domain;
        private readonly IEnumerable<IValueConverter> valueConverters;
        Type _type;
        DomainBuilder _domainTypeBuilder;
        IDataReader _reader;
        internal PropertyBuilder(DomainBuilder domainTypeBuilder, IDataReader reader, TDomain domain, IEnumerable<IValueConverter> valueConverters)
        {
            _domain = domain;
            this.valueConverters = valueConverters;
            _reader = reader;
            _type = typeof(TDomain);
            _domainTypeBuilder = domainTypeBuilder;

        }

        public PropertyBuilder<TDomain> SetPropertyValue<TProperty>(Expression<Func<TDomain, TProperty>> propertyExpression, string columnName)
        {
            return SetPropertyValue(propertyExpression, (r) => r.DataReader.GetValue<TProperty>(columnName));
        }

        public PropertyBuilder<TDomain> SetPropertyValue<TProperty>(Expression<Func<TDomain, TProperty>> propertyExpression)
        {
            var propInfo = ObjectExtensions.GetPropertyInfo(propertyExpression);

            return SetPropertyValue(propInfo, (r) => r.DataReader.GetValue<TProperty>(propInfo.Name));
        }

        public PropertyBuilder<TDomain> SetPropertyValue<TProperty>(Expression<Func<TDomain, TProperty>> propertyExpression, Func<PropertyBuilder<TDomain>, TProperty> setter)
        {
            var propInfo = ObjectExtensions.GetPropertyInfo(propertyExpression);

            return SetPropertyValue(propInfo, setter);
        }

        public PropertyBuilder<TDomain> SetPropertyValue<TProperty>(string propertyName, string columnName)
        {
            return SetPropertyValue(propertyName, (r) => r.DataReader.GetValue<TProperty>(columnName));
        }

        public PropertyBuilder<TDomain> SetPropertyValue<TProperty>(string propertyName) => SetPropertyValue<TProperty>(propertyName, propertyName);

        public PropertyBuilder<TDomain> SetPropertyValue<TProperty>(string propertyName, Func<PropertyBuilder<TDomain>, TProperty> setter)
        {
            var member = _type.GetMemberInclPrivateBase(propertyName);
            return SetPropertyValue(member, setter);
        }

        private PropertyBuilder<TDomain> SetPropertyValue<TProperty>(MemberInfo memberInfo, Func<PropertyBuilder<TDomain>, TProperty> setter)
        {

            if (setter == null) throw new ArgumentNullException(nameof(setter));

            var value = setter.Invoke(this);
            
            TrySetValue(memberInfo, value);

            return this;
        }

        private PropertyBuilder<TDomain> SetPropertyValue(MemberInfo memberInfo, string columnName)
        {
            var value = _reader.GetValue(columnName);

            TrySetValue(memberInfo, value);

            return this;
        }
        public bool TrySetValue(MemberInfo memberInfo, object value) 
        {
            var typeMember = TypeExtensions.GetTypeMember(memberInfo);
            if (value != null && typeMember != null) 
            {
                if (typeMember.IsAssignableFrom(value.GetType()))
                {
                    _domain.SetMemberValue(memberInfo, value);
                    return true;
                }
                else 
                {
                    var converter = valueConverters.FirstOrDefault(x => x.IsApply(value.GetType(), typeMember));

                    if (converter != null)
                    {
                        var _value = converter.Create(value, typeMember);
                        _domain.SetMemberValue(memberInfo, _value);
                        return true;
                    }
                    else 
                    {
                        var constructor = typeMember.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { value.GetType() }, null);
                        if (constructor != null)
                        {
                            var _value  = constructor.Invoke(new object[] { value });
                            _domain.SetMemberValue(memberInfo, _value);
                            return true;
                        }
                    }
                }
            }
            return false;
            
        }
        public PropertyBuilder<TDomain> AddItems<TProperty, T>(Expression<Func<TDomain, TProperty>> propertyExpression, Func<PropertyBuilder<TDomain>, IEnumerable<T>> func)
        {
            _domain.AddItems(propertyExpression, func(this));

            return this;
        }
        
        public PropertyBuilder<TDomain> AddItems<T>(string propertyName, Func<PropertyBuilder<TDomain>, IEnumerable<T>> func)
        {
            _domain.AddItems(propertyName, func(this));

            return this;
        }
        public void AutoMap(Type attributeIgnore, StringComparison stringComparison) 
        {
            var members = _type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                .Where(x => ((x.MemberType == MemberTypes.Field && !((FieldInfo)x).IsInitOnly) ||
                                            (x.MemberType == MemberTypes.Property && ((PropertyInfo)x).CanWrite)) &&
                                            (attributeIgnore == null ||!x.GetCustomAttributes().Any(x=>x.GetType().Equals(attributeIgnore)))
                                       );

            var names = _reader.GetNames();
            foreach (var item in members)
            {
                var nameColumn = names.Where(x => x.Value.Equals(item.Name, stringComparison)).Select(x=>x.Value).FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(nameColumn))
                {
                    switch (item.MemberType)
                    {
                        case MemberTypes.Field:
                            SetPropertyValue(item, nameColumn);
                            break;
                        case MemberTypes.Property:
                            SetPropertyValue(item, nameColumn);
                            break;
                    }
                }
                
            }

        }
        public TDomain Domain => _domain;

        public IDataReader DataReader => _reader;

        public DomainBuilder DomainBuilder => _domainTypeBuilder;
    }
}
