using System.Globalization;
using IBuyStuff.Application.Commands;
using IBuyStuff.Application.ViewModels.Orders;
using IBuyStuff.Domain.Repositories;
using IBuyStuff.Domain.Services;
using IBuyStuff.Domain.Services.Impl;
using IBuyStuff.Persistence.Repositories;

namespace IBuyStuff.Application.Handlers
{
    public class ProcessOrderAfterPaymentHandler 
        : ICommandHandler<ProcessOrderAfterPaymentCommand, OrderProcessedViewModel>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICatalogService _catalogService;
        private readonly IOrderRequestService _requestService;
        private readonly IShipmentService _shipmentService;

        public ProcessOrderAfterPaymentHandler()
            : this(new OrderRepository(), new CatalogService(), new OrderRequestService(), new ShipmentService())
        {
        }

        public ProcessOrderAfterPaymentHandler(IOrderRepository orderRepository, ICatalogService catalogService, IOrderRequestService requestService, IShipmentService shipmentService)
        {
            _orderRepository = orderRepository;
            _catalogService = catalogService;
            _requestService = requestService;
            _shipmentService = shipmentService;
        }

        public OrderProcessedViewModel Handle(ProcessOrderAfterPaymentCommand command)
        {
            // 1. Create order ID
            var tempOrderId = _requestService.GenerateTemporaryOrderId();

            // 2. Register order in the system 
            var order = Domain.Orders.Order.CreateFromShoppingCart(tempOrderId, command.ShoppingCart.OrderRequest);
            var orderId = _orderRepository.AddAndReturnKey(order);

            // 3. Ship 
            var shipmentDetails = _shipmentService.SendRequestForDelivery(order);

            // 4. Update fidelity card and membership status


            // Prepare model
            var model = new OrderProcessedViewModel
            {
                OrderId = orderId.ToString(CultureInfo.InvariantCulture),
                PaymentDetails = { TransactionId = command.TransactionId },
                ShippingDetails = shipmentDetails
            };
            return model;
        }
    }
}