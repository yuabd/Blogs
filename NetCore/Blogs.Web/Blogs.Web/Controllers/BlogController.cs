using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogs.Model.DbModels;
using Blogs.Model.ViewModels;
using Blogs.Model.ViewModels.Others;
using Blogs.Web.Filters;
using Blogs.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogs.Web.Controllers
{
	[DomainFilter]
	public class BlogController : Controller
	{
		private SiteDataContext db;
		private IHttpContextAccessor _accessor;

		public BlogController(SiteDataContext _context, IHttpContextAccessor accessor)
		{
			db = _context;
			_accessor = accessor;
		}

		//[ResponseCache(Duration = 3600)]
		public async Task<IActionResult> Index(int? page, string keywords)
		{
			string type = "index";
			if (!page.HasValue)
			{
				page = 1;
			}

			var blogs = from m in db.Blog.AsNoTracking()
						//join t in db.BlogTag.AsNoTracking() on m.BlogID equals t.BlogID
						where m.IsPublic == true
						orderby m.DateCreated descending
						select new Blog()
						{
							AuthorID = m.AuthorID,
							BlogID = m.BlogID,
							//BlogTags = t,
							BlogTitle = m.BlogTitle,
							DateCreated = m.DateCreated,
							CategoryID = m.CategoryID,
							Slug = m.Slug,
							PageVisits = m.PageVisits,
							PageTitle = m.PageTitle,
							MetaDescription = m.MetaDescription,
							MetaKeywords = m.MetaKeywords
						};

			if (!string.IsNullOrEmpty(keywords))
			{
				type = "search";
				var key = keywords.Split(' ');
				foreach (var item in key)
				{
					blogs = (from l in blogs
							 where l.BlogTitle.Contains(item)// || l.BlogContent.Contains(item)
							 select l);
				}
				ViewBag.Keywords = keywords;
			}

			ViewBag.Count = await blogs.Select(m => m.BlogID).CountAsync();

			var pBlogs = new Paginated<Blog>(page ?? 1, 10, _accessor);
			await pBlogs.Init(blogs.OrderByDescending(m => m.DateCreated));

			var popularTags = await (from p in db.BlogTag.AsNoTracking()
									 group p by new { p.Tag } into t
									 orderby t.Count() descending
									 select new Anonymous { Tag = t.Key.Tag, Num = t.Count() }).Take(10).ToListAsync();

			var ids = pBlogs.Select(m => m.BlogID).ToList();
			var tags = await db.BlogTag.Where(m => ids.Contains(m.BlogID)).ToListAsync();

			foreach (var item in pBlogs)
			{
				item.BlogTags = tags.Where(m => m.BlogID == item.BlogID).ToList();
			}

			var archives = new List<Archive>();

			var model = new BlogsViewModel(pBlogs, null, popularTags, archives);
			ViewBag.PageTitle = "Pos机文档";
			//ViewBag.Blog = "current";
			ViewBag.Type = type;

			await GetHotAndNewBlogs();

			if (page.HasValue && page.Value > 1)
			{
				ViewBag.PageTitle += "_第" + page + "页";
			}

			return View(model);
		}

		//[ResponseCache(Duration = int.MaxValue)]
		public async Task<IActionResult> Category(int id, int? page)
		{
			var category = await db.BlogCategory.Where(m => m.CategoryID == id).FirstOrDefaultAsync();

			if (!page.HasValue)
			{
				page = 1;
			}

			var blogs = db.Blog.Where(m => m.IsPublic == true && m.CategoryID == id)
				.OrderByDescending(m => m.DateCreated).Select(m => new Blog()
				{
					AuthorID = m.AuthorID,
					BlogID = m.BlogID,
					BlogTitle = m.BlogTitle,
					DateCreated = m.DateCreated,
					CategoryID = m.CategoryID,
					Slug = m.Slug,
					PageVisits = m.PageVisits,
					PageTitle = m.PageTitle,
					MetaDescription = m.MetaDescription,
					MetaKeywords = m.MetaKeywords
				});

			ViewBag.Count = await blogs.Select(m => m.BlogID).CountAsync();

			var pBlogs =
				new Paginated<Blog>(page ?? 1, 10, _accessor); //blogs.OrderByDescending(m => m.DateCreated).Skip((page.Value - 1) * 10).Take(10).ToList();
			await pBlogs.Init(blogs);

			var popularTags = await (from p in db.BlogTag.AsNoTracking()
									 group p by new { p.Tag } into t
									 orderby t.Count() descending
									 select new Anonymous { Tag = t.Key.Tag, Num = t.Count() }).Take(10).ToListAsync();

			var ids = pBlogs.Select(m => m.BlogID).ToList();
			var tags = await db.BlogTag.Where(m => ids.Contains(m.BlogID)).ToListAsync();

			foreach (var item in pBlogs)
			{
				item.BlogTags = tags.Where(m => m.BlogID == item.BlogID).ToList();
			}

			var archives = new List<Archive>();

			var model = new BlogsViewModel(pBlogs, null, popularTags, archives);
			ViewBag.PageTitle = category.CategoryName;
			ViewBag.Type = "category";
			ViewBag.Category = category;

			await GetHotAndNewBlogs();

			if (page.HasValue && page.Value > 1)
			{
				ViewBag.PageTitle += "_第" + page + "页";
			}

			return View("Index", model);
		}

		//[ResponseCache(Duration = 3600)]
		public async Task<IActionResult> Tags(string id, int? page)
		{
			var blogs = (from m in db.Blog.AsNoTracking()
						 join bc in db.BlogTag.AsNoTracking() on m.BlogID equals bc.BlogID
						 where bc.Tag == id && m.IsPublic == true
						 orderby m.DateCreated descending
						 select new Blog()
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
						 });

			ViewBag.Count = await blogs.Select(m => m.BlogID).CountAsync();

			var pBlogs = new Paginated<Blog>(page ?? 1, 10, _accessor);
			await pBlogs.Init(blogs);

			var popularTags = await (from p in db.BlogTag.AsNoTracking()
									 group p by new { p.Tag } into t
									 orderby t.Count() descending
									 select new Anonymous { Tag = t.Key.Tag, Num = t.Count() }).Take(10).ToListAsync();

			var ids = pBlogs.Select(m => m.BlogID).ToList();
			var tags = await db.BlogTag.Where(m => ids.Contains(m.BlogID)).ToListAsync();

			foreach (var item in pBlogs)
			{
				item.BlogTags = tags.Where(m => m.BlogID == item.BlogID).ToList();
			}

			var model = new BlogsViewModel(pBlogs, null, popularTags, null);

			ViewBag.PageTitle = id;
			if (page.HasValue && page.Value > 1)
			{
				ViewBag.PageTitle += "_第" + page + "页";
			}
			ViewBag.Type = "Tag";
			ViewBag.Tag = id;

			await GetHotAndNewBlogs();

			return View("Index", model);
		}

		//[GenerateStaticFileAttribute]
		//[ResponseCache(Duration = 3600)]
		public async Task<IActionResult> Archives(string id, string month, int? page)
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

			var blogs = db.Blog.AsNoTracking().Where(b => b.DateCreated >= fromTime && b.DateCreated <= toTime && b.IsPublic == true).OrderByDescending(b => b.DateCreated);

			ViewBag.Count = await blogs.Select(m => m.BlogID).CountAsync();

			var pBlogs = new Paginated<Blog>(page ?? 1, 10, _accessor);
			await pBlogs.Init(blogs);

			var popularTags = await (from p in db.BlogTag.AsNoTracking()
									 group p by new { p.Tag } into t
									 orderby t.Count() descending
									 select new Anonymous { Tag = t.Key.Tag, Num = t.Count() }).Take(10).ToListAsync();

			var model = new BlogsViewModel(pBlogs, null, popularTags, null);

			ViewBag.PageTitle = string.Format("{0}{1}", string.IsNullOrEmpty(id) ? "" : (id + "年"), month);
			if (page.HasValue && page.Value > 1)
			{
				ViewBag.PageTitle += "_第" + page + "页";
			}
			return View("Index", model);
		}

		//[GenerateStaticFileAttribute]
		//[ResponseCache(Duration = int.MaxValue)]
		public async Task<ActionResult> Post(int id)
		{
			var blog = await db.Blog.Include(m => m.BlogTags).Include(m => m.BlogCategory).Where(m => m.BlogID == id).FirstOrDefaultAsync();

			if (blog == null)
			{
				string newurl = "http://www.henhaoji.com.cn" + _accessor.HttpContext.Request.QueryString.ToString();
				_accessor.HttpContext.Response.Clear();
				_accessor.HttpContext.Response.StatusCode = 404;
				_accessor.HttpContext.Response.Headers.TryAdd("Location", "");
				_accessor.HttpContext.Response.Redirect("/Home/Error");
				//return View("/Home/Error");
				//_accessor.HttpContext.Response.();
			}
			else
			{
				blog.PageVisits += 1;
				await db.SaveChangesAsync();
			}

			var blogID = blog == null ? 0 : blog.BlogID;

			ViewBag.CommentCount = 0;// await db.BlogComment.Where(m => m.BlogID == blogID && m.IsPublic == true).Select(m => m.BlogID).CountAsync();
									 //var categories = bs.GetBlogCategories().ToList();
			var popularTags = await (from p in db.BlogTag.AsNoTracking()
									 group p by new { p.Tag } into t
									 orderby t.Count() descending
									 select new Anonymous { Tag = t.Key.Tag, Num = t.Count() }).Take(10).ToListAsync();
			//var archives = bs.GetArchives().ToList();

			var preNextBlog = new PreNextBlog();

			var pre = await (from l in db.Blog.AsNoTracking()
							 where l.BlogID < blogID && l.IsPublic == true
							 orderby l.BlogID descending
							 select new BlogIDName()
							 {
								 ID = l.BlogID,
								 Slug = l.Slug,
								 Title = l.BlogTitle
							 }).FirstOrDefaultAsync();

			var next = await (from l in db.Blog.AsNoTracking()
							  where l.BlogID > blogID && l.IsPublic == true
							  orderby l.BlogID
							  select new BlogIDName()
							  {
								  ID = l.BlogID,
								  Slug = l.Slug,
								  Title = l.BlogTitle
							  }).FirstOrDefaultAsync();

			preNextBlog.PreBlog = pre;
			preNextBlog.NextBlog = next;

			await GetHotAndNewBlogs();

			var model = new BlogViewModel(blog, popularTags, preNextBlog);

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

		[HttpGet]//,ResponseCache(Duration = int.MaxValue)]
		public async Task<IActionResult> GetCategories()
		{
			var categories = await db.BlogCategory.AsNoTracking().ToListAsync();

			return Json(categories);
		}

		private async Task GetHotAndNewBlogs()
		{
			ViewBag.Hots = await db.Blog.AsNoTracking().Where(m => m.IsPublic).OrderByDescending(m => m.PageVisits).Take(6).ToListAsync();
			ViewBag.News = await db.Blog.AsNoTracking().Where(m => m.IsPublic).OrderByDescending(m => m.DateCreated).Take(6).ToListAsync();
		}

		/// <summary>
		/// 赞
		/// </summary>
		/// <returns></returns>
		[Route("~/like/{id}")]
		public async Task<ActionResult> Like(int id)
		{
			var ses = HttpContext.Request.Cookies["like-" + id];

			if (ses == null)
			{
				var movie = await db.Blog.Where(m => m.BlogID == id).FirstOrDefaultAsync();

				movie.Like += 1;

				db.SaveChanges();

				HttpContext.Response.Cookies.Append("like-" + id, id.ToString());

				return Json(new { IsSuccess = 1 });
			}

			return Json(new { Message = "你已经点过赞了！" });
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