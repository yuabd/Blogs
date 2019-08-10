using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Blogs.Model.DbModels;
using Blogs.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Blogs.Web.Areas.Admin.Controllers
{
	[Area("admin"), Authorize]
    public class LinksController : Controller
    {
		private SiteDataContext db;
		private IHttpContextAccessor _accessor;
		public LinksController(SiteDataContext _db,
			IHttpContextAccessor accessor)
		{
			db = _db;
			_accessor = accessor;
		}

		public async Task<ActionResult> Index(int? page)
		{
			var links = db.Link;
			var model = new Paginated<Links>(page ?? 1, 20, _accessor);
			await model.Init(links.OrderBy(m => m.SortOrder));

			return View(model);
		}

		public ActionResult Add()
		{
			var link = new Links();
			link.SortOrder = 0;

			return View(link);
		}

		[HttpPost]
		public async Task<ActionResult> Add(Links links)
		{
			if (ModelState.IsValid)
			{
				var obj = await db.Link.AddAsync(links); //new SiteHelp().InsertLink(links);
				await db.SaveChangesAsync();
				//if (obj.Tag <= 0)
				//{
				//	ViewBag.Error = obj.Message;
				//	return View(links);
				//}

				return Redirect("/Admin/Links");
			}

			return View(links);
		}

		public async Task<ActionResult> Edit(int id)
		{
			var link = await db.Link.FirstOrDefaultAsync(m => m.ID == id); //new SiteHelp().GetLink(id);

			return View(link);
		}

		[HttpPost]
		public ActionResult Edit(Links links)
		{
			if (ModelState.IsValid)
			{
				var obj = db.Link.Update(links);
				db.SaveChanges();

				return Redirect("/Admin/Links");
			}

			return View(links);
		}

		public async Task<ActionResult> Delete(int id)
		{
			var obj = await db.Link.FirstOrDefaultAsync(m => m.ID == id);//new SiteHelp().DeleteLink(id);
			if (obj == null)
			{
				ViewBag.Error = "未找到数据";
			}

			db.Link.Remove(obj);
			await db.SaveChangesAsync();

			return Redirect("/Admin/Links");
		}
	}
}