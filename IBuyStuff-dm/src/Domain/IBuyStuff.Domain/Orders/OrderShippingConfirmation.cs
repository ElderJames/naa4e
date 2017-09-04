using System;

namespace IBuyStuff.Domain.Orders
{
    public class OrderShippingConfirmation
    {
        public OrderShippingConfirmation()
        {
            TrackingId = String.Empty;
            ExpectedShipDate = DateTime.MaxValue;
        }
        public string TrackingId { get; set; }
        public DateTime ExpectedShipDate { get; set; }
    }
}
