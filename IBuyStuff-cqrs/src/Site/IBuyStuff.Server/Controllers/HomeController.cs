using System.Web.Mvc;
using IBuyStuff.Application.Services;
using IBuyStuff.Application.Services.Home;

namespace IBuyStuff.Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeControllerService _service;

        public HomeController()
            : this(new HomeControllerService())
        {
        }
        public HomeController(IHomeControllerService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var model = _service.LayOutHomePage();
            return View(model);
        }

        public ActionResult Subscribe(string  email)
        {
            var model = _service.NewSubscriber(email);
            return View("index", model);
        }
    }
}