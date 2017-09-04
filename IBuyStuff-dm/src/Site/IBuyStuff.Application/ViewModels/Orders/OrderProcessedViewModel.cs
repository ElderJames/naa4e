using System;
using IBuyStuff.Domain.Orders;

namespace IBuyStuff.Application.ViewModels.Orders
{
    public class OrderProcessedViewModel : OrderProcessingViewModel
    {
        public OrderProcessedViewModel()
        {
            OrderId = "---";
            PaymentDetails = new OrderPaymentConfirmation();
            ShippingDetails = new OrderShippingConfirmation();
        }
        public string OrderId { get; set; }
        public OrderPaymentConfirmation PaymentDetails { get; set; }
        public OrderShippingConfirmation ShippingDetails { get; set; }
    }
}
