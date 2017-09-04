using System;
using IBuyStuff.Domain.Customers;
using IBuyStuff.Domain.Orders;
using IBuyStuff.Domain.Shared;

namespace IBuyStuff.Domain.Products
{
    public class NullProduct : Product
    {
        public static NullProduct Instance = new NullProduct();
        public NullProduct() : base(0, String.Empty, Money.Zero, 0)
        {
        }
    }
}