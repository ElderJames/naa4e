using Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merp.Registry.CommandStack.Events
{
    public class CompanyRegisteredEvent : DomainEvent
    {
        public Guid CompanyId { get; private set; }
        public string CompanyName { get; private set; }
        public string VatIndex { get; private set; }

        public CompanyRegisteredEvent(Guid companyId, string companyName, string vatIndex)
        {
            CompanyId = companyId;
            CompanyName = companyName;
            VatIndex = vatIndex;
        }
    }
}
