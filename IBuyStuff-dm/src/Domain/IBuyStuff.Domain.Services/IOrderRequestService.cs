using System.Collections;
using System.Collections.Generic;
using IBuyStuff.Domain.Customers;
using IBuyStuff.Domain.Orders;
using IBuyStuff.Domain.Products;
using IBuyStuff.Domain.Services.DTO;
using IBuyStuff.Domain.Shared;

namespace IBuyStuff.Domain.Services
{
    public interface IOrderRequestService
    {
        /// <summary>
        /// For each line of the order, it ensures that enough items are in stock. If 
        /// stock level falls below a given threshold, it places an internal order to refill.
        /// </summary>
        /// <returns>True if successful; False otherwise</returns>
        LowStockReport CheckStockLevelForOrderedItems(ShoppingCart cart);

        /// <summary>
        /// The method places an order (whatever that means) to cause a refill of the 
        /// store all specified products. The method is invoked after current order is placed or
        /// even periodically by some scheduled service.
        /// </summary>
        void RefillStoreForProduct(IEnumerable<Product> products);

        /// <summary>
        /// Saves address and payment information within the customer record.
        /// </summary>
        /// <param name="orderRequest">Order request</param>
        /// <param name="address">Address to store within the customer record</param>
        /// <param name="card">Payment details to store within the customer record</param>
        void SaveCheckoutInformation(ShoppingCart orderRequest, Address address, CreditCard card);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId">ID of the customer to check</param>
        /// <returns></returns>
        bool CheckCustomerPaymentHistory(string customerId);

        /// <summary>
        /// Return the next order ID
        /// </summary>
        /// <returns>Order ID</returns>
        int GenerateTemporaryOrderId();
    }
}