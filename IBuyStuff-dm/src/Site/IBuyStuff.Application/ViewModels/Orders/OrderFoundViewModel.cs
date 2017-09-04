using System;
using System.Collections.Generic;
using IBuyStuff.Domain.Orders;

namespace IBuyStuff.Application.ViewModels.Orders
{
    public class OrderFoundViewModel : ViewModelBase
    {
        public static OrderFoundViewModel CreateFromOrder(Order order)
        {
            if (order == null)
                return new OrderFoundViewModel();

            var model = new OrderFoundViewModel()
            {
                Id = order.OrderId,
                State = order.State,
                OrderDate = order.Date,
                Total = order.Total.ToString()
            };
            foreach (var item in order.Items)
            {
                model.Details.Add(item);
            }
            return model;
        }

        public OrderFoundViewModel()
        {
            Title = "Orders";
            Id = 0;
            State = OrderState.Pending;
            Details = new List<OrderItem>();
        }

        public int Id { get; set; }
        public OrderState State { get; set; }
        public DateTime OrderDate { get; set; }
        public string Total { get; set; }
        public ICollection<OrderItem> Details { get; set; }

    }
}