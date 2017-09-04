using IBuyStuff.Application.InputModels.Order;
using IBuyStuff.Application.ViewModels.Orders;

namespace IBuyStuff.Application.Commands
{
    public class ProcessOrderBeforePaymentCommand : Command
    {
        public ProcessOrderBeforePaymentCommand(ShoppingCartViewModel cart, CheckoutInputModel checkout)
        {
            ShoppingCart = cart;
            CheckoutData = checkout;
        }

        public ShoppingCartViewModel ShoppingCart { get; private set; }
        public CheckoutInputModel CheckoutData { get; private set; }
    }
}