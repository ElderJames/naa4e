using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;

namespace IBuyStuff.Server.Controllers.Authentication
{
    public class BaseIdentityController : Controller
    {
        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        protected IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
	}
}