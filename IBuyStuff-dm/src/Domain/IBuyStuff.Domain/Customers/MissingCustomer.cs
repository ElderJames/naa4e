namespace IBuyStuff.Domain.Customers
{
    public class MissingCustomer : Customer
    {
        public static MissingCustomer Instance = new MissingCustomer();
    }
}
