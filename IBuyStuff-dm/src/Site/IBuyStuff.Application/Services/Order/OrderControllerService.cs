using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using IBuyStuff.Application.InputModels.Order;
using IBuyStuff.Application.ViewModels.Orders;
using IBuyStuff.Domain.Customers;
using IBuyStuff.Domain.Orders;
using IBuyStuff.Domain.Products;
using IBuyStuff.Domain.Repositories;
using IBuyStuff.Domain.Services;
using IBuyStuff.Domain.Services.Impl;
using IBuyStuff.Domain.Shared;
using IBuyStuff.Persistence.Repositories;

namespace IBuyStuff.Application.Services.Order
{
    public class OrderControllerService : IOrderControllerService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICatalogService _catalogService;
        private readonly IOrderRequestService _requestService;
        private readonly IShipmentService _shipmentService;

        public OrderControllerService()
            : this(new OrderRepository(), new CatalogService(), new OrderRequestService(), new ShipmentService())
        {
        }

        public OrderControllerService(IOrderRepository orderRepository, ICatalogService catalogService, IOrderRequestService requestService, IShipmentService shipmentService )
        {
            _orderRepository = orderRepository;
            _catalogService = catalogService;
            _requestService = requestService;
            _shipmentService = shipmentService;
        }

        public OrderFoundViewModel RetrieveOrderForCustomer(int orderId)
        {
            var order = _orderRepository.FindById(orderId);
            if (order is MissingOrder)
                return new OrderFoundViewModel();
            return OrderFoundViewModel.CreateFromOrder(order);
        }

        public OrderFoundViewModel RetrieveLastOrderForCustomer(string customerId)
        {
            var order = _orderRepository.FindLastByCustomer(customerId);
            if (order is MissingOrder)
                return new OrderFoundViewModel();
            return OrderFoundViewModel.CreateFromOrder(order);
        }

        public ShoppingCartViewModel  CreateShoppingCartForCustomer(string customerId)
        {
            var adHocCatalog = _catalogService.GetCustomerAdHocCatalog(customerId);
            var cart = ShoppingCart.CreateEmpty(adHocCatalog.Customer);
            return ShoppingCartViewModel.CreateEmpty(cart, adHocCatalog.Products);
        }

        public ShoppingCartViewModel AddProductToShoppingCart(ShoppingCartViewModel cart, int productId, int quantity)
        {
            var product = (from p in cart.Products where p.Id == productId select p).Single();
            cart.OrderRequest.AddItem(quantity, product);
            return cart;
        }

        public OrderProcessingViewModel ProcessOrderBeforePayment(ShoppingCartViewModel cart, CheckoutInputModel checkout)
        {
            var response = new OrderProcessingViewModel();
            
            // 1. Save checkout data as part of the customer record (shipping & payment).
            //    This is a bit simplistic as shipping address and payment details should be both on Customer
            //    (to set default options) and Order (to be part of the history).
            var address = Address.Create(checkout.Address, "", checkout.City, "", checkout.Country);
            var payment = CreditCard.Create(checkout.CardType, checkout.CardNumber, "", new ExpiryDate(checkout.Month, checkout.Year));
            _requestService.SaveCheckoutInformation(cart.OrderRequest, address, payment);


            // 2. Goods in store (precheck to give users a chance not to place an order that may take a while to 
            //    be completed and served. Most sites just make you pay and place an order for missing items while
            //    giving you a chance to cancel the order at any time.
            var stock = _requestService.CheckStockLevelForOrderedItems(cart.OrderRequest);
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
            if (!_requestService.CheckCustomerPaymentHistory(cart.OrderRequest.Buyer.CustomerId))
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

        public OrderProcessedViewModel ProcessOrderAfterPayment(ShoppingCartViewModel cart, string transactionId)
        {
            // 1. Create order ID
            var tempOrderId = _requestService.GenerateTemporaryOrderId();

            // 2. Register order in the system 
            var order = Domain.Orders.Order.CreateFromShoppingCart(tempOrderId, cart.OrderRequest);
            var orderId = _orderRepository.AddAndReturnKey(order);

            // 3. Ship 
            var shipmentDetails = _shipmentService.SendRequestForDelivery(order);

            // 4. Update fidelity card and membership status

            // Prepare model
            var model = new OrderProcessedViewModel
            {
                OrderId = orderId.ToString(CultureInfo.InvariantCulture),
                PaymentDetails = {TransactionId = transactionId},
                ShippingDetails = shipmentDetails
            };
            return model;
        }
    }
}