using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memento.Messaging.Postie;
using Memento;
using Memento.Persistence;
using Merp.Registry.CommandStack.Commands;
using Merp.Registry.CommandStack.Model;

namespace Merp.Registry.CommandStack.Sagas
{
    public class CompanySaga : Saga,
        IAmStartedBy<RegisterCompanyCommand>,
        IHandleMessages<ChangeCompanyNameCommand>
    {
        public CompanySaga(IBus bus, IEventStore eventStore, IRepository repository)
            : base(bus, eventStore, repository)
        {
            
        }

        public void Handle(RegisterCompanyCommand message)
        {
            var company = Company.Factory.CreateNewEntry(message.CompanyName, message.VatIndex);
            Repository.Save(company);
        }

        public void Handle(ChangeCompanyNameCommand message)
        {
            var company = Repository.GetById<Company>(message.CompanyId);
            company.ChangeName(message.CompanyName, message.EffectiveDate);
            Repository.Save(company);
        }
    }
}
