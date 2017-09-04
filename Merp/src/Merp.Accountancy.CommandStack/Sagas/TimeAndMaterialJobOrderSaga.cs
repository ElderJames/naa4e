using Merp.Accountancy.CommandStack.Commands;
using Merp.Accountancy.CommandStack.Events;
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
    public class TimeAndMaterialJobOrderSaga : Saga,
        IAmStartedBy<RegisterTimeAndMaterialJobOrderCommand>,
        IHandleMessages<ExtendTimeAndMaterialJobOrderCommand>,
        IHandleMessages<MarkTimeAndMaterialJobOrderAsCompletedCommand>,
        IHandleMessages<LinkIncomingInvoiceToJobOrderCommand>,
        IHandleMessages<LinkOutgoingInvoiceToJobOrderCommand>
    {
        public IJobOrderNumberGenerator JobOrderNumberGenerator { get; private set; }

        public TimeAndMaterialJobOrderSaga(IBus bus, IEventStore eventStore, IRepository repository, IJobOrderNumberGenerator jobOrderNumberGenerator)
            : base(bus, eventStore, repository)
        {
            if(jobOrderNumberGenerator==null)
                throw new ArgumentNullException(nameof(jobOrderNumberGenerator));

            JobOrderNumberGenerator = jobOrderNumberGenerator;
        }

        public void Handle(RegisterTimeAndMaterialJobOrderCommand message)
        {
            var jobOrder = TimeAndMaterialJobOrder.Factory.CreateNewInstance(
                JobOrderNumberGenerator,
                message.CustomerId,
                message.ManagerId,
                message.Value,
                message.Currency,
                message.DateOfStart,
                message.DateOfExpiration,
                message.JobOrderName,
                message.PurchaseOrderNumber,
                message.Description
                );
            this.Repository.Save(jobOrder);
        }

        public void Handle(ExtendTimeAndMaterialJobOrderCommand message)
        {
            var jobOrder = Repository.GetById<TimeAndMaterialJobOrder>(message.JobOrderId);
            jobOrder.Extend(message.NewDateOfExpiration, message.Value);
            Repository.Save(jobOrder);
        }

        public void Handle(MarkTimeAndMaterialJobOrderAsCompletedCommand message)
        {
            var jobOrder = Repository.GetById<TimeAndMaterialJobOrder>(message.JobOrderId);
            jobOrder.MarkAsCompleted(message.DateOfCompletion);
            Repository.Save(jobOrder);
        }

        public void Handle(LinkIncomingInvoiceToJobOrderCommand message)
        {
            var jobOrder = Repository.GetById<TimeAndMaterialJobOrder>(message.JobOrderId);
            jobOrder.LinkIncomingInvoice(EventStore, message.InvoiceId, message.DateOfLink, message.Amount);
            Repository.Save(jobOrder);
        }

        public void Handle(LinkOutgoingInvoiceToJobOrderCommand message)
        {
            var jobOrder = Repository.GetById<TimeAndMaterialJobOrder>(message.JobOrderId);
            jobOrder.LinkOutgoingInvoice(EventStore, message.InvoiceId, message.DateOfLink, message.Amount);
            Repository.Save(jobOrder);
        }
    }
}
