using Memento;
using Merp.Registry.CommandStack.Events;
using Merp.Registry.QueryStack.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memento.Messaging.Postie;

namespace Merp.Registry.QueryStack.Denormalizers
{
    public class PersonDenormalizer : IHandleMessages<PersonRegisteredEvent>
    {
        public void Handle(PersonRegisteredEvent message)
        {
            var p = new Person()
            {
                FirstName = message.FirstName,
                LastName = message.LastName,
                OriginalId = message.PersonId,
                DisplayName = $"{message.FirstName} {message.LastName}"
            };
            using(var context = new RegistryDbContext())
            {
                context.Parties.Add(p);
                context.SaveChanges();
            }

        }
    }
}
