using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.WebPages;
using IBuyStuff.Application;
using IBuyStuff.Application.Commands;
using IBuyStuff.Application.Handlers;
using IBuyStuff.Server.Common;
using IBuyStuff.Server.Config;

namespace IBuyStuff.Server
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DisplayConfig.RegisterModes(DisplayModeProvider.Instance.Modes);

            // Register commands
            CommandProcessor
                .RegisterHandler<ProcessOrderBeforePaymentCommand, ProcessOrderBeforePaymentHandler>();
            CommandProcessor
                .RegisterHandler<ProcessOrderAfterPaymentCommand, ProcessOrderAfterPaymentHandler>();
        }
    }
}
