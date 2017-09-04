using Memento;
using Memento.Persistence;
using Merp.Registry.CommandStack.Commands;
using Merp.Registry.CommandStack.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memento.Messaging.Postie;


namespace Merp.Registry.CommandStack.Sagas
{
    public class PersonSaga : Saga,
        IAmStartedBy<RegisterPersonCommand>
    {
        public PersonSaga(IBus bus, IEventStore eventStore, IRepository repository)
            : base(bus, eventStore, repository)
        {
            
        }

        public void Handle(RegisterPersonCommand message)
        {
            var person = Person.Factory.CreateNewEntry(message.FirstName, message.LastName, message.DateOfBirth);
            Repository.Save<Person>(person);
        }
    }
}
