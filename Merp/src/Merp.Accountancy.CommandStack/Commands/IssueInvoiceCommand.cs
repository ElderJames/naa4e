using Merp.Accountancy.CommandStack.Services;
using Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merp.Accountancy.CommandStack.Commands
{
    public class IssueInvoiceCommand : Command
    {
        public class CustomerInfo
        {
            public Guid Id { get; private set; }
            public string Name { get; private set; }
            public string StreetName { get; private set; }
            public string City { get; private set; }
            public string PostalCode { get; private set; }
            public string Country { get; private set; }
            public string VatIndex { get; private set; }
            public string NationalIdentificationNumber { get; private set; }

            public CustomerInfo(Guid customerId, string customerName, string streetName, string city, string postalCode, string country, string vatIndex, string nationalIdentificationNumber)
            {
                City = city;
                Name=customerName;
                Country = country;
                Id = customerId;
                NationalIdentificationNumber = nationalIdentificationNumber;
                PostalCode = postalCode;
                StreetName=streetName;
                VatIndex = vatIndex;
            }
        }

        public CustomerInfo Customer { get; private set; }
        public DateTime InvoiceDate { get; private set; }
        public decimal Amount { get; private set; }
        public decimal Taxes { get; private set; }
        public decimal TotalPrice { get; private set; }
        public string Description { get; private set; }
        public string PaymentTerms { get; private set; }
        public string PurchaseOrderNumber { get; private set; }

        public IssueInvoiceCommand(DateTime invoiceDate, decimal amount, decimal taxes, decimal totalPrice, string description, string paymentTerms, string purchaseOrderNumber, Guid customerId, string customerName, string streetName, string city, string postalCode, string country, string vatIndex, string nationalIdentificationNumber)
        {
            var customer = new CustomerInfo(
                city: city,
                customerName: customerName,
                country: country,
                customerId: customerId,
                nationalIdentificationNumber: nationalIdentificationNumber,
                postalCode: postalCode,
                streetName: streetName,
                vatIndex: vatIndex
            );
            Customer = customer;
            InvoiceDate = invoiceDate;
            Amount = amount;
            Taxes = taxes;
            TotalPrice = totalPrice;
            Description = description;
            PaymentTerms = paymentTerms;
            PurchaseOrderNumber = purchaseOrderNumber;
        }
    }
}
