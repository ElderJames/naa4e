using System.Collections.Generic;
using IBuyStuff.Domain.Products;

namespace IBuyStuff.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> FindProductBelowStockLevel(IEnumerable<Product> products, int threshold);
    }
}