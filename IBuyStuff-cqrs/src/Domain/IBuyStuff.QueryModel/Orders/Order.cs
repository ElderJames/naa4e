using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using IBuyStuff.QueryModel.Core;
using IBuyStuff.QueryModel.Shared;

namespace IBuyStuff.QueryModel.Orders
{
    public class Order 
    {
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
        }

        #endregion

        #region Props
        /// <summary>
        /// ID of the order, it represents the identity of the entity.
        /// </summary>
        public int OrderId { get; set; }

        public ICollection<OrderItem> Items { get; set; }

        /// <summary>
        /// Total of the order. This property should be set care of the repository: either from
        /// a redundand column in the Orders table or via a GROUP/JOIN query on Items.
        /// </summary>
        public Money Total { get; set; }

        /// <summary>
        /// Indicates the state of the order.
        /// </summary>
        public OrderState State { get; set; }

        /// <summary>
        /// Date of the order
        /// </summary>
        public DateTime Date { get; set; }
        #endregion

    }
}
