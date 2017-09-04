using System;
using System.Collections.Generic;
using System.Linq;
using IBuyStuff.Domain.Customers;
using IBuyStuff.Domain.Orders;
using IBuyStuff.Domain.Products;
using IBuyStuff.Domain.Repositories;
using IBuyStuff.Domain.Services.DTO;
using IBuyStuff.Domain.Shared;
using IBuyStuff.Persistence.Repositories;

namespace IBuyStuff.Domain.Services.Impl
{
    public class OrderRequestService : IOrderRequestService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderRequestService() : this(new ProductRepository(), new CustomerRepository())
        {            
        }
        public OrderRequestService(IProductRepository productRepository, ICustomerRepository customerRepository)
        {
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }

        public LowStockReport CheckStockLevelForOrderedItems(ShoppingCart cart)
        {
            var threshold = GetCriticalThresholdForStock();

            var requestedProducts = (from p in cart.Items select p.Product).ToArray();
            var criticalProducts = _productRepository.FindProductBelowStockLevel(requestedProducts, threshold);

            var stock = new LowStockReport();
            foreach (var item in criticalProducts)
            {
                var quantityOrdered = (from p in cart.Items where p.Product.Id == item.Id select p.Quantity).Single();
                if (item.StockLevel - quantityOrdered <= 0)
                {
                    stock.Insufficient.Add(item);
                }
                else
                {
                    stock.Low.Add(item);
                }
            }
            return stock;
        }

        public void RefillStoreForProduct(IEnumerable<Product> products)
        {
            // Goes through the list of products and places an order 
            // to increase stock.
            return;
        }

        public void SaveCheckoutInformation(ShoppingCart orderRequest, Address address, CreditCard card)
        {
            orderRequest.Buyer.SetAddress(address);
            orderRequest.Buyer.SetPaymentDetails(card);
            _customerRepository.Save(orderRequest.Buyer);
        }

        public bool CheckCustomerPaymentHistory(string customerId)
        {
            return true;
        }

        public int GenerateTemporaryOrderId()
        {
            // Will use the order repository
            return new Random().Next(1000000);
        }

        #region Private methods

        private static int GetCriticalThresholdForStock()
        {
            return 3;
        }

        #endregion
    }
}