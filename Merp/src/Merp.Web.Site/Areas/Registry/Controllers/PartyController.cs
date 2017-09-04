﻿using Merp.Web.Site.Areas.Registry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMate.Web.Mvc;
using Merp.Web.Site.Areas.Registry.WorkerServices;
using Merp.Web.Site.Support;

namespace Merp.Web.Site.Areas.Registry.Controllers
{
    public class PartyController : Controller
    {
        public PartyControllerWorkerServices WorkerServices { get; private set; }

        public PartyController(PartyControllerWorkerServices workerServices)
        {
            if(workerServices==null)
                throw new ArgumentNullException(nameof(workerServices));

            WorkerServices = workerServices;
        }

        [HttpGet]
        public ActionResult Detail(Guid? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(400);
            switch (WorkerServices.GetDetailViewModel(id.Value))
            {
                case "Company":
                    return Redirect(UrlBuilder.Registry.CompanyInfo(id.Value));
                case "Person":
                    return Redirect(UrlBuilder.Registry.PersonInfo(id.Value));
                default:
                    return RedirectToAction("Search");
            }
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetParties(string query)
        {
            var model = WorkerServices.GetParties(query);
            return this.Jsonp(model);
        }

        [HttpGet]
        public ActionResult GetPartyInfoByPattern(string text)
        {
            var model = WorkerServices.GetPartyNamesByPattern(text);
            return this.Jsonp(model);
        }

        [HttpGet]
        public ActionResult GetPartyInfoById(int id)
        {
            var model = WorkerServices.GetPartyInfoByPattern(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPersonInfoByPattern(string text)
        {
            var model = WorkerServices.GetPersonNamesByPattern(text);
            return this.Jsonp(model);
        }

        [HttpGet]
        public ActionResult GetPersonInfoById(int id)
        {
            var model = WorkerServices.GetPersonInfoByPattern(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}