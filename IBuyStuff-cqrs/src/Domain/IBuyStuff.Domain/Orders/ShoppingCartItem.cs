using IBuyStuff.Domain.Products;

namespace IBuyStuff.Domain.Orders
{
    public class ShoppingCartItem
    {
        public static ShoppingCartItem Create(int quantity, Product product)
        {
            var item = new ShoppingCartItem {Quantity = quantity, Product = product};
            return item;
        }

        private ShoppingCartItem()
        {
            Quantity = 0;
            Product = NullProduct.Instance;
        }

        public int Quantity { get; set; }
        public Product Product { get; set; }
    }
}
