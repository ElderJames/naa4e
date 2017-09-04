namespace IBuyStuff.Domain.Misc
{
    public class Subscriber
    {
        public static Subscriber CreateNew(string email)
        {
            var subscriber = new Subscriber {Email = email};
            return subscriber;
        }

        protected Subscriber()
        {
        }

        public int Id { get; private set; }
        public string Email { get; private set; } 
    }
}