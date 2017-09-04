using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memento;
using Memento.Domain;

namespace Merp.Accountancy.CommandStack.Events
{
    public class TimeAndMaterialJobOrderRegisteredEvent : DomainEvent
    {
        public Guid JobOrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid ManagerId { get; private set; }
        public decimal Value { get; private set; }
        public string Currency { get; private set; }
        [Timestamp]
        public DateTime DateOfStart { get; private set; }
        public DateTime? DateOfExpiration { get; private set; }
        public string JobOrderName { get; private set; }
        public string JobOrderNumber { get; set; }
        public string PurchaseOrderNumber { get; private set; }
        public string Description { get; private set; }

        public TimeAndMaterialJobOrderRegisteredEvent(Guid jobOrderId, Guid customerId, Guid managerId, decimal value, string currency, DateTime dateOfStart, DateTime? dateOfExpiration, string jobOrderName, string jobOrderNumber, string purchaseOrderNumber, string description)
        {
            JobOrderId = jobOrderId;
            CustomerId = customerId;
            ManagerId = managerId;
            Value = value;
            Currency = currency;
            DateOfStart = dateOfStart;
            DateOfExpiration = dateOfExpiration;
            JobOrderName = jobOrderName;
            JobOrderNumber = jobOrderNumber;
            PurchaseOrderNumber = purchaseOrderNumber;
            Description = description;
        }
    }
}
