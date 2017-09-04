using Merp.Web.Site.Areas.Accountancy.Models.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Merp.Web.Site.Areas.Accountancy.Controllers
{
    public class HomeController : Controller
    {
        // GET: Accountancy/Home
        public ActionResult Index()
        {
            return View();
        }


    }
}