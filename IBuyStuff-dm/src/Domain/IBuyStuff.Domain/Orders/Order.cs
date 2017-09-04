using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using IBuyStuff.Domain.Customers;
using IBuyStuff.Domain.Shared;

namespace IBuyStuff.Domain.Orders
{
    public class Order : IAggregateRoot
    {
        // The class has no public constructor because instances of it will only be 
        // created materializing from storage. An O/RM will do it and it can use
        // the protected ctor.

        // This method is only for demo purposes to initially fill up the database
        public static Order CreateNew(int id, Customer customer)
        {
            var order = new Order(id, customer);
            return order;
        }

        public static Order CreateFromShoppingCart(int temporaryId, ShoppingCart cart)
        {
            var order = new Order(temporaryId, cart.Buyer);
            foreach (var cartItem in cart.Items)
            {
                var orderItem = OrderItem.CreateNewForOrder(order, cartItem.Quantity, cartItem.Product);
                order.Items.Add(orderItem);
                order.Total = cart.GetTotal();
            }
            return order;
        }

        protected Order(int orderId, Customer customer)
        {
            OrderId = orderId;
            State = OrderState.Pending;
            Total = Money.Zero;
            Date = DateTime.Today;
            Items = new Collection<OrderItem>();
            Buyer = customer;
        }

        #region Added to please the O/RM

        /// <summary>
        /// Used by the O/RM to materialize objects
        /// </summary>
        protected Order()
        {
            State = OrderState.Pending;
            Total = Money.Zero;
            Date = DateTime.Today;
            Items = new Collection<OrderItem>();
            Buyer = MissingCustomer.Instance;
        }

        #endregion

        #region Props
        /// <summary>
        /// ID of the order, it represents the identity of the entity.
        /// </summary>
        public int OrderId { get; private set; }

        public Customer Buyer { get; private set; }

        /*
         * If you expose a public collection property, you prevent from adding/removing 
         * new items, but not from updating existing items. This holds true even if you 
         * change ICollection<T> to IEnumerable<T>. The following code is always possible to write.
         * 
         * Items[0].Quantity ++; 
         * 
         * You can't block setters on Quantity as OrderItems must be set when preparing 
         * the order request.
         * a) You expose order items through a different type with private setters and 
         *    not using Product as a reference!
         * b) You may even return a clone of Items if for some reason you need to make sure
         *    that the original collection is not touched.
         *    
         * Simplest way to handle this is using different types, which leads towards CQRS.
         * 
         * Simplest way to handle this in DDD is handling updates on an order through a different 
         * object such OrderUpdateRequest. And just return Items as is, but just ignoring any 
         * changes it may run into.
         */ 
        public ICollection<OrderItem> Items { get; private set; }

        /// <summary>
        /// Total of the order. This property should be set care of the repository: either from
        /// a redundand column in the Orders table or via a GROUP/JOIN query on Items.
        /// </summary>
        public Money Total { get; private set; }

        /// <summary>
        /// Indicates the state of the order.
        /// </summary>
        public OrderState State { get; private set; }

        /// <summary>
        /// Date of the order
        /// </summary>
        public DateTime Date { get; private set; }
        #endregion

        #region Behavior
        /// <summary>
        /// Add a collection of order items to the order
        /// </summary>
        /// <param name="items">Collection of order items</param>
        /// <returns>Same instance</returns>
        public Order AddItems(ICollection<OrderItem> items)
        {
            foreach (var item in items)
            {
                Items.Add(item);
            }
            return this;
        }

        public Order Cancel()
        {
            if (State != OrderState.Pending)
                throw new InvalidOperationException("Can't cancel an order that is not pending.");

            State = OrderState.Canceled;
            return this;
        }
        public Order Archive()
        {
            if (State != OrderState.Shipped)
                throw new InvalidOperationException("Can't archive an order that has not shipped yet.");

            State = OrderState.Canceled;
            return this;
        }
        public Order HasShipped()
        {
            if (State != OrderState.Shipped)
                throw new InvalidOperationException("Can't mark as shipped an order that is not pending.");

            State = OrderState.Shipped;
            return this;
        }
        #endregion

        #region Identity Management
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;
            var other = (Order)obj;

            // Your identity logic goes here.  
            // You may refactor this code to the method of an entity interface 
            return OrderId == other.OrderId;
        }

        public override int GetHashCode()
        {
            return OrderId.GetHashCode();
        }
        #endregion
    }
}
