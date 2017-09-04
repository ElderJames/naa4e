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
    public class TimeAndMaterialJobOrderDenormalizer : 
        IHandleMessages<TimeAndMaterialJobOrderRegisteredEvent>, 
        IHandleMessages<TimeAndMaterialJobOrderExtendedEvent>,
        IHandleMessages<TimeAndMaterialJobOrderCompletedEvent>
    {
        public void Handle(TimeAndMaterialJobOrderRegisteredEvent message)
        {
            var timeAndMaterialJobOrder = new TimeAndMaterialJobOrder();
            timeAndMaterialJobOrder.OriginalId = message.JobOrderId;
            timeAndMaterialJobOrder.CustomerId = message.CustomerId;
            timeAndMaterialJobOrder.Description = message.Description;
            timeAndMaterialJobOrder.ManagerId = message.ManagerId;
            timeAndMaterialJobOrder.DateOfStart = message.DateOfStart;
            timeAndMaterialJobOrder.DateOfExpiration = message.DateOfExpiration;
            timeAndMaterialJobOrder.Name = message.JobOrderName;
            timeAndMaterialJobOrder.Number = message.JobOrderNumber;
            timeAndMaterialJobOrder.PurchaseOrderNumber = message.PurchaseOrderNumber;
            timeAndMaterialJobOrder.Currency = message.Currency;
            timeAndMaterialJobOrder.Value = message.Value;
            timeAndMaterialJobOrder.IsCompleted = false;
            timeAndMaterialJobOrder.IsTimeAndMaterial = true;
            timeAndMaterialJobOrder.IsFixedPrice = false;

            using(var db = new AccountancyContext())
            {
                db.JobOrders.Add(timeAndMaterialJobOrder);
                db.SaveChanges();
            }
        }

        public void Handle(TimeAndMaterialJobOrderExtendedEvent message)
        {
            using (var db = new AccountancyContext())
            {
                var jobOrder = db.JobOrders.OfType<TimeAndMaterialJobOrder>().Where(jo => jo.OriginalId == message.JobOrderId).Single();
                jobOrder.DateOfExpiration = message.NewDateOfExpiration;
                jobOrder.Value = message.Value;
                db.SaveChanges();
            }
        }

        public void Handle(TimeAndMaterialJobOrderCompletedEvent message)
        {
            using (var db = new AccountancyContext())
            {
                var jobOrder = db.JobOrders.OfType<TimeAndMaterialJobOrder>().Where(jo => jo.OriginalId == message.JobOrderId).Single();
                jobOrder.DateOfCompletion = message.DateOfCompletion;
                jobOrder.IsCompleted = true;
                db.SaveChanges();
            }
        }
    }
}
