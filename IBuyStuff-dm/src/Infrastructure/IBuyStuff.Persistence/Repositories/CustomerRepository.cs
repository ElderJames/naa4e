using System;
using System.Collections.Generic;
using System.Linq;
using IBuyStuff.Domain.Customers;
using IBuyStuff.Domain.Repositories;
using IBuyStuff.Persistence.Facade;

namespace IBuyStuff.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public Customer FindById(string id)
        {
            using (var db = new DomainModelFacade())
            {
                try
                {
                    var customer = (from c in db.Customers where c.CustomerId == id select c).Single();
                    return customer;
                }
                catch (InvalidOperationException)
                {
                    return new MissingCustomer();
                }
            }
        }

        #region IRepository MEMBERS

        public IList<Customer> FindAll()
        {
            throw new NotImplementedException();
        }

        public bool Add(Customer aggregate)
        {
            using (var db = new DomainModelFacade())
            {
                db.Customers.Add(aggregate);
                return db.SaveChanges() >0;
            }
        }

        public bool Save(Customer aggregate)
        {
            using (var db = new DomainModelFacade())
            {
                try
                {
                    var customer = (from c in db.Customers where c.CustomerId == aggregate.CustomerId select c).Single();
                    customer.SetAddress(aggregate.Address);
                    customer.SetAvatar(aggregate.Avatar);
                    customer.SetPaymentDetails(aggregate.Payment);
                    customer.SetPasswordHash(aggregate.PasswordHash);
                    var changes = db.SaveChanges();
                    return changes >0;
                }
                catch (InvalidOperationException)
                {
                    return false;
                }
            }
        }

        public bool Delete(Customer aggregate)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}