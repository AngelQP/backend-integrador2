using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Application.Domain.ReaderCsv
{
    public class PopulaterProperties<T>
    {
        private readonly T data;
        private readonly string[] recordItem;
        private readonly IEnumerable<ItemSequenceField> sequence;
        public PopulaterProperties(T data,
            string[] recordItem,
            IEnumerable<ItemSequenceField> sequence)
        {
            this.data = data;
            this.recordItem = recordItem;
            this.sequence = sequence;
        }
        public PopulaterProperties<T> SetValue(
            Expression<Func<T, object>> expression)
        {
            var propertyName = GetPropertyName(expression);

            var item = sequence.Where(x => x.Field.PropertyName.Equals(propertyName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (item != null)
            {
                var value = GetValue(recordItem, item.Index);
                if (value != null) SetValue(propertyName, value);
            }

            return this;
        }

        private void SetValue(string propertyName, object value)
        {
            var type = data.GetType();
            type.GetProperty(propertyName).SetValue(data, value);
        }
        private object GetValue(string[] recordItem, int index)
        {
            if (recordItem.Length - 1 >= index)
                return recordItem[index];
            return null;
        }
        private string GetPropertyName(Expression<Func<T, object>> expression)
        {
            Type type = typeof(T);

            MemberExpression member = expression.Body as MemberExpression;

            var unary = expression.Body as UnaryExpression;

            member = member ?? (unary != null ? unary.Operand as MemberExpression : null);

            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    expression.ToString()));

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    expression.ToString()));

            if (type != propInfo.ReflectedType &&
                !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(string.Format(
                    "Expresion '{0}' refers to a property that is not from type {1}.",
                    expression.ToString(),
                    type));

            return propInfo.Name;
        }


    }
    public class PopulaterProperties
    {
        public static PopulaterProperties<T> CreatePopulater<T>(
            T data,
            string[] recordItem,
            IEnumerable<ItemSequenceField> sequence)
        {
            return new PopulaterProperties<T>(data, recordItem, sequence);
        }
    }
}
