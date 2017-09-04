using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IBuyStuff.Domain.Orders;
using IBuyStuff.Domain.Repositories;
using IBuyStuff.Persistence.Facade;

namespace IBuyStuff.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        #region Specific members

        public Order FindById(int id)
        {
            using (var db = new DomainModelFacade())
            {
                try
                {
                    // Need to load the entire graph of objects
                    var order = (from o in db.Orders
                        .Include("Items")
                        .Include("Items.Product")
                        where o.OrderId == id
                        select o).Single();
                    return order;
                }
                catch (InvalidOperationException)
                {
                    return new MissingOrder();
                }
            }
        }

        public Order FindLastByCustomer(string customerId)
        {
            using (var db = new DomainModelFacade())
            {
                try
                {
                    // Need to load the entire graph of objects
                    var order = (from o in db.Orders
                        .Include("Buyer")
                        .Include("Items")
                        .Include("Items.Product")
                        where o.Buyer.CustomerId == customerId
                        orderby o.OrderId descending 
                        select o).First();
                    return order;
                }
                catch (InvalidOperationException)
                {
                    return new MissingOrder();
                }
            }
        }
        
        public int AddAndReturnKey(Order aggregate)
        {
            using (var db = new DomainModelFacade())
            {
                db.Entry(aggregate.Buyer).State = EntityState.Unchanged;
                db.Orders.Add(aggregate);
                if (db.SaveChanges() > 0)
                {
                    return aggregate.OrderId;
                }
                return 0;
            }
        }
        #endregion

        #region IRepository MEMBERS

        public IList<Order> FindAll()
        {
            using (var db = new DomainModelFacade())
            {
                var orders = (from o in db.Orders select o).ToList();
                return orders;
            }
        }

        public bool Save(Order aggregate)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Order aggregate)
        {
            throw new NotImplementedException();
        }

        #endregion


        public bool Add(Order aggregate)
        {
            throw new NotImplementedException();
        }
    }
}