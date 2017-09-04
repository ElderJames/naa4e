using System.Linq;
using System.Web.Mvc;

namespace IBuyStuff.Server.Common
{
    public class MultiDeviceRazorViewEngine : RazorViewEngine
    {
        public MultiDeviceRazorViewEngine()
        {
            var newViewLocations = new[]
            {
                "~/Views/{1}/{0}/{0}.cshtml",
                "~/Views/{1}/{0}/Partials/{0}.cshtml",
                "~/Views/{1}/Partials/{0}.cshtml",
                "~/Views/{1}/Partials/{0}/{0}.cshtml",
                "~/Views/Shared/{0}/{0}.cshtml",
                "~/Views/Shared/Partials/{0}.cshtml",
                "~/Views/Shared/Partials/{0}/{0}.cshtml"
            };

            ViewLocationFormats = ViewLocationFormats.Union(newViewLocations).ToArray();
            MasterLocationFormats = MasterLocationFormats.Union(newViewLocations).ToArray();
            PartialViewLocationFormats = PartialViewLocationFormats.Union(newViewLocations).ToArray();
        }
    }
}