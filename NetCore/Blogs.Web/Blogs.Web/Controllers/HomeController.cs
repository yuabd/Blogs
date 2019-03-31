using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogs.Model.DbModels;
using Blogs.Model.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blogs.Web.Controllers
{
    public class HomeController : Controller
    {
		private SiteDataContext db;

		public HomeController(SiteDataContext _context)
		{
			db = _context;
		}

        public IActionResult Index()
        {
			var blogs = db.Blog.Where(m => m.IsPublic == true).OrderByDescending(m => m.DateCreated).Take(10).ToList();

			//if (!string.IsNullOrEmpty(keywords))
			//{
			//	var key = keywords.Split(' ');
			//	foreach (var item in key)
			//	{
			//		blogs = (from l in blogs
			//				 where l.BlogTitle.Contains(item) || l.BlogContent.Contains(item)
			//				 select l);
			//	}
			//}

			//ViewBag.Count = blogs.Select(m => m.BlogID).Count();

			//var pBlogs = new Paginated<Blog>(blogs.ToList(), page ?? 1, 8);

			////var categories = bs.GetBlogCategories().ToList();

			////var popularTags = (from p in bs.GetTags()
			////				   group p by new { p.Tag } into t
			////				   orderby t.Count() descending
			////				   select new Anonymous { Tag = t.Key.Tag, Num = t.Count() }).Take(10).ToList();

			////var archives = bs.GetArchives().ToList();

			//var model = new BlogsViewModel(pBlogs, null, , null);
			//ViewBag.PageTitle = "yuabd's Blog";
			////ViewBag.Blog = "current";

			//if (page.HasValue)
			//{
			//	ViewBag.PageTitle += "_第" + page + "页";
			//}

			//return View("~/Views/Blog/Index.cshtml", model);

			return View(blogs);
		}

		public IActionResult Error()
		{

			return View();
		}

	}
}