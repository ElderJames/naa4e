using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using IBuyStuff.Application.InputModels.Login;
using IBuyStuff.Application.Services;
using IBuyStuff.Application.Services.Authentication;
using IBuyStuff.Application.Utils;
using IBuyStuff.Application.ViewModels.Login;
using IBuyStuff.Domain.Customers;
using IBuyStuff.Server.Common.Identity;
using IBuyStuff.Server.Common.Misc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace IBuyStuff.Server.Controllers.Authentication
{
    public class LoginController : BaseIdentityController 
    {
        private readonly ILoginControllerService _service;

        public LoginController()
            : this(new LoginControllerService())
        {
        }
        public LoginController(ILoginControllerService service)
        {
            _service = service;
        }


        #region Action:: SignIn
        [AllowAnonymous]
        [HttpGet]
        [Route("login")]
        public ActionResult SignIn(String returnUrl)
        {
            var model = new LoginInputModel {ReturnUrl = returnUrl, RememberMe = true};
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignIn(LoginInputModel model, String returnUrl)
        {
            // Validate credentials
            var customer = _service.ValidateAndReturn(model);
            if (customer != null)
            {
                var identity = IdentityHelpers.Create(customer.CustomerId, customer.Email, customer.Gender, customer.Avatar); 
                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);
                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError("failed", "Oh snap! Change a few things up and try again!");
            return View(model);
        }
        #endregion

        #region Action:: SignOut
        [Route("logout")]
        public ActionResult SignOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Action:: Twitter 
        [AllowAnonymous]
        public ActionResult Twitter(String returnUrl)
        {
            const string provider = "Twitter";
            return new ChallengeResult(provider,
                Url.Action("TwitterLoginCallback", "Login", new { ReturnUrl = returnUrl }));
        }

        public async Task<ActionResult> TwitterLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("SignIn");
            }             

            return RedirectToAction("AddDetails", "Login",
                new
                {
                    FirstName = "",
                    LastName = "",
                    ReturnUrl = returnUrl,
                    Email = loginInfo.Email,
                    UserName = loginInfo.DefaultUserName
                });
        }
       
        #endregion

        #region Action:: Facebook
        [AllowAnonymous]
        public ActionResult Fb(String returnUrl)
        {
            const string provider = "Facebook";
            return new ChallengeResult(provider,
                Url.Action("FacebookLoginCallback", "Login", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public async Task<ActionResult> FacebookLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("SignIn");
            }

            var fbId = loginInfo.ExternalIdentity.GetUserId();
            var first = String.Empty;
            var last = String.Empty;
            var claimName = loginInfo.ExternalIdentity.Claims.SingleOrDefault(c => c.Type == "urn:facebook:name");
            if (claimName != null)
            {
                var fbName = claimName.Value;
                if (!fbName.IsNullOrEmpty())
                {
                    var tokens = fbName.Split(' ');
                    if (tokens.Length > 0)
                    {
                        first = tokens[0].Trim();
                        last = fbName.Replace(first, "").Trim();
                    }
                }
            }

            return RedirectToAction("AddDetails", "Login",
                new
                {
                    FirstName = first,
                    LastName = last,
                    ReturnUrl = returnUrl,
                    Email = loginInfo.Email,
                    UserName = loginInfo.DefaultUserName,
                    Avatar = String.Format("https://graph.facebook.com/{0}/picture?type=large", fbId)
                });
        }
        #endregion

        #region Action:: Finalize external login

        [AllowAnonymous]
        [ActionName("AddDetails")]
        [HttpGet]
        public ActionResult AddDetailsGet(AddDetailsViewModel model)
        {
            return View(model);
        }

        [AllowAnonymous]
        [ActionName("AddDetails")]
        [HttpPost]
        public ActionResult AddDetailsPost(AddDetailsViewModel model)
        {
            // Create a user too if missing
            var customer = _service.GetCustomer(model.UserName);
            if (customer == null)
            {
                var register = new RegisterInputModel()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    Avatar = model.Avatar
                };
                _service.Register(register);
            }

            var identity = IdentityHelpers.Create(model.UserName, model.Email, model.Gender, model.Avatar);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);
            return RedirectToLocal(model.ReturnUrl);
        }

        #endregion
    }
}