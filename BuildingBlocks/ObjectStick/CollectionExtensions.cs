using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.ObjectStick
{
    public static class CollectionExtensions
    {
        public static T AddItems<T, TProperty, TValue>(this T target, Expression<Func<T, TProperty>> propertyExpression, IEnumerable<TValue> values)
        {
            var member = ObjectExtensions.GetPropertyInfo(propertyExpression);

            AddItemToProperty(target, member, values);

            return target;
        }

        public static T AddItems<T, TValue>(this T target, string propertyName, IEnumerable<TValue> values)
        {
            var member = typeof(T).GetMemberInclPrivateBase(propertyName);

            AddItemToProperty(target, member, values);

            return target;
        }

        internal static void AddItemToProperty<T, TValue>(T target, MemberInfo member, IEnumerable<TValue> values)
        {

            var collection = target.GetPropertyValue(member) as ICollection<TValue>;

            if (collection != null)
            {
                foreach (var item in values)
                    collection.Add(item);
            }
            else throw new NotImplementedException($"The property {member.Name} not implement ICollection interface");

        }

    }
}
