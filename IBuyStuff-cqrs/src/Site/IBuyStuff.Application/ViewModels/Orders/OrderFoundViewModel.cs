using System;
using System.Collections.Generic;
using IBuyStuff.QueryModel.Orders;
using IBuyStuff.QueryModel.Shared;


namespace IBuyStuff.Application.ViewModels.Orders
{
    public class OrderFoundViewModel : ViewModelBase
    {
        //public static OrderFoundViewModel CreateFromOrder(Order order)
        //{
        //    if (order == null)
        //        return new OrderFoundViewModel();

        //    var model = new OrderFoundViewModel()
        //    {
        //        Id = order.OrderId,
        //        State = order.State,
        //        OrderDate = order.Date,
        //        Total = order.Total.ToString()
        //    };
        //    foreach (var item in order.Items)
        //    {
        //        model.Details.Add(item);
        //    }
        //    return model;
        //}

        public OrderFoundViewModel()
        {
            Title = "Orders";
            Id = 0;
            Details = new List<OrderItem>();
        }

        public int Id { get; set; }
        public string State { get; set; }
        public DateTime OrderDate { get; set; }
        public Money Total { get; set; }
        public IEnumerable<OrderItem> Details { get; set; }

    }
}