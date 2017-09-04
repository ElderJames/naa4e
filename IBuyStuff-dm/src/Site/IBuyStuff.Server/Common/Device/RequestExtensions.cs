using System;
using System.Web;
using IBuyStuff.Application.Utils;

namespace IBuyStuff.Server.Common.Device
{
    public static class HttpRequestBaseExtensions
    {
        public static Boolean IsDesktop(this HttpRequestBase request)
        {
            return true;
        }
        public static Boolean IsLegacy(this HttpRequestBase request)
        {
            return request.Browser.IsMobileDevice;
        }
        public static Boolean IsTablet(this HttpRequestBase request)
        {
            return IsTabletInternal(request.UserAgent);
        }

        #region Internals
        private static Boolean IsTabletInternal(String userAgent)
        {
            return userAgent.ToLower().ContainsAny("ipad", "gt-");
        }
        #endregion
    }
}