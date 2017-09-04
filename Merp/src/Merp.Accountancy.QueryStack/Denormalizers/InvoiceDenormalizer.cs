using Merp.Accountancy.CommandStack.Events;
using Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memento.Messaging.Postie;

namespace Merp.Accountancy.QueryStack.Denormalizers
{
    public class InvoiceDenormalizer :
        IHandleMessages<IncomingInvoiceLinkedToJobOrderEvent>,
        IHandleMessages<OutgoingInvoiceLinkedToJobOrderEvent>
    {
        public void Handle(IncomingInvoiceLinkedToJobOrderEvent message)
        {
            using(var ctx = new AccountancyContext())
            {
                var invoice = ctx.IncomingInvoices.Where(i => i.OriginalId == message.InvoiceId).Single();
                invoice.JobOrderId = message.JobOrderId;
                ctx.SaveChanges();
            }
        }

        public void Handle(OutgoingInvoiceLinkedToJobOrderEvent message)
        {
            using (var ctx = new AccountancyContext())
            {
                var invoice = ctx.OutgoingInvoices.Where(i => i.OriginalId == message.InvoiceId).Single();
                invoice.JobOrderId = message.JobOrderId;
                ctx.SaveChanges();
            }
        }
    }
}
