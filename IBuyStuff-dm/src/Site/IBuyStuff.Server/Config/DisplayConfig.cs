using System.Collections.Generic;
using System.Web.WebPages;
using IBuyStuff.Server.Common.Device;

namespace IBuyStuff.Server.Config
{
    public class DisplayConfig
    {
        public static void RegisterModes(IList<IDisplayMode> displayModes)
        {
            var modeDesktop = new DefaultDisplayMode("")
            {
                ContextCondition = (c => c.Request.IsDesktop())
            };
            var modeTablet = new DefaultDisplayMode("tablet")
            {
                ContextCondition = (c => c.Request.IsTablet())
            };
            var modeLegacy = new DefaultDisplayMode("legacy")
            {
                ContextCondition = (c => c.Request.IsLegacy())
            };

            displayModes.Clear();
            displayModes.Add(modeTablet);
            displayModes.Add(modeLegacy);
            displayModes.Add(modeDesktop);
        }
    }
}
