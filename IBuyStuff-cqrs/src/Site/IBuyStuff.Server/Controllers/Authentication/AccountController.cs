using System.Web.Mvc;
using IBuyStuff.Application.InputModels.Login;
using IBuyStuff.Application.Services;
using IBuyStuff.Application.Services.Authentication;
using IBuyStuff.Server.Common.Identity;
using Microsoft.Owin.Security;

namespace IBuyStuff.Server.Controllers.Authentication
{
    public class AccountController : BaseIdentityController 
    {
        private readonly ILoginControllerService _service;

        public AccountController()
            : this(new LoginControllerService())
        {
        }
        public AccountController(ILoginControllerService service)
        {
            _service = service;
        }

       #region Action:: Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            var model = new RegisterInputModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterInputModel model)
        {
            // Add customer and sign in
            var succeeded = _service.Register(model);
            if (succeeded)
            {
                var identity = IdentityHelpers.Create(model.UserName, model.Email, model.Gender);
                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("failed", "Oh snap! Change a few things up and try again!");
            return View(model);
        }
        #endregion
    }
}