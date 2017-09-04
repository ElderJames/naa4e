using IBuyStuff.Domain.Products;
using IBuyStuff.Domain.Shared;

namespace IBuyStuff.Domain.Orders
{
    public class OrderItem
    {
        public static OrderItem CreateNewForOrder(Order order, int quantity, Product product)
        {
            var item = new OrderItem {Quantity = quantity, Product = product, Order = order};
            return item;
        }

        protected OrderItem()
        {
            
        }

        public int Id { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }

        #region Behavior
        /// <summary>
        /// Get the total of the order item
        /// </summary>
        /// <returns>Total</returns>
        public Money GetTotal()
        {
            return new Money(Product.UnitPrice.Currency, Quantity*Product.UnitPrice.Value);
        }
        #endregion
    }
}