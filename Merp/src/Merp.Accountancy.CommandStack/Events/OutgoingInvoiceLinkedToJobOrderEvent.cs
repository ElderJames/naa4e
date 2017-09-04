using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memento;
using Memento.Domain;

namespace Merp.Accountancy.CommandStack.Events
{
    public class OutgoingInvoiceLinkedToJobOrderEvent : DomainEvent
    {
        public Guid InvoiceId { get; private set; }

        public Guid JobOrderId { get; private set; }

        [Timestamp]
        public DateTime DateOfLink { get; private set; }

        public decimal Amount { get; private set; }

        public OutgoingInvoiceLinkedToJobOrderEvent(Guid invoiceId, Guid jobOrderId, DateTime dateOfLink, decimal amount)
        {
            InvoiceId = invoiceId;
            JobOrderId = jobOrderId;
            DateOfLink = dateOfLink;
            Amount = amount;
        }
    }
}
