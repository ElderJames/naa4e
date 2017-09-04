using System.Web.Mvc;

namespace Merp.Web.Site.Areas.Registry
{
    public class RegistryAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Registry";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Registry_default",
                "Registry/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Merp.Web.Site.Areas.Registry.Controllers" }
            );
        }
    }
}