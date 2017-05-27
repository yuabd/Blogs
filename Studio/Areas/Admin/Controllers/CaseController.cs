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
	[Authorize(Roles = "Administrator")]
    public class CaseController : Controller
    {
		//private SiteService siteService = new SiteService();
        //
        // GET: /Admin/Case/

        public ActionResult Index(int? page)
        {
            var cases = new SiteHelp().GetCases().ToList();
			var model = new Paginated<Case>(cases, page ?? 1, 10);

            return View(model);
        }

		public ActionResult Add()
		{
			var _case = new Case();

			return View(_case);
		}

		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Add(Case _case, HttpPostedFileBase file)
		{
			if (ModelState.IsValid)
			{
                new SiteHelp().InsertCase(_case, file);
				//siteService.Save();

				return RedirectToAction("Edit", new { id = _case.CaseID });
			}

			return View(_case);
		}

		public ActionResult Edit(int id)
		{
            var _case = new SiteHelp().GetCase(id);

			return View(_case);
		}

		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Edit(Case _case, HttpPostedFileBase file)
		{
			if (ModelState.IsValid)
			{
                new SiteHelp().UpdateCase(_case, file);
				//siteService.Save();

				return RedirectToAction("Index");
			}

			return View(_case);
		}

		public ActionResult Delete(int id)
		{
            new SiteHelp().Delete(id);
			//siteService.Save();

			return RedirectToAction("Index");
		}

		public ActionResult CaseCategories(int? page)
		{
            var categories = new SiteHelp().GetCaseCategories().ToList();
			var model = new Paginated<CaseCategory>(categories, page ?? 1, 20);

			return View(model);
		}

		[HttpPost]
		public ActionResult CaseCategories(CaseCategory category)
		{
			if (ModelState.IsValid)
			{
				if (category.CategoryID > 0)
				{
                    new SiteHelp().UpdateCaseCategory(category);
					//siteService.Save();
				}
				else
				{
                    new SiteHelp().InsertCaseCategory(category);
                    //new SiteHelp().Save();
				}

				return RedirectToAction("CaseCategories");
			}

			return RedirectToAction("CaseCategories");
		}

		public ActionResult DeleteCategory(int id)
		{
            new SiteHelp().DeleteCaseCategory(id);
			//siteService.Save();

			return RedirectToAction("CaseCategories");
		}

    }
}
