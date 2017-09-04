namespace IBuyStuff.Domain.Services.Events
{
    public interface IHandler<T>
    {
        bool CanHandle(IDomainEvent eventType);
        void Handle(IDomainEvent eventData);
    }
}