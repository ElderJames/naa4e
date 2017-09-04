using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memento;
using Memento.Domain;

namespace Merp.Registry.CommandStack.Events
{
    public class CompanyNameChangedEvent : DomainEvent
    {
        public Guid CompanyId { get; private set; }
        [Timestamp]
        public DateTime EffectiveDate { get; private set; }
        public string CompanyName { get; private set; }

        public CompanyNameChangedEvent(Guid companyId, string companyName, DateTime effectiveDate)
        {
            CompanyId = companyId;
            CompanyName = companyName;
            EffectiveDate = effectiveDate;
        }
    }
}
