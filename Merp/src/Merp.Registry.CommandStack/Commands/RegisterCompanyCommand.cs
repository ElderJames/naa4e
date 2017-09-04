using Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merp.Registry.CommandStack.Commands
{
    public class RegisterCompanyCommand : Command
    {
        public string CompanyName { get; private set; }
        public string VatIndex { get; private set; }

        public RegisterCompanyCommand(string companyName, string vatIndex)
        {
            CompanyName = companyName;
            VatIndex = vatIndex;
        }
    }
}
