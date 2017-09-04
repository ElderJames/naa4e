using Merp.Accountancy.CommandStack.Commands;
using Merp.Accountancy.CommandStack.Model;
using Merp.Accountancy.CommandStack.Services;
using Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memento.Persistence;
using Memento.Messaging.Postie;

namespace Merp.Accountancy.CommandStack.Sagas
{
    public class OutgoingInvoiceSaga : Saga,
        IAmStartedBy<IssueInvoiceCommand>
    {
        public IOutgoingInvoiceNumberGenerator InvoiceNumberGenerator { get; private set; }

        public OutgoingInvoiceSaga(IBus bus, IEventStore eventStore, IRepository repository, IOutgoingInvoiceNumberGenerator invoiceNumberGenerator)
            : base(bus, eventStore, repository)
        {
            if (invoiceNumberGenerator == null)
                throw new ArgumentNullException(nameof(invoiceNumberGenerator));

            InvoiceNumberGenerator = invoiceNumberGenerator;
        }

        public void Handle(IssueInvoiceCommand message)
        {
            var invoice = OutgoingInvoice.Factory.Create(
                this.InvoiceNumberGenerator,
                message.InvoiceDate,
                message.Amount,
                message.Taxes,
                message.TotalPrice,
                message.Description,
                message.PaymentTerms,
                message.PurchaseOrderNumber,
                message.Customer.Id,
                message.Customer.Name
                );
            this.Repository.Save(invoice);
        }
    }
}
