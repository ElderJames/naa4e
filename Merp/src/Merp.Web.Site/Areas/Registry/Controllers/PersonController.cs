using Merp.Web.Site.Areas.Registry.Models.Person;
using Merp.Web.Site.Areas.Registry.WorkerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Merp.Web.Site.Areas.Registry.Controllers
{
    public class PersonController : Controller
    {
        public PersonControllerWorkerServices WorkerServices { get; private set; }

        public PersonController(PersonControllerWorkerServices workerServices)
        {
            if(workerServices==null)
                throw new ArgumentNullException(nameof(workerServices));

            WorkerServices = workerServices;
        }

        [HttpGet]
        public ActionResult AddEntry()
        {
            var model = new AddEntryViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddEntry(AddEntryViewModel model)
        {
            if(!this.ModelState.IsValid)
            {
                return View(model);
            }
            WorkerServices.AddEntry(model);
            return Redirect("/Registry/");
        }

        [HttpGet]
        public ActionResult Info(Guid? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(404);
            var model = WorkerServices.GetInfoViewModel(id.Value);
            return View(model);
        }
    }
}