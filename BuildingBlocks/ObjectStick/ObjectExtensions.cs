using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.ObjectStick
{
    public static class ObjectExtensions
    {
        public static T SetPropertyValue<T, TValue>(this T target, string propertyName, TValue value)
        {
            var member = typeof(T).GetMemberInclPrivateBase(propertyName);

            target.SetMemberValue(member, value);

            return target;
        }


        public static T SetPropertyValue<T, TValue>(this T target, Expression<Func<T, TValue>> propertyExpression, TValue value)
        {
            var member = GetMemeberInfo(propertyExpression);

            target.SetMemberValue(member, value);

            return target;
        }

        public static MemberInfo GetMemeberInfo<T, TValue>(Expression<Func<T, TValue>> propertyExpression)
        {
            MemberExpression member = propertyExpression.Body as MemberExpression;

            var unary = propertyExpression.Body as UnaryExpression;

            member = member ?? (unary != null ? unary.Operand as MemberExpression : null);

            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    propertyExpression.ToString()));

            return member.Member;
        }

        internal static PropertyInfo GetPropertyInfo<T, TValue>(Expression<Func<T, TValue>> propertyExpression)
        {
            var member = GetMemeberInfo(propertyExpression);

            PropertyInfo propInfo = member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    propertyExpression.ToString()));

            return propInfo;
        }
        internal static object GetPropertyValue<T>(this T target, MemberInfo memberInfo)
        {
            if (memberInfo == null) throw new ArgumentNullException(nameof(memberInfo));

            var field = memberInfo as FieldInfo;

            if (field != null) return field.GetValue(target);
            else
            {
                var property = memberInfo as PropertyInfo;
                if (property != null) return property.GetValue(target);

            }
            throw new NotImplementedException($" The member '{memberInfo.Name}' of type '{memberInfo.MemberType}' is not possible to get a value");
        }
        internal static void SetMemberValue<T, TValue>(this T target, MemberInfo memberInfo, TValue value)
        {
            if (memberInfo == null) throw new ArgumentNullException(nameof(memberInfo));

            var field = memberInfo as FieldInfo;

            if (field != null)
            {
                field.SetValue(target, value);
                return;
            } 
            else
            {
                var property = memberInfo as PropertyInfo;
                if (property != null) 
                {
                    property.SetValue(target, value);
                    return;
                } 

            }
            throw new NotImplementedException($" The member '{memberInfo.Name}' of type '{memberInfo.MemberType}' is not possible to set a value");

        }

        public static T Construct<T>(Type[] paramTypes, object[] paramValues)
        {
            return (T)Construct(typeof(T), paramTypes, paramValues);
        }

        public static object Construct(Type typeImplementation, Type[] paramTypes, object[] paramValues)
        {
            ConstructorInfo ci = typeImplementation.GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic,
                null, paramTypes, null);
            if (ci == null) 
            {
                throw new NotImplementedException("No valid constructor found");
            }
            return ci.Invoke(paramValues);
        }

    }
}
