using System.Collections.Generic;
using IBuyStuff.Domain.Products;
using IBuyStuff.Domain.Services.DTO;

namespace IBuyStuff.Domain.Services
{
    public interface ICatalogService
    {
        ICollection<Product> GetFeaturedProducts(int count);
        AdHocCatalog GetCustomerAdHocCatalog(string customerId);
    }
}