using Studio.Models;
using Studio.Models.Others;
using Studio.Models.Site;
using Studio.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Studio.Controllers
{
    [TestFilter]
    public class HomeController : Controller
    {
        private BlogService bs = new BlogService();

        // GET: Home
        public ActionResult Index(int? page, string keywords)
        {
            var blogs = bs.GetBlogs().Where(m => m.IsPublic == true);

            if (!string.IsNullOrEmpty(keywords))
            {
                var key = keywords.Split(' ');
                foreach (var item in key)
                {
                    blogs = (from l in blogs
                             where l.BlogTitle.Contains(item) || l.BlogContent.Contains(item)
                             select l);
                }
            }

            ViewBag.Count = blogs.Select(m => m.BlogID).Count();

            var pBlogs = new Paginated<Blog>(blogs.ToList(), page ?? 1, 8);

            var categories = bs.GetBlogCategories().ToList();

            var popularTags = (from p in bs.GetTags()
                               group p by new { p.Tag } into t
                               orderby t.Count() descending
                               select new Anonymous { Tag = t.Key.Tag, Num = t.Count() }).Take(10).ToList();

            var archives = bs.GetArchives().ToList();

            var model = new BlogsViewModel(pBlogs, categories, popularTags, archives);
            ViewBag.PageTitle = "yuabd's Blog";
            ViewBag.Blog = "current";

            return View("~/Views/Blog/Index.cshtml",model);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}