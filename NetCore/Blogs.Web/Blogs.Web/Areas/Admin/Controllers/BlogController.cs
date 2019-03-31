using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogs.Model.DbModels;
using Blogs.Models;
using Blogs.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogs.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
    public class BlogController : Controller
    {
		private SiteDataContext db;
		private IHttpContextAccessor _accessor;

		public BlogController(SiteDataContext _context, IHttpContextAccessor accessor)
		{
			db = _context;
			_accessor = accessor;
		}

        public IActionResult Index(int? page)
        {
			var blogs = db.Blog.OrderByDescending(m => m.DateCreated);
			var pblogs = new Paginated<Blog>(blogs, page ?? 1, 25, _accessor);

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

			var model = new BlogViewModel(blog, blogTags, categories);

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

				return RedirectToAction("Index", new { Area = "Admin" });
			}
			else
			{
				var categories = db.BlogCategory.AsNoTracking().ToList();

				var model = new BlogViewModel(blog, blogTags, categories);

				return View(model);
			}
		}

		public ActionResult Edit(int id)
		{
			var blog = db.Blog.Where(m => m.BlogID == id).FirstOrDefault();
			var blogTags = db.BlogTag.Where(m => m.BlogID == id).ToList();
			var categories = db.BlogCategory.AsNoTracking().ToList();

			var model = new BlogViewModel(blog, blogTags, categories);

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

				return RedirectToAction("Index");
			}
			else
			{
				var categories = db.BlogCategory.AsNoTracking().ToList();
				var model = new BlogViewModel(blog, blogTags,categories);
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

				return RedirectToAction("Categories");
			}
			else
			{
				return RedirectToAction("Categories");
			}
		}

		public ActionResult DeleteCategory(int id)
		{
			var category = db.BlogCategory.Where(m => m.CategoryID == id).FirstOrDefault();

			db.BlogCategory.Remove(category);

			db.SaveChanges();
			//blogService.Save();

			return RedirectToAction("Categories");
		}
	}
}