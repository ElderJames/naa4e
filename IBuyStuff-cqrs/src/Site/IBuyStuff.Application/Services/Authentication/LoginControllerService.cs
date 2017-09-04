using IBuyStuff.Application.InputModels.Login;
using IBuyStuff.Application.Utils;
using IBuyStuff.Domain.Customers;
using IBuyStuff.Domain.Repositories;
using IBuyStuff.Infrastructure;
using IBuyStuff.Infrastructure.Hashing;
using IBuyStuff.Persistence.Repositories;

namespace IBuyStuff.Application.Services.Authentication
{
    public class LoginControllerService : ILoginControllerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IHashingService _hashingService;

        public LoginControllerService() : this(new CustomerRepository(), new DefaultPasswordHasher())
        {
        }
        public LoginControllerService(ICustomerRepository customerRepository, IHashingService hashingService)
        {
            _customerRepository = customerRepository;
            _hashingService = hashingService;
        }

        public Customer ValidateAndReturn(LoginInputModel model)
        {
            var customer = _customerRepository.FindById(model.UserName);
            if (customer != null)
            {
                if (model.Password.IsNullOrEmpty())
                    return customer;
                if (_hashingService.Validate(model.Password, customer.PasswordHash))
                    return customer;
            } 
            return null;
        }

        public Customer GetCustomer(string userName)
        {
            return _customerRepository.FindById(userName);
        }

        public bool Register(RegisterInputModel model)
        {
            if (model.IsValid())
                return false;

            var customer = Customer.CreateNew(model.Gender, model.UserName, model.FirstName, model.LastName, model.Email);
            customer.SetAvatar(model.Avatar);
            var hash = _hashingService.Hash(model.Password);
            customer.SetPasswordHash(hash);
            return _customerRepository.Add(customer);
        }
    }
}