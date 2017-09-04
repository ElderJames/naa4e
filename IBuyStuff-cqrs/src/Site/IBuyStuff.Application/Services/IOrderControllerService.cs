using IBuyStuff.Application.ViewModels.Orders;

namespace IBuyStuff.Application.Services
{
    public interface IOrderControllerService
    {
        OrderFoundViewModel RetrieveOrderForCustomer(int orderId);
        OrderFoundViewModel RetrieveLastOrderForCustomer(string customerId);
        ShoppingCartViewModel CreateShoppingCartForCustomer(string customerId);
        ShoppingCartViewModel AddProductToShoppingCart(ShoppingCartViewModel cart, int productId, int quantity);
    }
}