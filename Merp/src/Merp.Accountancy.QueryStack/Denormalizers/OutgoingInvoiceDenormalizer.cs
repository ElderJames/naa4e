using Merp.Accountancy.CommandStack.Events;
using Merp.Accountancy.QueryStack.Model;
using Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memento.Messaging.Postie;

namespace Merp.Accountancy.QueryStack.Denormalizers
{
    public class OutgoingInvoiceDenormalizer :
        IHandleMessages<OutgoingInvoiceIssuedEvent>
    {
        public void Handle(OutgoingInvoiceIssuedEvent message)
        {
            var invoice = new OutgoingInvoice();
            invoice.Amount = message.Amount;
            invoice.Date = message.InvoiceDate;
            invoice.Description = message.Description;
            invoice.Number = message.InvoiceNumber;
            invoice.OriginalId = message.InvoiceId;
            invoice.PurchaseOrderNumber = message.PurchaseOrderNumber;
            invoice.Taxes = message.Taxes;
            invoice.TotalPrice = message.TotalPrice;
            invoice.Customer = new Invoice.PartyInfo()
                                {
                                    City = message.Customer.City,
                                    Country = message.Customer.Country,
                                    Name = message.Customer.Name,
                                    NationalIdentificationNumber = message.Customer.NationalIdentificationNumber,
                                    OriginalId = message.Customer.Id,
                                    PostalCode = message.Customer.PostalCode,
                                    StreetName = message.Customer.StreetName,
                                    VatIndex = message.Customer.VatIndex
                                };
            using(var ctx = new AccountancyContext())
            {
                ctx.OutgoingInvoices.Add(invoice);
                ctx.SaveChanges();
            }
        }
    }
}
