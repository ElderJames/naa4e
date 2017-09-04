using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merp.Registry.QueryStack
{
    public class Database : IDatabase, IDisposable
    {
        private RegistryDbContext Context = null;

        public Database()
        {
            Context = new RegistryDbContext();
            Context.Configuration.AutoDetectChangesEnabled = false;
        }
        public IQueryable<Model.Party> Parties
        {
            get
            {
                return Context.Parties;
            }

        }

        public void Dispose()
        {
            if(Context!= null)
                Context.Dispose();
        }
    }
}
