using System.Collections;
using System.Collections.Generic;
using IBuyStuff.Domain.Customers;
using IBuyStuff.Domain.Products;

namespace IBuyStuff.Domain.Services.DTO
{
    public class AdHocCatalog
    {
        public AdHocCatalog()
        {
            Customer = MissingCustomer.Instance;
            Products = new List<Product>();
        }
        public Customer Customer { get; set; }
        public IList<Product> Products { get; set; }
    }
}