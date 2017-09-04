using System.Linq;
using IBuyStuff.QueryModel.Orders;
using IBuyStuff.QueryModel.Products;

namespace IBuyStuff.QueryModel
{
    public interface IQueryModelDatabase
    {
        //IQueryable<Customer> Customers { get; }
        IQueryable<Order> Orders { get; }
        IQueryable<Product> Products { get; } 
    }
}