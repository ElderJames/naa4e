using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace IBuyStuff.Server.Controllers.Authentication
{
    public class IdentityController<TUser, TDbContext> : Controller 
        where TUser : IdentityUser 
        where TDbContext : IdentityDbContext, new()
    {
        public IdentityController() 
            : this(new UserManager<TUser>(
                new UserStore<TUser>(new TDbContext())))
        {
        }
        public IdentityController(UserManager<TUser> userManager)
        {
            UserManager = userManager;
        }

        protected UserManager<TUser> UserManager { get; set; }

        protected IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        //protected async Task SignInAsync(TUser user, bool isPersistent)
        //{
        //    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);

        //    // PS: DefaultAuthenticationTypes must match what set in IAppBuilder configuration
        //    var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

        //    //var claims = new List<Claim>
        //    //{
        //    //    new Claim(ClaimTypes.Name, user.UserName),
        //    //    new Claim(ClaimTypes.Email, user.Email)
        //    //};
        //    //var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

        //    AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent = isPersistent}, identity);
        //}

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        //protected ClaimsIdentity GetBasicUserIdentity(string name)
        //{
        //    var claims = new List<Claim> { new Claim(ClaimTypes.Name, name) };
        //    return new ClaimsIdentity(claims, 
        //        DefaultAuthenticationTypes.ApplicationCookie);
        //}
	}
}