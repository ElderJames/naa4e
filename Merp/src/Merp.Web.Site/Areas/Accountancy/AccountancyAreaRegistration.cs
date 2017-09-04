using System.Web.Mvc;

namespace Merp.Web.Site.Areas.Accountancy
{
    public class AccountancyAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Accountancy";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Accountancy_default",
                "Accountancy/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Merp.Web.Site.Areas.Accountancy.Controllers" }
            );
        }
    }
}