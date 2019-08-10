using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogs.Model.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace Blogs.Web.Controllers
{
    public class SitemapController : Controller
    {
		private SiteDataContext db;

		public SitemapController(SiteDataContext _db)
		{
			db = _db;
		}

        public IActionResult Index()
        {
			ViewBag.Category = db.BlogCategory.ToList();

			ViewBag.Blogs = db.Blog.Where(m => m.IsPublic)
				.Select(m => new Blog()
				{
					BlogID = m.BlogID,
					BlogTitle = m.BlogTitle,
					MetaKeywords = m.MetaKeywords
				}).ToList();

			//ViewBag.mccs = db.POS_MCC.ToList();

			//ViewBag.Areas = db.POS_MerchantArea.ToList();

			//ViewBag.Agency = db.POS_Agency.ToList();

			//ViewBag.License = db.POS_PaymentLicense.ToList();

			return View();
        }
    }
}