using IBuyStuff.Domain.Orders;

namespace IBuyStuff.Domain.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        //Order FindById(int id);
        Order FindLastByCustomer(string customerId);
        int AddAndReturnKey(Order aggregate);
    }
}