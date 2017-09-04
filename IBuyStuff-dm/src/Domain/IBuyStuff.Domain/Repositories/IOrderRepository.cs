using IBuyStuff.Domain.Orders;

namespace IBuyStuff.Domain.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order FindById(int id);
        Order FindLastByCustomer(string customerId);


        /// <summary>
        /// Add an aggregate graph to the store and return the key.
        /// </summary>
        /// <param name="aggregate">Aggregate root object</param>
        /// <returns>Key</returns>
        int AddAndReturnKey(Order aggregate);
    }
}