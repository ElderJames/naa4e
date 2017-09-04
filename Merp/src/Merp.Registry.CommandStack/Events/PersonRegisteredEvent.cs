using Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merp.Registry.CommandStack.Events
{
    public class PersonRegisteredEvent : DomainEvent
    {
        public Guid PersonId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public PersonRegisteredEvent(Guid personId, string firstName, string lastName)
        {
            PersonId = personId;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
