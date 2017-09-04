using System.Collections.Generic;
using System.Linq;
using IBuyStuff.Domain.Products;
using IBuyStuff.Domain.Services.DTO;
using IBuyStuff.Persistence.Facade;

namespace IBuyStuff.Domain.Services.Impl
{
    public class CatalogService : ICatalogService
    {
        public ICollection<Product> GetFeaturedProducts(int count = 3)
        {
            // Featured() is just a business-oriented way of writing a WHERE clause
            using (var db = new DomainModelFacade())
            {
                var products = (from p in db.Products where p.Featured select p).Take(count).ToList();
                if (!products.Any())
                    products = (from p in db.Products orderby p.StockLevel descending select p).Take(1).ToList();
                return products;
            }
        }

        public AdHocCatalog GetCustomerAdHocCatalog(string customerId)
        {
            var catalog = new AdHocCatalog();
            using (var db = new DomainModelFacade())
            {
                catalog.Products = (from p in db.Products select p).ToList();
                catalog.Customer = (from c in db.Customers where c.CustomerId == customerId select c).Single();
            }
            return catalog;
        }
    }
}