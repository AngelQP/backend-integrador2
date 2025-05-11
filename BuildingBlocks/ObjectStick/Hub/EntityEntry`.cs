using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.ObjectStick.Hub
{
    public class EntityEntry<TEntity> : EntityEntry where TEntity : class
    {
        private readonly TEntity entity;
        internal EntityEntry(InternalEntityEntry internalEntityEntry) : base(internalEntityEntry)
        {
            this.entity = (TEntity)internalEntityEntry.Entity;
        }

        public new virtual TEntity Entity => entity;

        protected override object EntityObject => this.entity;
    }
}
