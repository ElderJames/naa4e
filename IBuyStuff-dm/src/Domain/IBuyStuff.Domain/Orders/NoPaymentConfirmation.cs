namespace IBuyStuff.Domain.Orders
{
    public class NoPaymentConfirmation : OrderPaymentConfirmation
    {
        public static NoPaymentConfirmation Instance = new NoPaymentConfirmation();
        private NoPaymentConfirmation() 
        {
        }
    }
}
