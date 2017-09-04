using IBuyStuff.QueryModel.Products;
using IBuyStuff.QueryModel.Shared;

namespace IBuyStuff.QueryModel.Orders
{
    public class OrderItem
    {
        protected OrderItem()
        {
            
        }

        public int Id { get; private set; }
        public int Quantity { get; private set; }
        public Product Product { get; set; }
        public  Order Order { get; set; }

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