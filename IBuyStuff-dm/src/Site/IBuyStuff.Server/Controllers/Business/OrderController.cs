using System;
using System.Web.Mvc;
using IBuyStuff.Application.InputModels.Order;
using IBuyStuff.Application.Services;
using IBuyStuff.Application.Services.Order;
using IBuyStuff.Application.ViewModels;
using IBuyStuff.Application.ViewModels.Orders;

namespace IBuyStuff.Server.Controllers.Business
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderControllerService _service;
        public OrderController()
            : this(new OrderControllerService())
        {
        }
        public OrderController(IOrderControllerService service)
        {
            _service = service;
        }

        #region Search task

        [Route("orders")]
        [ActionName("SearchUi")]
        public ActionResult SearchMain()
        {
            return View("search-ui", new ViewModelBase());
        }

        [HttpGet]
        [ActionName("Search")]
        public ActionResult SearchResults(int id)
        {
            var model = _service.RetrieveOrderForCustomer(id);
            return View(model);
        }

        [HttpPost]
        [ActionName("Search")]
        public ActionResult SearchCommand(int id = 0)
        {
            // PRG (Post-Redirect-Get) pattern to avoid F5 refresh issues 
            // (and also key step to neatly separate Commands from Queries in the future)
            return RedirectToAction("Search", new {id = id});
        }
        
        [HttpGet]
        [ActionName("Last")]
        public ActionResult LastOrderResults()
        {
            var customerId = User.Identity.Name;
            var model = _service.RetrieveLastOrderForCustomer(customerId);
            return View("Search", model);
        }

        [HttpPost]
        [ActionName("Last")]
        public ActionResult LastOrderCommand(int id = 0)
        {
            // PRG (Post-Redirect-Get) pattern to avoid F5 refresh issues 
            // (and also key step to neatly separate Commands from Queries in the future)
            return RedirectToAction("Last"/*, new { id = id }*/);
        }
        #endregion

        #region New Order task
        [HttpGet]
        public ActionResult New()
        {
            var customerId = User.Identity.Name;
            var shoppingCartModel = _service.CreateShoppingCartForCustomer(customerId);
            shoppingCartModel.EnableEditOnShoppingCart = true;
            SaveCurrentShoppingCart(shoppingCartModel);
            return View("shoppingcart", shoppingCartModel);
        }
        #endregion

        #region Add Item task
        [HttpPost]
        [ActionName("AddTo")]
        public ActionResult AddToShoppingCartCommand(int productId, int quantity=1)
        {
            var cart = RetrieveCurrentShoppingCart();
            cart = _service.AddProductToShoppingCart(cart, productId, quantity);
            SaveCurrentShoppingCart(cart);

            // PRG (Post-Redirect-Get) pattern to avoid F5 refresh issues 
            // (and also key step to neatly separate Commands from Queries in the future)
            return RedirectToAction("AddTo");  
        }

        [HttpGet]
        [ActionName("AddTo")]
        public ActionResult DisplayShoppingCartCommand()
        {
            var cart = RetrieveCurrentShoppingCart();
            cart.EnableEditOnShoppingCart = true;
            return View("shoppingcart", cart);
        }

        #endregion

        #region Remove Order Item task

        [HttpGet]
        [ActionName("Remove")]
        public ActionResult RefreshShoppingCart(int itemIndex = -1)
        {
            return DisplayShoppingCartCommand();
        }

        [HttpPost]
        [ActionName("Remove")]
        public ActionResult RemoveItemFromShoppingCart(int itemIndex = -1)
        {
            if (itemIndex < 0)
                return RedirectToAction("Remove");

            var cart = RetrieveCurrentShoppingCart();
            if (itemIndex >= cart.OrderRequest.Items.Count)
                return RedirectToAction("Remove");

            cart.OrderRequest.Items.RemoveAt(itemIndex);
            SaveCurrentShoppingCart(cart);

            // PRG (Post-Redirect-Get) pattern to avoid F5 refresh issues 
            // (and also key step to neatly separate Commands from Queries in the future)
            return RedirectToAction("Remove");
        }

        #endregion

        #region Checkout

        [HttpGet]
        [ActionName("Checkout")]
        public ActionResult DisplayCheckoutPage()
        {
            // Get details: address, payment
            var cart = RetrieveCurrentShoppingCart();
            cart.EnableEditOnShoppingCart = false;
            return View(cart);
        }

        [HttpPost]
        [ActionName("Checkout")]
        public ActionResult Checkout(CheckoutInputModel checkout)
        {
            // Pre-payment steps
            var cart = RetrieveCurrentShoppingCart();
            var response = _service.ProcessOrderBeforePayment(cart, checkout);
            if (!response.Denied)
            {
                return Redirect(Url.Content("~/fake_payment.aspx?"));
            }

            TempData["ibuy-stuff:denied"] = response;
            return RedirectToAction("Denied");            
        }

        public ActionResult EndCheckout(string tid)
        {
            // Pre-payment steps
            var cart = RetrieveCurrentShoppingCart();
            var response = _service.ProcessOrderAfterPayment(cart, tid);
            var action = response.Denied ? "denied" : "processed";
            return View(action, response);
        }

        public ActionResult Denied()
        {
            var model = TempData["ibuy-stuff:denied"] ?? new OrderProcessingViewModel();
            return View(model);
        }

        public ActionResult Processed()
        {
            return View(new OrderProcessedViewModel());
        }
        #endregion

   
        #region Internal members

        private static string GetShoppingCartName(string customerId)
        {
            return String.Format("I-Buy-Stuff-Cart:{0}", customerId);
        }
        private ShoppingCartViewModel RetrieveCurrentShoppingCart()
        {
            var customerId = User.Identity.Name;
            var cartName = GetShoppingCartName(customerId);
            var cart = Session[cartName] as ShoppingCartViewModel ?? _service.CreateShoppingCartForCustomer(customerId);
            return cart;
        }
        private void SaveCurrentShoppingCart(ShoppingCartViewModel cart)
        {
            var customerId = User.Identity.Name;
            var cartName = GetShoppingCartName(customerId); 
            Session[cartName] = cart;
        }
        #endregion
    }
}