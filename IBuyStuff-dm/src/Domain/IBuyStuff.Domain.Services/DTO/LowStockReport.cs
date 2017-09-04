using System.Collections.Generic;
using System.Collections.ObjectModel;
using IBuyStuff.Domain.Products;

namespace IBuyStuff.Domain.Services.DTO
{
    public class LowStockReport
    {
        public LowStockReport()
        {
            Insufficient = new Collection<Product>();
            Low = new Collection<Product>();
        }
        public ICollection<Product> Insufficient { get; private set; }
        public ICollection<Product> Low { get; private set; }
    }
}