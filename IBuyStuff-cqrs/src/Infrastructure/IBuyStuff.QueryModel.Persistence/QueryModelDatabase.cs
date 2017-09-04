using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using IBuyStuff.QueryModel.Persistence.Mappings;
using IBuyStuff.QueryModel.Customers;
using IBuyStuff.QueryModel.Orders;
using IBuyStuff.QueryModel.Products;
using IBuyStuff.QueryModel.Shared;

namespace IBuyStuff.QueryModel.Persistence
{
    public class QueryModelDatabase : DbContext, IQueryModelDatabase
    {
        public QueryModelDatabase() : base("naa4e-11")
        {
            _products = base.Set<Product>();
            _orders = base.Set<Order>();
        }

        private readonly DbSet<Order> _orders = null;
        private readonly DbSet<Product> _products = null;

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.ComplexType<Money>();
            modelBuilder.ComplexType<Address>();
            modelBuilder.ComplexType<CreditCard>();

            modelBuilder.Configurations.Add(new ExpiryDateMap());
            //modelBuilder.Configurations.Add(new FidelityCardMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new CurrencyMap());
            modelBuilder.Configurations.Add(new OrderItemMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            //modelBuilder.Configurations.Add(new AdminMap());
        }

        public IQueryable<Order> Orders
        {
            get { return this._orders.Include("Items").Include("Items.Product"); }
        }

        public IQueryable<Product> Products
        {
            get { return _products; }
        }
    }
}