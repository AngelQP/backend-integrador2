using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.ObjectStick.Hub
{
    public interface ICollectorEntity<TEntity>
        where TEntity : class
    {
        IEnumerable Collect(IEnumerable<TEntity> entities);

    }
}
