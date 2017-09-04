using IBuyStuff.Application.InputModels.Login;
using IBuyStuff.Domain.Customers;

namespace IBuyStuff.Application.Services
{
    public interface ILoginControllerService
    {
        Customer ValidateAndReturn(LoginInputModel model);
        Customer GetCustomer(string userName);
        bool Register(RegisterInputModel model);
    }
}