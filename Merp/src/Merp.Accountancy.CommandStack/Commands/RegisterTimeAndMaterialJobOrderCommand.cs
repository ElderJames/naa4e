using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memento;

namespace Merp.Accountancy.CommandStack.Commands
{
    public sealed class RegisterTimeAndMaterialJobOrderCommand : Command
    {
        public Guid CustomerId { get; private set; }
        public Guid ManagerId { get; private set; }
        public decimal Value { get; private set; }
        public string Currency { get; private set; }
        public DateTime DateOfStart { get; private set; }
        public DateTime? DateOfExpiration { get; private set; }
        public string JobOrderName { get; private set; }
        public string PurchaseOrderNumber { get; private set; }
        public string Description { get; private set; }

        public RegisterTimeAndMaterialJobOrderCommand(Guid customerId, Guid managerId, decimal value, string currency, DateTime dateOfStart, DateTime? dateOfExpiration, string jobOrderName, string purchaseOrderNumber, string description)
        {
            CustomerId = customerId;
            ManagerId = managerId;
            Value = value;
            Currency = currency;
            DateOfStart = dateOfStart;
            DateOfExpiration = dateOfExpiration;
            JobOrderName = jobOrderName;
            PurchaseOrderNumber = purchaseOrderNumber;
            Description = description;
        }
    }
}
