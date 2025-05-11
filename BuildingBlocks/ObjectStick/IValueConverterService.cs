using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.ObjectStick
{
    public interface IValueConverterService<TSource, TDestination>
    {
        TDestination Create(TSource source);

    }
}
