using IBuyStuff.Domain.Customers;

namespace IBuyStuff.Domain.Services.Events
{
    public class CustomerReachedGoldMemberStatus : IDomainEvent
    {
        public Customer Customer { get; set; }
    }
}