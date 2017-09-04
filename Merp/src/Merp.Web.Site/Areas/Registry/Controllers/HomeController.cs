using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Merp.Web.Site.Areas.Registry.Controllers
{
    public class HomeController : Controller
    {
        // GET: Registry/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}