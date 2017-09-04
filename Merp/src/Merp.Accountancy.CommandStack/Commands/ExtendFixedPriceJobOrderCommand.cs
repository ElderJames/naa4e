using Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merp.Accountancy.CommandStack.Commands
{
    public class ExtendFixedPriceJobOrderCommand : Command
    {
        public Guid JobOrderId { get; private set; }
        public DateTime NewDueDate { get; private set; }
        public decimal Price { get; private set; }

        public ExtendFixedPriceJobOrderCommand(Guid jobOrderId, DateTime newDueDate, decimal price)
        {
            JobOrderId = jobOrderId;
            NewDueDate = newDueDate;
            Price = price;
        }
    }
}
