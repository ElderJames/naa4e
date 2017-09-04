using System;
using IBuyStuff.QueryModel.Shared;

namespace IBuyStuff.QueryModel.Products
{
    public class MissingProduct : Product
    {
        public static MissingProduct Instance = new MissingProduct();
        public MissingProduct()
            : base(0, String.Empty, Money.Zero, 0)
        {
        }
    }
}