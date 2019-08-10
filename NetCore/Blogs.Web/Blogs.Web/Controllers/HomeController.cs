using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Blogs.Model.DbModels;
using Blogs.Model.ViewModels;
using Blogs.Web.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogs.Web.Controllers
{
	[DomainFilter]
    public class HomeController : Controller
    {
		private SiteDataContext db;

		public HomeController(SiteDataContext _context)
		{
			db = _context;
		}

		//[ResponseCache(Duration = int.MaxValue)]
		public async Task<IActionResult> Index()
        {
			var blogs = await db.Blog.
				Where(m => m.IsPublic == true).Select(m => new Blog()
				{
					AuthorID = m.AuthorID,
					BlogID = m.BlogID,
					//BlogTags = m.BlogTags,
					BlogTitle = m.BlogTitle,
					DateCreated = m.DateCreated,
					CategoryID = m.CategoryID,
					Slug = m.Slug,
					PageVisits = m.PageVisits,
					PageTitle = m.PageTitle,
					MetaDescription = m.MetaDescription,
					MetaKeywords = m.MetaKeywords
				}).OrderByDescending(m => m.DateCreated).Take(6).ToListAsync();

			var ids = blogs.Select(m => m.BlogID).ToList();
			var tags = await db.BlogTag.Where(m => ids.Contains(m.BlogID)).ToListAsync();

			foreach (var item in blogs)
			{
				item.BlogTags = tags.Where(m => m.BlogID == item.BlogID).ToList();
			}


			ViewBag.Links = await db.Link.OrderBy(m => m.SortOrder).ToListAsync();
			
			return View(blogs);
		}

		public IActionResult Error()
		{

			return View();
		}

	}
}