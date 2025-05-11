using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.ObjectStick
{
    public interface IValueConverter
    {
        object Create(object value, Type modelType);

        bool IsApply(Type clrType, Type modelType);
    }
}
