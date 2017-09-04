using System.Collections.Generic;
using System.Linq;
using IBuyStuff.Domain.Misc;
using IBuyStuff.Domain.Repositories;
using IBuyStuff.Persistence.Facade;

namespace IBuyStuff.Persistence.Repositories
{
    public class SubscriberRepository : ISubscriberRepository
    {
        public int Count()
        {
            using (var db = new DomainModelFacade())
            {
                return (from s in db.Subscribers select s).Count();
            }
        }

        #region IRepository
        public IList<Subscriber> FindAll()
        {
            throw new System.NotImplementedException();
        }

        public bool Add(Subscriber aggregate)
        {
            using (var db = new DomainModelFacade())
            {
                db.Subscribers.Add(aggregate);
                return db.SaveChanges() > 0;
            }
        }

        public bool Save(Subscriber aggregate)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(Subscriber aggregate)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}