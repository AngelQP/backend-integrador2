﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Domain
{
    public abstract class Entity
    {
        private List<IDomainEvent> _domainEvents;

        /// <summary>
        /// Domain events occurred.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        /// <summary>
        /// Add domain event.
        /// </summary>
        /// <param name="domainEvent">Domain event.</param>
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents ??= new List<IDomainEvent>();

            this._domainEvents.Add(domainEvent);
        }

        protected void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}
