using System.Web.Optimization;

namespace IBuyStuff.Server.Config
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Bundles/Core")
                   .Include("~/Content/Scripts/jquery-1.10.2.min.js",
                            "~/Content/Scripts/ibuystuff.js"));

            bundles.Add(new ScriptBundle("~/Bundles/Bootstrap")
                   .Include("~/Content/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Bundles/Css")
                   .Include("~/Content/Styles/bootstrap.min.css",
                            "~/Content/Styles/site.css"));

#if DEBUG
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
