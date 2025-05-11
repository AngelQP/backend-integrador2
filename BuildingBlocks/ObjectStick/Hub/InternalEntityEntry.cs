using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.ObjectStick.Hub
{
    internal class InternalEntityEntry
    {
        protected EntityState _state;
        protected string _mark;
        protected object _entity;

        internal InternalEntityEntry(object entity, EntityState state, string mark)
        {
            _state = state;
            _mark = mark;
            _entity = entity;
        }
        protected virtual object EntityObject => _entity;

        public virtual object Entity => EntityObject;

        public EntityState State => _state;

        public string Mark => _mark;

        public void SetState(EntityState state)
        {
            _state = state;
        }
    }
}
