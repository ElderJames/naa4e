using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memento;

namespace Merp.Accountancy.CommandStack.Commands
{
    public sealed class RegisterFixedPriceJobOrderCommand : Command
    {
        public Guid CustomerId { get; private set; }
        public Guid ManagerId { get; private set; }
        public decimal Price { get; private set; }
        public string Currency { get; private set; }
        public DateTime DateOfStart { get; private set; }
        public DateTime DueDate { get; private set; }
        public string JobOrderName { get; private set; }
        public string PurchaseOrderNumber { get; private set; }
        public string Description { get; private set; }

        public RegisterFixedPriceJobOrderCommand(Guid customerId, Guid managerId, decimal price, string currency, DateTime dateOfStart, DateTime dueDate, string jobOrderName, string purchaseOrderNumber, string description)
        {
            CustomerId = customerId;
            ManagerId = managerId;
            Price = price;
            Currency = currency;
            DateOfStart = dateOfStart;
            DueDate = dueDate;
            JobOrderName = jobOrderName;
            PurchaseOrderNumber = purchaseOrderNumber;
            Description = description;
        }
    }
}
