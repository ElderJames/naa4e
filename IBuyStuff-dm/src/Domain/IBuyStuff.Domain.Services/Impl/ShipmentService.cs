using System;
using IBuyStuff.Domain.Orders;

namespace IBuyStuff.Domain.Services.Impl
{
    public class ShipmentService : IShipmentService
    {
        public OrderShippingConfirmation SendRequestForDelivery(Order order)
        {
            var randomDelay = new Random().Next(10); 
            var trackId = new Random().Next(10000000);
            return new OrderShippingConfirmation()
            {
                ExpectedShipDate = DateTime.Today.AddDays(randomDelay),
                TrackingId = trackId.ToString()
            };
        }
    }
}