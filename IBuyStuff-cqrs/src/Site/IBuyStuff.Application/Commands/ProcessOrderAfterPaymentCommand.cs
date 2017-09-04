using IBuyStuff.Application.ViewModels.Orders;

namespace IBuyStuff.Application.Commands
{
    public class ProcessOrderAfterPaymentCommand : Command
    {
        public ProcessOrderAfterPaymentCommand(ShoppingCartViewModel cart, string transactionId)
        {
            ShoppingCart = cart;
            TransactionId = transactionId;
        }

        public ShoppingCartViewModel ShoppingCart { get; private set; }
        public string TransactionId { get; private set; }
    }
}