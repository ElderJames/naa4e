using System.Security.Cryptography.X509Certificates;
using IBuyStuff.Domain.Misc;

namespace IBuyStuff.Domain.Repositories
{
    public interface ISubscriberRepository : IRepository<Subscriber>
    {
        int Count();
    }
}