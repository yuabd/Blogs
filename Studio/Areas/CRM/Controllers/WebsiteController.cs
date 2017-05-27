using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Studio.Models.Others;
using Studio.Services;
using Studio.Models;
using Studio.Models.CRM;


namespace Studio.Areas.CRM.Controllers
{
	[Authorize(Roles = "Administrator")]
    public class WebsiteController : Controller
    {
		private WebsiteService websiteService = new WebsiteService();
        //
        // GET: /Website/

        public ActionResult Index(int? page)
        {
			var websites = websiteService.GetWebsites().ToList();
			var model = new Paginated<Website>(websites, page ?? 1, 20);
			ViewBag.Website = "active";

            return View(model);
        }

		public ActionResult Add()
		{
			var website = new Website();
			website.DateCreated = DateTime.Now;
			ViewBag.Website = "active";

			return View(website);
		}

		[HttpPost]
		public ActionResult Add(Website website)
		{
			ViewBag.Website = "active";
			if (ModelState.IsValid)
			{
				websiteService.InsertWebsite(website);
				websiteService.Save();

				return RedirectToAction("Index");
			}

			return View(website);
			
		}

		public ActionResult Edit(int id)
		{
			var website = websiteService.GetWebsite(id);
			ViewBag.Website = "active";

			return View(website);
		}

		[HttpPost]
		public ActionResult Edit(Website website)
		{
			ViewBag.Website = "active";
			if (ModelState.IsValid)
			{
				websiteService.UpdateWebsite(website);
				websiteService.Save();

				return RedirectToAction("Index");
			}

			return View(website);
		}

		public ActionResult Delete(int id)
		{
			websiteService.DeleteWebsite(id);
			websiteService.Save();

			return RedirectToAction("Index");
		}

		public ActionResult Detail(int id, int? page, int? page1)
		{
			ViewBag.Website = "active";
			var website = websiteService.GetWebsite(id);
			var websiteDetails = websiteService.GetWebsiteDetails(id).ToList();
			var websiteUsers = websiteService.GetWebsiteUsers(id).ToList();
			var pwebsiteDetails = new Paginated<WebsiteDetail>(websiteDetails, page ??  1, 10);
			var pwebsiteUsers = new Paginated<WebsiteUser>(websiteUsers, page1 ?? 1, 10);
			var model = new WebsiteViewModel(website, pwebsiteDetails, pwebsiteUsers);

			return View(model);
		}

    }
}
