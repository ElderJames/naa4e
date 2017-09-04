using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Merp.Web.Site.Startup))]
namespace Merp.Web.Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
}
