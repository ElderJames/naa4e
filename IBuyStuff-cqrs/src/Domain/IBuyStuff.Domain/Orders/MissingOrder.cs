using IBuyStuff.Domain.Customers;

namespace IBuyStuff.Domain.Orders
{
    public class MissingOrder : Order 
    {
        public static MissingOrder Instance = new MissingOrder();
        public MissingOrder() : base(0, MissingCustomer.Instance)
        {
        }
    }
}
