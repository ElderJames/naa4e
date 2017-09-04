using IBuyStuff.Domain.Shared;

namespace IBuyStuff.Application.ViewModels.Login
{
    public class AddDetailsViewModel : ViewModelBase
    {
        public string ReturnUrl { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public Gender Gender { get; set; }
    }
}