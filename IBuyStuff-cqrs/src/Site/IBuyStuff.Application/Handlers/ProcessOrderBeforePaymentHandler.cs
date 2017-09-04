using System.Collections.Generic;
using System.Linq;
using IBuyStuff.Application.Commands;
using IBuyStuff.Application.ViewModels.Orders;
using IBuyStuff.Domain.Customers;
using IBuyStuff.Domain.Products;
using IBuyStuff.Domain.Repositories;
using IBuyStuff.Domain.Services;
using IBuyStuff.Domain.Services.Impl;
using IBuyStuff.Domain.Shared;
using IBuyStuff.Persistence.Repositories;

namespace IBuyStuff.Application.Handlers
{
    public class ProcessOrderBeforePaymentHandler 
        : ICommandHandler<ProcessOrderBeforePaymentCommand, OrderProcessingViewModel>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICatalogService _catalogService;
        private readonly IOrderRequestService _requestService;
        private readonly IShipmentService _shipmentService;

        public ProcessOrderBeforePaymentHandler()
            : this(new OrderRepository(), new CatalogService(), new OrderRequestService(), new ShipmentService())
        {
        }

        public ProcessOrderBeforePaymentHandler(IOrderRepository orderRepository, ICatalogService catalogService, IOrderRequestService requestService, IShipmentService shipmentService)
        {
            _orderRepository = orderRepository;
            _catalogService = catalogService;
            _requestService = requestService;
            _shipmentService = shipmentService;
        }

        public OrderProcessingViewModel Handle(ProcessOrderBeforePaymentCommand command)
        {
            var response = new OrderProcessingViewModel();

            // 1. Save checkout data as part of the customer record (shipping & payment).
            //    This is a bit simplistic as shipping address and payment details should be both on Customer
            //    (to set default options) and Order (to be part of the history).
            var address = Address.Create(
                command.CheckoutData.Address, 
                "", 
                command.CheckoutData.City, 
                "", 
                command.CheckoutData.Country);

            var payment = CreditCard.Create(
                command.CheckoutData.CardType, 
                command.CheckoutData.CardNumber, 
                "", 
                new ExpiryDate(command.CheckoutData.Month, command.CheckoutData.Year));

            _requestService.SaveCheckoutInformation(command.ShoppingCart.OrderRequest, address, payment);


            // 2. Goods in store (precheck to give users a chance not to place an order that may take a while to 
            //    be completed and served. Most sites just make you pay and place an order for missing items while
            //    giving you a chance to cancel the order at any time.
            var stock = _requestService.CheckStockLevelForOrderedItems(command.ShoppingCart.OrderRequest);
            if (stock.Insufficient.Any())
            {
                response.Denied = true;
                response.AddMessage("It seems that we don't have available all the items you ordered. What would you like to do? Buying a bit less or trying later?");
                return response;
            }

            // 3. Payment history for the customer
            //    Probably not really an appropriate scenario for this simple store: if the online store accept
            //    payment cash-on-delivery, however, you might want to enable it only for customers with a
            //    positive payment history.
            if (!_requestService.CheckCustomerPaymentHistory(command.ShoppingCart.OrderRequest.Buyer.CustomerId))
            {
                response.Denied = true;
                response.AddMessage("We've found something incorrect in your record that prevents our system from processing your order. Please, contact our customer care.");
                return response;
            }

            // 4. Refill stock
            var productsToOrder = new List<Product>();
            productsToOrder.AddRange(stock.Low);
            productsToOrder.AddRange(stock.Insufficient);
            _requestService.RefillStoreForProduct(productsToOrder);

            return response;            
        }
    }
}