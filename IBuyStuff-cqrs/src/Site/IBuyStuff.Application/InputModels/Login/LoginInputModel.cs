using IBuyStuff.Application.ViewModels;

namespace IBuyStuff.Application.InputModels.Login
{
    public class LoginInputModel : ViewModelBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
        public string Email { get; set; }
    }
}