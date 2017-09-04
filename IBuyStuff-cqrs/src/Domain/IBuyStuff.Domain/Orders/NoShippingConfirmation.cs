namespace IBuyStuff.Domain.Orders
{
    public class NoShippingConfirmation : OrderShippingConfirmation
    {
        public static NoShippingConfirmation Instance = new NoShippingConfirmation();
        private NoShippingConfirmation()  
        {
        }
    }
}
