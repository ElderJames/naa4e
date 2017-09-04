using IBuyStuff.Domain.Shared;

namespace IBuyStuff.Domain.Products
{
    public class Product : IAggregateRoot
    {
        // Added for convenience only -- to quickly populate the DB on app startup.
        public Product(int id, string description, Money unitPrice, int stockLevel)
        {
            Id = id;
            Description = description;
            UnitPrice = unitPrice;
            StockLevel = stockLevel;
        }

        #region Added to please the O/RM

        /// <summary>
        /// Used by the O/RM to materialize objects
        /// </summary>
        protected Product()
        {
        }

        #endregion

        /// <summary>
        /// ID of the order, it represents the identity of the entity.
        /// /images/products/{id}.jpg is the convention for the product picture.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Description of the product.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Price per unit of product.
        /// </summary>
        public Money UnitPrice { get; private set; }

        /// <summary>
        /// Stock level defined for the product.
        /// </summary>
        public int StockLevel { get; private set; }

        /// <summary>
        /// Indicates whether the product is featured on the home page.
        /// That makes it appear available for selection in the shopping cart (in THIS example).
        /// </summary>
        public bool Featured { get; private set; }

        #region Behavior
        /// <summary>
        /// Applies a bit of biz logic to determine how many items left should
        /// be displayed when the product is featured. By default half the real stock.
        /// </summary>
        /// <returns></returns>
        public int GetStockForDisplay()
        {
            return StockLevel/2;
        }

        #endregion

        #region Identity Management
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;
            var other = (Product)obj;

            // Your identity logic goes here.  
            // You may refactor this code to the method of an entity interface 
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion
    }
}
