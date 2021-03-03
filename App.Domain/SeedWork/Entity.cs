using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Enums;

namespace App.Domain.SeedWork
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public RecordStatus RecordStatus { get; private set; }

        private List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void SetRecordStatus(RecordStatus recordStatus)
        {
            RecordStatus = recordStatus;
        }

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvent()
        {
            _domainEvents?.Clear();
        }

        public async Task CheckRule(IBusinessRule rule)
        {
            if (await rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}