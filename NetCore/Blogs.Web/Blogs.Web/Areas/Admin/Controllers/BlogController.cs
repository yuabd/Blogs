using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogs.Model.DbModels;
using Blogs.Web.Areas.Admin.Models;
using Blogs.Web.JobWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Quartz;
using Microsoft.AspNetCore.Authorization;
using Blogs.Model.ViewModels.Others;
using Microsoft.AspNetCore.Hosting;
using Quartz;
using Blogs.Web.Models;

namespace Blogs.Web.Areas.Admin.Controllers
{
	[Area("Admin"), Authorize]
	public class BlogController : Controller
	{
		private SiteDataContext db;
		private IHttpContextAccessor _accessor;
		//private readonly ISchedulerFactory _schedulerFactory;
		//private IScheduler _scheduler;
		private readonly IHostingEnvironment _hostingEnvironment;

		public BlogController(SiteDataContext _context,
			IHttpContextAccessor accessor
			//,ISchedulerFactory schedulerFactory
			, IHostingEnvironment hostingEnvironment
			)
		{
			db = _context;
			_accessor = accessor;
			//this._schedulerFactory = schedulerFactory;
			_hostingEnvironment = hostingEnvironment;
		}

		//[HttpGet]
		//public async Task<string[]> Get51()
		//{
		//	//1、通过调度工厂获得调度器
		//	_scheduler = await _schedulerFactory.GetScheduler();
		//	//2、开启调度器
		//	//await _scheduler.Start();
		//	//3、创建一个触发器
		//	var trigger = TriggerBuilder.Create()
		//					.WithSimpleSchedule(x => x.WithIntervalInMinutes(1).RepeatForever())//每两秒执行一次
		//					.WithIdentity("trigger2", "group1")
		//					.Build();
		//	//4、创建任务
		//	var jobDetail = JobBuilder.Create<Add51Job>()
		//					.UsingJobData("key1", 1)  //通过在Trigger中添加参数值
		//					.WithIdentity("job", "group")
		//					.Build();
		//	//5、将触发器和任务器绑定到调度器中
		//	var l = await _scheduler.ScheduleJob(jobDetail, trigger);

		//	await _scheduler.Start();


		//	return await Task.FromResult(new string[] { "value1", "value2" });
		//}

		public async Task<IActionResult> Index(string keywords, int? page)
		{
			var blogs = db.Blog.Select(m => new Blog()
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
				MetaKeywords = m.MetaKeywords,
				IsPublic = m.IsPublic
			});

			if (!string.IsNullOrWhiteSpace(keywords))
				blogs = blogs.Where(m => m.BlogTitle.Contains(keywords));

			var pblogs = new Paginated<Blog>(page ?? 1, 25, _accessor);
			await pblogs.Init(blogs.OrderByDescending(m => m.DateCreated));

			return View(pblogs);
		}

		public ActionResult Add()
		{
			var blog = new Blog();
			blog.AuthorID = "1";
			blog.DateCreated = DateTime.Now;
			blog.IsPublic = true;

			IEnumerable<BlogTag> blogTags = null;

			var categories = db.BlogCategory.AsNoTracking().ToList();

			var model = new Admin.Models.BlogViewModel(blog, blogTags, categories);

			return View(model);
		}

		[HttpPost]
		public ActionResult Add(Blog blog, List<BlogTag> blogTags)
		{
			if (ModelState.IsValid)
			{
				//HttpPostedFile file = Request.Files["PictureFile"];

				if (string.IsNullOrEmpty(blog.MetaKeywords))
				{
					foreach (var tag in blogTags)
					{
						if (!string.IsNullOrEmpty(tag.Tag))
							blog.MetaKeywords += tag.Tag + ",";
					}
				}

				if (string.IsNullOrEmpty(blog.PageTitle))
				{
					blog.PageTitle = blog.BlogTitle;
				}

				db.Blog.Add(blog);
				db.SaveChanges();

				// add slug after (depends on ID)
				blog.Slug = blog.BlogID.ToString();

				//var bt = db.BlogTag.Where(b => b.BlogID == blog.BlogID).ToList();

				//foreach (BlogTag tag in bt)
				//{
				//	DeleteBlogTag(tag);
				//}

				foreach (BlogTag tag in blogTags)
				{
					tag.BlogID = blog.BlogID;
					if (!string.IsNullOrEmpty(tag.Tag))
					{
						tag.Tag = Helpers.Utilities.GenerateSlug(tag.Tag, 20);
						db.BlogTag.Add(tag);
					}
				}

				db.SaveChanges();

				return Redirect("/Admin/Blog/Index");
			}
			else
			{
				var categories = db.BlogCategory.AsNoTracking().ToList();

				var model = new Admin.Models.BlogViewModel(blog, blogTags, categories);

				return View(model);
			}
		}

		public ActionResult Edit(int id)
		{
			var blog = db.Blog.Where(m => m.BlogID == id).FirstOrDefault();
			var blogTags = db.BlogTag.Where(m => m.BlogID == id).ToList();
			var categories = db.BlogCategory.AsNoTracking().ToList();
			var model = new Admin.Models.BlogViewModel(blog, blogTags, categories);

			return View(model);
		}

		[HttpPost]
		public ActionResult Edit(Blog blog, List<BlogTag> blogTags)
		{
			if (ModelState.IsValid)
			{
				//HttpPostedFileBase file = Request.Files["PictureFile"];

				if (string.IsNullOrEmpty(blog.MetaKeywords))
				{
					foreach (var tag in blogTags)
					{
						if (!string.IsNullOrEmpty(tag.Tag))
							blog.MetaKeywords += tag.Tag + ",";
					}
				}

				var b = db.Blog.Where(m => m.BlogID == blog.BlogID).FirstOrDefault();

				b.CategoryID = blog.CategoryID;
				b.BlogTitle = blog.BlogTitle;
				b.BlogContent = blog.BlogContent;
				b.DateCreated = blog.DateCreated;
				b.PageTitle = blog.PageTitle;
				b.MetaDescription = blog.MetaDescription;
				b.MetaKeywords = blog.MetaKeywords;
				//b.Slug = blog.BlogID.ToString();
				b.IsPublic = blog.IsPublic;

				var bt = db.BlogTag.Where(m => m.BlogID == blog.BlogID).ToList();

				db.BlogTag.RemoveRange(bt);

				db.SaveChanges();

				foreach (BlogTag tag in blogTags)
				{
					tag.BlogID = blog.BlogID;
					if (!string.IsNullOrEmpty(tag.Tag))
					{
						tag.Tag = Helpers.Utilities.GenerateSlug(tag.Tag, 20);
						db.BlogTag.Add(tag);
					}
				}

				db.SaveChanges();

				return Redirect("/Admin/Blog/Index");
			}
			else
			{
				var categories = db.BlogCategory.AsNoTracking().ToList();
				var model = new Admin.Models.BlogViewModel(blog, blogTags, categories);
				return View(model);
			}
		}

		public ActionResult Delete(int id)
		{
			var blog = db.Blog.Where(m => m.BlogID == id).FirstOrDefault();

			db.Blog.Remove(blog);

			db.SaveChanges();

			return RedirectToAction("Index");
		}

		//
		// Comments

		public ActionResult PendingComments()
		{
			var blogs = (from b in db.Blog.AsNoTracking()
						 where db.BlogComment.AsNoTracking().Any(c => c.IsPublic == false && c.BlogID == b.BlogID)
						 select b).ToList();

			return View(blogs);
		}

		// TODO: is this being used?
		[HttpPost]
		public string AddComment(BlogComment comment)
		{
			if (ModelState.IsValid)
			{
				comment.IsPublic = false;
				comment.DateCreated = DateTime.Now;
				db.BlogComment.Add(comment);

				db.SaveChanges();

				return "Thank you for your comment";
			}
			else
			{
				return "Error";
			}
		}

		public ActionResult ApproveComment(int id)
		{
			var c = db.BlogComment.FirstOrDefault(m => m.CommentID == id);
			c.IsPublic = true;
			//c.BlogID
			db.SaveChanges();

			return RedirectToAction("PendingComments");
		}

		public ActionResult DeleteComment(int id)
		{
			var c = db.BlogComment.AsNoTracking().Where(m => m.CommentID == id).FirstOrDefault();
			db.BlogComment.Remove(c);

			db.SaveChanges();

			return RedirectToAction("PendingComments");
		}

		//
		// GET: /Admin/Blog/Categories

		public ActionResult Categories()
		{
			var categories = db.BlogCategory.AsNoTracking().ToList();

			return View(categories);
		}

		[HttpPost]
		public ActionResult Categories(BlogCategory category)
		{
			if (ModelState.IsValid)
			{
				if (category.CategoryID > 0)
				{
					var bc = db.BlogCategory.Where(m => m.CategoryID == category.CategoryID).FirstOrDefault();

					bc.CategoryName = category.CategoryName;
					bc.PageTitle = category.PageTitle;
					bc.MetaDescription = category.MetaDescription;
					bc.MetaKeywords = category.MetaKeywords;
					bc.LanguageID = category.LanguageID;    // used for multilanguage sites
					bc.SortOrder = category.SortOrder;

					db.SaveChanges();
				}
				else
				{
					if (string.IsNullOrEmpty(category.PageTitle))
					{
						category.PageTitle = category.CategoryName;
					}
					db.BlogCategory.Add(category);

					db.SaveChanges();

					category.Slug = category.CategoryID.ToString();

					db.SaveChanges();
				}

				return Redirect("/Admin/Blog/Categories");
			}
			else
			{
				return Redirect("/Admin/Blog/Categories");
			}
		}

		public ActionResult DeleteCategory(int id)
		{
			var category = db.BlogCategory.Where(m => m.CategoryID == id).FirstOrDefault();

			db.BlogCategory.Remove(category);

			db.SaveChanges();

			return Redirect("/Admin/Blog/Categories");
		}

		public ActionResult UploadPicture(IFormFile filedata)
		{
			xheditorModel model = new xheditorModel();

			try
			{
				if (filedata.Length > 0)
				{
					var fileName = Guid.NewGuid().ToString();
					var file = Helpers.IO.UploadImageFile(filedata.OpenReadStream(), $"Pictures/Blog", fileName);

					model.msg = "/" + file;
				}

				//JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				//return this.Content(javaScriptSerializer.Serialize(model));

				return Json(model);
			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				filedata = null;
			}
		}
	}
}