using IBuyStuff.Application.ViewModels.Home;

namespace IBuyStuff.Application.Services
{
    public interface IHomeControllerService
    {
        IndexViewModel LayOutHomePage();
        IndexViewModel NewSubscriber(string email);
    }
}