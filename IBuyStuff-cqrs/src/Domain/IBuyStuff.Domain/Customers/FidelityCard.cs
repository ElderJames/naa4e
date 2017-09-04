namespace IBuyStuff.Domain.Customers
{
    public class FidelityCard
    {
        public static FidelityCard CreateNew(string number, Customer customer)
        {
            var card = new FidelityCard {Number = number, Owner = customer};
            return card;
        }

        protected FidelityCard()
        {
            Number = "";
            Owner = MissingCustomer.Instance;
            Points = 0;
        }

        public string Number { get; private set; }
        public Customer Owner { get; private set; }
        public int Points { get; private set; }

        public int AddPoints(int points)
        {
            Points += points;
            return Points;
        }
    }
}