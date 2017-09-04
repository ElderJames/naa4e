using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memento;
using Memento.Domain;

namespace Merp.Accountancy.CommandStack.Events
{
    public class FixedPriceJobOrderRegisteredEvent : DomainEvent
    {
        public Guid JobOrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid ManagerId { get; private set; }
        public decimal Price { get; private set; }
        public string Currency { get; private set; }
        [Timestamp]
        public DateTime DateOfStart { get; private set; }
        public DateTime DueDate { get; private set; }
        public string JobOrderName { get; private set; }
        public string JobOrderNumber { get; set; }
        public string PurchaseOrderNumber { get; private set; }
        public string Description { get; private set; }

        public FixedPriceJobOrderRegisteredEvent(Guid jobOrderId, Guid customerId, Guid managerId, decimal price, string currency, DateTime dateOfStart, DateTime dueDate, string jobOrderName, string jobOrderNumber, string purchaseOrderNumber, string description)
        {
            JobOrderId = jobOrderId;
            CustomerId = customerId;
            ManagerId = managerId;
            Price = price;
            Currency = currency;
            DateOfStart = dateOfStart;
            DueDate = dueDate;
            JobOrderName = jobOrderName;
            JobOrderNumber = jobOrderNumber;
            PurchaseOrderNumber = purchaseOrderNumber;
            Description = description;
        }
    }
}
