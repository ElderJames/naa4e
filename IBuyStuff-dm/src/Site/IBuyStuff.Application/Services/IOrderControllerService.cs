using IBuyStuff.Application.InputModels.Order;
using IBuyStuff.Application.ViewModels.Orders;

namespace IBuyStuff.Application.Services
{
    public interface IOrderControllerService
    {
        OrderFoundViewModel RetrieveOrderForCustomer(int orderId);
        OrderFoundViewModel RetrieveLastOrderForCustomer(string customerId);
        ShoppingCartViewModel CreateShoppingCartForCustomer(string customerId);
        ShoppingCartViewModel AddProductToShoppingCart(ShoppingCartViewModel cart, int productId, int quantity);
        OrderProcessingViewModel ProcessOrderBeforePayment(ShoppingCartViewModel cart, CheckoutInputModel checkout);
        OrderProcessedViewModel ProcessOrderAfterPayment(ShoppingCartViewModel cart, string transactionId);
    }
}