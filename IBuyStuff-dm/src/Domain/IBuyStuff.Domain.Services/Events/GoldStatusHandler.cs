namespace IBuyStuff.Domain.Services.Events
{
    public class GoldStatusHandler : IHandler<IDomainEvent>
    {
        public void Handle(IDomainEvent eventData)
        {
            return;
        }

        public bool CanHandle(IDomainEvent eventType)
        {
            return eventType is CustomerReachedGoldMemberStatus;
        }
    }
}