using System.Collections.Generic;
using System.Data.Entity;
using IBuyStuff.Domain.Customers;
using IBuyStuff.Domain.Orders;
using IBuyStuff.Domain.Products;
using IBuyStuff.Domain.Shared;
using IBuyStuff.Persistence.Facade;

namespace IBuyStuff.Persistence.Utils
{
    public class SampleAppInitializer : DropCreateDatabaseIfModelChanges<DomainModelFacade>
    {
        protected override void Seed(DomainModelFacade context)
        {
            /////////////////////////////////////////////////////////////////
            // Products
            var products = new List<Product>
            {
                new Product(1, "Tennis Racquet", new Money(Currency.Default, 200), 10),
                new Product(2, "Dartboard", new Money(Currency.Default, 10), 10),
                new Product(3, "Volley ball", new Money(Currency.Default, 10), 10),
                new Product(4, "Baseball cap", new Money(Currency.Default, 50), 10),
                new Product(5, "Bike", new Money(Currency.Default, 50), 10),
                new Product(6, "Ice skate shoes", new Money(Currency.Default, 20), 10),
                new Product(7, "Running shoes", new Money(Currency.Default, 60), 10),
                new Product(8, "Basket ball", new Money(Currency.Default, 15), 10),
                new Product(9, "Umbrella", new Money(Currency.Default, 8), 10),
                new Product(10, "Goggles", new Money(Currency.Default, 11), 10),
            };
            context.Products.AddRange(products);

            /////////////////////////////////////////////////////////////////
            // Customers
            var defaultCustomer = Customer.CreateNew(Gender.Male, "naa4e", "Foo", "Bar", "naa4e@expoware.org");
            var customers = new List<Customer>
            {
                defaultCustomer
            };
            context.Customers.AddRange(customers);

            /////////////////////////////////////////////////////////////////
            // Admins
            var admins = new List<Admin>
            {
                Admin.CreateNew("admin"),
            };
            context.Admins.AddRange(admins);

            /////////////////////////////////////////////////////////////////
            // Orders
            var orders = new List<Order>
            {
                Order.CreateNew(1, defaultCustomer),
                Order.CreateNew(2, defaultCustomer),
                Order.CreateNew(3, defaultCustomer)
            };
            context.Orders.AddRange(orders);

            /////////////////////////////////////////////////////////////////
            // Fidelity Cards
            var cards = new List<FidelityCard>
            {
                FidelityCard.CreateNew("0101xyz001", defaultCustomer),
            };
            context.FidelityCards.AddRange(cards);

            // All standards will
            base.Seed(context);
        }
    }
}
