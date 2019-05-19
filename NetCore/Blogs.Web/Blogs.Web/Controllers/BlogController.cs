using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogs.Model.DbModels;
using Blogs.Model.ViewModels;
using Blogs.Model.ViewModels.Others;
using Blogs.Models;
using Blogs.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogs.Web.Controllers
{
    public class BlogController : Controller
    {
		private SiteDataContext db;
		private IHttpContextAccessor _accessor;

		public BlogController(SiteDataContext _context, IHttpContextAccessor accessor)
		{
			db = _context;
			_accessor = accessor;
		}

		public IActionResult Index(int? page, string keywords)
        {
			if (!page.HasValue)
			{
				page = 1;
			}

			var blogs = db.Blog.Include(m => m.BlogTags).Where(m => m.IsPublic == true);

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

			var pBlogs = blogs.OrderByDescending(m => m.DateCreated).Skip((page.Value - 1) * 10).Take(10).ToList();

			var categories = db.BlogCategory.ToList();

			var popularTags = (from p in db.BlogTag.AsNoTracking()
							   group p by new { p.Tag } into t
							   orderby t.Count() descending
							   select new Anonymous { Tag = t.Key.Tag, Num = t.Count() }).Take(10).ToList();

			var archives = new List<Archive>();

			var model = new BlogsViewModel(pBlogs, categories, popularTags, archives);
			ViewBag.PageTitle = "帮助中心";
			//ViewBag.Blog = "current";

			if (page.HasValue)
			{
				ViewBag.PageTitle += "_第" + page + "页";
			}

			return View(model);
        }

		public async Task<IActionResult> Category(int id, int? page)
		{
			var category = await db.BlogCategory.Where(m => m.CategoryID == id).FirstOrDefaultAsync();

			if (!page.HasValue)
			{
				page = 1;
			}

			var blogs = db.Blog.Include(m => m.BlogTags).Where(m => m.IsPublic == true && m.CategoryID == id);
			
			ViewBag.Count = blogs.Select(m => m.BlogID).Count();

			var pBlogs = blogs.OrderByDescending(m => m.DateCreated).Skip((page.Value - 1) * 10).Take(10).ToList();

			var categories = db.BlogCategory.ToList();

			var popularTags = (from p in db.BlogTag
							   group p by new { p.Tag } into t
							   orderby t.Count() descending
							   select new Anonymous { Tag = t.Key.Tag, Num = t.Count() }).Take(10).ToList();

			var archives = new List<Archive>();

			var model = new BlogsViewModel(pBlogs, categories, popularTags, archives);
			ViewBag.PageTitle = "帮助中心";
			//ViewBag.Blog = "current";

			if (page.HasValue)
			{
				ViewBag.PageTitle += "_第" + page + "页";
			}

			return View("Index", model);
		}

		public ActionResult Tags(string id, int? page)
		{
			var blogs = (from b in db.Blog.AsNoTracking()
						 join bc in db.BlogTag.AsNoTracking() on b.BlogID equals bc.BlogID
						 where bc.Tag == id && b.IsPublic == true
						 orderby b.DateCreated descending
						 select b);

			ViewBag.Count = blogs.Select(m => m.BlogID).Count();

			var pBlogs = new Paginated<Blog>(blogs, page ?? 1, 8, _accessor);

			var popularTags = (from p in db.BlogTag.AsNoTracking()
							   group p by new { p.Tag } into t
							   orderby t.Count() descending
							   select new Anonymous { Tag = t.Key.Tag, Num = t.Count() }).Take(10).ToList();

			var model = new BlogsViewModel(pBlogs, null, popularTags, null);

			ViewBag.PageTitle = "Tag: " + id;
			if (page.HasValue)
			{
				ViewBag.PageTitle += "_第" + page + "页";
			}

			return View("Index", model);
		}

		//[GenerateStaticFileAttribute]
		public ActionResult Archives(string id, string month, int? page)
		{
			string type = "month";

			if (string.IsNullOrEmpty(month))
			{
				type = "year";
			}

			if (string.IsNullOrEmpty(id))
			{
				id = DateTime.Now.Year.ToString();
			}
			
			if (string.IsNullOrEmpty(month))
			{
				month = "January";
			}

			DateTime fromTime = Convert.ToDateTime(id + "/" + month);

			DateTime toTime = fromTime;
			if (type == "year")
				toTime = fromTime.AddYears(1);
			else if (type == "month")
				toTime = fromTime.AddMonths(1);

			var blogs  = db.Blog.AsNoTracking().Where(b => b.DateCreated >= fromTime && b.DateCreated <= toTime && b.IsPublic == true).OrderByDescending(b => b.DateCreated);

			ViewBag.Count = blogs.Select(m => m.BlogID).Count();

			var pBlogs = new Paginated<Blog>(blogs, page ?? 1, 8, _accessor);
			

			var popularTags = (from p in db.BlogTag.AsNoTracking()
							   group p by new { p.Tag } into t
							   orderby t.Count() descending
							   select new Anonymous { Tag = t.Key.Tag, Num = t.Count() }).Take(10).ToList();

			var model = new BlogsViewModel(pBlogs, null, popularTags, null);

			ViewBag.PageTitle = string.Format("{0}{1}", string.IsNullOrEmpty(id) ? "" : (id + "年"), month);
			if (page.HasValue)
			{
				ViewBag.PageTitle += "_第" + page + "页";
			}
			return View("Index", model);
		}

		//[GenerateStaticFileAttribute]
		public ActionResult Post(int id)
		{
			var blog = db.Blog.Include(m => m.BlogTags).Include(m => m.BlogCategory).AsNoTracking().Where(m => m.BlogID == id).FirstOrDefault();

			if (blog == null)
			{
				//string newurl = "http://www.henhaoji.com.cn" + System.Web.HttpContext.Current.Request.RawUrl;
				//System.Web.HttpContext.Current.Response.Clear();
				//System.Web.HttpContext.Current.Response.StatusCode = 404;
				//System.Web.HttpContext.Current.Response.Status = "404 Moved Permanently";
				//System.Web.HttpContext.Current.Response.AddHeader("Location", "");
				//Response.Redirect("/404.html");
				//return View("NotFound");
				//Response.End();
			}

			//var blogComment = new BlogComment();
			var blogID = blog == null ? 0 : blog.BlogID;
			//blogComment.BlogID = blogID;
			//blogComment.IsPublic = true;
			//blogComment.ValidationCodeSource = DateTime.Now.Millisecond.ToString();

			ViewBag.CommentCount = db.BlogComment.Where(m => m.BlogID == blogID).Where(m => m.IsPublic == true).Select(m => m.BlogID).Count();
			//var categories = bs.GetBlogCategories().ToList();
			var popularTags = (from p in db.BlogTag.AsNoTracking()
							   group p by new { p.Tag } into t
							   orderby t.Count() descending
							   select new Anonymous { Tag = t.Key.Tag, Num = t.Count() }).Take(10).ToList();
			//var archives = bs.GetArchives().ToList();

			var preNextBlog = new PreNextBlog();

			var pre = (from l in db.Blog
					   where l.BlogID < blogID
					   orderby l.BlogID descending
					   select new BlogIDName()
					   {
						   ID = l.BlogID,
						   Slug = l.Slug,
						   Title = l.BlogTitle
					   }).FirstOrDefault();

			var next = (from l in db.Blog
						where l.BlogID > blogID
						orderby l.BlogID
						select new BlogIDName()
						{
							ID = l.BlogID,
							Slug = l.Slug,
							Title = l.BlogTitle
						}).FirstOrDefault();
			preNextBlog.PreBlog = pre;
			preNextBlog.NextBlog = next;

			var model = new BlogViewModel(blog, new BlogComment(), null, null, popularTags, null, preNextBlog);

			ViewBag.Blog = "current";
			return View(model);
		}

		//public ActionResult Captcha()
		//{
		//	Captcha captcha = new Captcha(85, 32, 5, 25f);
		//	Session["Captcha"] = captcha.Text;
		//	return File(captcha.ImageData, "image/jpeg");
		//}

		public ActionResult GetApprovedCommentOfPost(int id)
		{
			var comments = db.BlogComment.AsNoTracking().Where(m => m.IsPublic == true && m.BlogID == id).ToList();

			return View(comments);
		}

		[HttpGet]
		public ActionResult GetCategories()
		{
			var categories = db.BlogCategory.AsNoTracking().ToList();

			return Json(categories);
		}

		//[HttpPost]
		//public ActionResult AddComment(BlogComment blogComment, string CaptchaCode)
		//{
		//	try
		//	{
		//		if (Session["Captcha"] != null && Session["Captcha"].ToString() == CaptchaCode)
		//		{
		//			bs.InsertBlogComment(blogComment);

		//			return Json(new { code = 1 }, JsonRequestBehavior.AllowGet);
		//		}

		//		return Json(new { code = -1 }, JsonRequestBehavior.AllowGet);

		//		throw new Exception();
		//	}
		//	catch (Exception e)
		//	{
		//		throw;
		//	}
		//}
	}
}