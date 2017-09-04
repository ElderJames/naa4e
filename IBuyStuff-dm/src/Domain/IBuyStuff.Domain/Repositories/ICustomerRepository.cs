using IBuyStuff.Domain.Customers;

namespace IBuyStuff.Domain.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer FindById(string id);
    }
}