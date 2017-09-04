using IBuyStuff.Domain.Orders;

namespace IBuyStuff.Domain.Services
{
    public interface IShipmentService
    {
        /// <summary>
        /// The method interacts with the backend of the shipping company and books a
        /// delivery of ordered goods.
        /// </summary>
        /// <returns>True if successful; False otherwise</returns>
        OrderShippingConfirmation SendRequestForDelivery(Order order);
    }
}