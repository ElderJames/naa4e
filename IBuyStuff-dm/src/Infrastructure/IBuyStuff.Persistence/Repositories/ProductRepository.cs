using System;
using System.Collections.Generic;
using System.Linq;
using IBuyStuff.Domain.Products;
using IBuyStuff.Domain.Repositories;
using IBuyStuff.Persistence.Facade;

namespace IBuyStuff.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        #region Specific members

        #endregion

        #region IRepository MEMBERS

        public IList<Product> FindAll()
        {
            using (var db = new DomainModelFacade())
            {
                var products = (from p in db.Products select p).ToList();
                return products;
            }
        }

        public bool Add(Product aggregate)
        {
            throw new System.NotImplementedException();
        }

        public bool Save(Product aggregate)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(Product aggregate)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Product> FindProductBelowStockLevel(IEnumerable<Product> products, int threshold)
        {
            var productIds = (from p in products.ToArray() select p.Id).ToList();

            using (var db = new DomainModelFacade())
            {
                var list = (from p in db.Products
                    where p.StockLevel < threshold && productIds.Contains(p.Id)
                    select p).ToList();

                return list; 
            }
        }

        #endregion
    }
}