using Bigstick.BuildingBlocks.Domain;
using Bigstick.BuildingBlocks.ObjectStick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Infrastructure.Converters
{
    public class ValueConverterTypedId : IValueConverter
    {
        
        public ValueConverterTypedId() 
        {
            
        }


        public object Create(object value, Type modelType)
        {
            var guid = value as Guid?;
            if (guid != null) 
            {
                return Activator.CreateInstance(modelType, guid.Value);
            }
            return null;
        }

        public bool IsApply(Type clrType, Type typeModel)
        {
            var isTypedIdValue = typeof(TypedIdValueBase).IsAssignableFrom(typeModel);

            return (clrType == typeof(Guid) || clrType == typeof(Guid?)) && isTypedIdValue;
         
        }
    }
}
