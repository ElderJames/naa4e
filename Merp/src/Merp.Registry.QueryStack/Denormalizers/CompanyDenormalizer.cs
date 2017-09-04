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
    public class CompanyDenormalizer : 
        IHandleMessages<CompanyRegisteredEvent>,
        IHandleMessages<CompanyNameChangedEvent>
    {
        public void Handle(CompanyRegisteredEvent message)
        {
            var p = new Company()
            {
                CompanyName = message.CompanyName,
                VatIndex = message.VatIndex,
                OriginalId = message.CompanyId,
                DisplayName = message.CompanyName
            };
            using(var context = new RegistryDbContext())
            {
                context.Parties.Add(p);
                context.SaveChanges();
            }
        }

        public void Handle(CompanyNameChangedEvent message)
        {
            using (var context = new RegistryDbContext())
            {
                var company = (from c in context.Parties.OfType<Company>()
                               where c.OriginalId == message.CompanyId
                               select c).Single();
                company.DisplayName = message.CompanyName;
                company.CompanyName = message.CompanyName;
                
                context.SaveChanges();
            }
        }
    }
}
