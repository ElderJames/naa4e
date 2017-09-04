using IBuyStuff.Application.Utils;
using IBuyStuff.Application.ViewModels.Home;
using IBuyStuff.Domain.Misc;
using IBuyStuff.Domain.Repositories;
using IBuyStuff.Domain.Services;
using IBuyStuff.Domain.Services.Impl;
using IBuyStuff.Persistence.Repositories;

namespace IBuyStuff.Application.Services.Home
{
    public class HomeControllerService : IHomeControllerService
    {
        private readonly ICatalogService _catalogService;
        private readonly ISubscriberRepository _subscriberRepository;
        private const int NumberOfFeaturedProducts = 3;

        public HomeControllerService()
            : this(new CatalogService(), new SubscriberRepository())
        {
        }

        public HomeControllerService(ICatalogService catalogService, ISubscriberRepository subscriberRepository)
        {
            _catalogService = catalogService;
            _subscriberRepository = subscriberRepository;
        }

        public IndexViewModel LayOutHomePage()
        {
            var model = new IndexViewModel
            {
                Featured = _catalogService.GetFeaturedProducts(NumberOfFeaturedProducts),
                SubscriberCount = _subscriberRepository.Count()
            };
            return model;
        }

        public IndexViewModel NewSubscriber(string email)
        {
            if (!email.IsNullOrEmpty())
                _subscriberRepository.Add(Subscriber.CreateNew(email));
            return LayOutHomePage();
        }
    }
}