using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using IBuyStuff.Domain.Customers;
using IBuyStuff.Domain.Misc;
using IBuyStuff.Domain.Orders;
using IBuyStuff.Domain.Products;
using IBuyStuff.Domain.Shared;
using IBuyStuff.Persistence.Mappings;
using IBuyStuff.Persistence.Utils;

namespace IBuyStuff.Persistence.Facade
{
    public class DomainModelFacade : DbContext 
    {
        static DomainModelFacade()
        {
            Database.SetInitializer(new SampleAppInitializer());
        }

        public DomainModelFacade() : base("naa4e-09")   // specify here conn-string entry if using SQL Server
        {
            Products = base.Set<Product>();
            Customers = base.Set<Customer>();
            Orders = base.Set<Order>();
            OrderItems = base.Set<OrderItem>();
            Admins = base.Set<Admin>();
            FidelityCards = base.Set<FidelityCard>();
            Subscribers = base.Set<Subscriber>();            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.ComplexType<Money>();
            modelBuilder.ComplexType<Address>();
            modelBuilder.ComplexType<CreditCard>();

            modelBuilder.Configurations.Add(new ExpiryDateMap());
            modelBuilder.Configurations.Add(new FidelityCardMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new CurrencyMap());
            modelBuilder.Configurations.Add(new OrderItemMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new AdminMap());
        }

        public DbSet<Order> Orders { get; private set; }

        public DbSet<OrderItem> OrderItems { get; private set; }

        public DbSet<Customer> Customers { get; private set; }

        public DbSet<Admin> Admins { get; private set; }

        public DbSet<Product> Products { get; private set; }

        public DbSet<FidelityCard> FidelityCards { get; private set; }

        public DbSet<Subscriber> Subscribers { get; private set; }
    }
}