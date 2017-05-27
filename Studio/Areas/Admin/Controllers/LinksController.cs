using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Studio.Services;
using Studio.Models.Others;
using Studio.Models;

namespace Studio.Areas.Admin.Controllers
{
    public class LinksController : Controller
    {
        //
        // GET: /Admin/Links/

        public ActionResult Index(int? page)
        {
			var links = new SiteHelp().GetLinks();
			var model = new Paginated<Links>(links, page ?? 1, 20);

			return View(model);
        }

		public ActionResult Add()
		{
			var link = new Links();
			link.SortOrder = 0;

			return View(link);
		}

		[HttpPost]
		public ActionResult Add(Links links)
		{
			if (ModelState.IsValid)
			{
				var obj = new SiteHelp().InsertLink(links);
				if (obj.Tag <= 0)
				{
					ViewBag.Error = obj.Message;
					return View(links);
				}

				return RedirectToAction("Index");
			}

			return View(links);
		}

		public ActionResult Edit(int id)
		{
			var link = new SiteHelp().GetLink(id);

			return View(link);
		}

		[HttpPost]
		public ActionResult Edit(Links links)
		{
			if (ModelState.IsValid)
			{
				var obj = new SiteHelp().UpdateLink(links);
				if (obj.Tag <= 0)
				{
					ViewBag.Error = obj.Message;
					return View(links);
				}

				return RedirectToAction("Index");
			}

			return View(links);
		}

		public ActionResult Delete(int id)
		{
			var obj = new SiteHelp().DeleteLink(id);
			if (obj.Tag <= 0)
			{
				ViewBag.Error = obj.Message;
			}
			return RedirectToAction("Index");
		}

    }
}
