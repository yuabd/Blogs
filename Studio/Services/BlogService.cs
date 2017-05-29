using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Studio.Models;
using System.Web.Mvc;
using Studio.Models.Others;

namespace Studio.Services
{
	public class BlogService : DbAccess
	{
		//private SiteDataContext db = new SiteDataContext();

		// Blog

		public void InsertBlog(Blog blog, HttpPostedFileBase file)
		{
			if (string.IsNullOrEmpty(blog.PageTitle))
			{
				blog.PageTitle = blog.BlogTitle;
			}

			db.Blogs.Add(blog);
			db.SaveChanges();

			// add slug after (depends on ID)
			blog.Slug =  blog.BlogID.ToString();

			// file
			if (file.ContentLength > 0)
			{
				UploadBlogPicture(blog, file);
			}
			db.SaveChanges();
		}

		public Blog GetBlog(int blogID)
		{
			return db.Blogs.Find(blogID);
		}

		public void UpdateBlog(Blog blog, HttpPostedFileBase file)
		{
			var b = GetBlog(blog.BlogID);
            
			b.CategoryID = blog.CategoryID;
			b.BlogTitle = blog.BlogTitle;
			b.BlogContent = blog.BlogContent;
			b.DateCreated = blog.DateCreated;
			b.PageTitle = blog.PageTitle;
			b.MetaDescription = blog.MetaDescription;
			b.MetaKeywords = blog.MetaKeywords;
			b.Slug = blog.BlogID.ToString();
			b.IsPublic = blog.IsPublic;

			// file
			if (file.ContentLength > 0)
			{
				UploadBlogPicture(b, file);
			}

			db.SaveChanges();
		}

		public void DeleteBlog(int blogID)
		{
			var b = GetBlog(blogID);
			db.Blogs.Remove(b);

			DeleteBlogPicture(b);

			db.SaveChanges();
		}

		public Blog GetBlog(string slug)
		{
			var blog = db.Blogs.FirstOrDefault(m => m.Slug == slug);
			if (blog != null)
			{
				blog.PageVisits += 1;
                if (string.IsNullOrEmpty(blog.MetaKeywords))
                {
                    blog.MetaKeywords = "";
                }
                db.SaveChanges();
			}

			return blog;
		}

		public Blog GetLastBlog()
		{
			return db.Blogs.Where(m => m.IsPublic == true).OrderByDescending(m => m.DateCreated).Take(1).FirstOrDefault();
		}

		public IQueryable<Blog> GetBlogs()
		{
			return db.Blogs.OrderByDescending(b => b.DateCreated);
		}

		public IQueryable<Blog> GetBlogs(string languageID)
		{
			return db.Blogs.Where(m => m.BlogCategory.LanguageID == languageID).OrderByDescending(b => b.DateCreated);
		}

		public IQueryable<Blog> GetBlogsByCategory(int id)
		{
			var l = (from b in db.Blogs
					 //join bc in db.BlogCategories on b.CategoryID equals bc.CategoryID
					 where b.CategoryID == id
					 orderby b.DateCreated descending
					 select b);

			return l;
		}

		public IQueryable<Blog> GetBlogsByTag(string tag)
		{
			var l = (from b in db.Blogs
					 join bc in db.BlogTags on b.BlogID equals bc.BlogID
					 where bc.Tag == tag
					 orderby b.DateCreated descending
					 select b);

			return l;
		}

		public IQueryable<Blog> GetBlogsByTag(string tag, string languageID)
		{
			var l = (from b in db.Blogs
					 join bc in db.BlogTags on b.BlogID equals bc.BlogID
					 where bc.Tag == tag && b.BlogCategory.LanguageID == languageID
					 orderby b.DateCreated descending
					 select b);

			return l;
		}

		public IQueryable<Blog> GetBlogsByArchive(string year, string month, string type)
		{
            if (string.IsNullOrEmpty(month))
            {
                month = "January";
            }
            //if (string.IsNullOrEmpty(year))
            //{
            //    year = DateTime.Now.Year.ToString();
            //}
            //DateTime fromTime = new DateTime();
            //if (!string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(month))
            //{
            DateTime  fromTime = Convert.ToDateTime(year + "/" + month);
            //}
            //else if(!string.IsNullOrEmpty())
            //{

            //}
			
			DateTime toTime = fromTime;
			if (type == "year")
				toTime = fromTime.AddYears(1);
			else if (type == "month")
				toTime = fromTime.AddMonths(1);

			return db.Blogs.Where(b => b.DateCreated >= fromTime && b.DateCreated <= toTime).OrderByDescending(b => b.DateCreated);
		}

		public IQueryable<Blog> GetBlogsByArchive(string year, string month, string type, string languageID)
		{
			return GetBlogsByArchive(year, month, type).Where(m => m.BlogCategory.LanguageID == languageID);
		}

		// Blog Comment

		public void InsertBlogComment(BlogComment blogComment)
		{
			blogComment.IsPublic = false;
			blogComment.DateCreated = DateTime.Now;
			db.BlogComments.Add(blogComment);
			db.SaveChanges();

			//try
			//{
			//	var subject = "有新的评论需要您审核-厦门巨易网络科技有限公司";

			//	var message = string.Format("<p>Hi {0},</p>" +
			//		"<p>评论信息:</p>" +
			//		"{1}" +
			//		"<p>文章: <a href='http://www.henhaoji.com.cn/Blog/Post/{2}'>{3}</a></p>" +
			//		"<p><a href='http://www.henhaoji.com.cn/admin/blog/PendingComments'>点击审核</a></p>",
			//		blogComment.Name,
			//		blogComment.Message,
			//		blogComment.Blog.Slug,
			//		blogComment.Blog.BlogTitle
			//		);
            
			//	Studio.Models.Others.MailBag mailBag = new Studio.Models.Others.MailBag();

			//	mailBag.ToMailAddress = "jerry@henhaoji.com.cn";
			//	mailBag.CcMailAddress = "yuabd1991@gmail.com";
			//	mailBag.Subject = subject;
			//	mailBag.Message = message;
			//	mailBag.Send(true);
			//}
			//catch (Exception e)
			//{
			//}
		}

		public void UpdateBlogComment(BlogComment blogComment)
		{
			var bf = GetBlogComment(blogComment.CommentID);
			bf.Message = blogComment.Message;
		}

		public void DeleteBlogComment(int commentID)
		{
			var c = GetBlogComment(commentID);
			db.BlogComments.Remove(c);

			db.SaveChanges();
		}

		public BlogComment GetBlogComment(int commentID)
		{
			return db.BlogComments.Find(commentID);
		}

		public IQueryable<BlogComment> GetBlogComments(int blogID)
		{
			return db.BlogComments.Where(p => p.BlogID == blogID).OrderByDescending(m => m.DateCreated);
		}

		public void ApproveBlogComment(int commentID)
		{
			var c = db.BlogComments.FirstOrDefault(m => m.CommentID == commentID);
			c.IsPublic = true;
			//c.BlogID
			db.SaveChanges();

			// notify posting user

			//try
			//{
			//	var subject = "您的评论已经审核通过-厦门巨易网络科技有限公司";

			//	var message = string.Format("<p>Hi {0},</p>" +
			//		"<p>您的评论已经被审核通过:</p>" +
			//		"{1}" +
			//		"<p>文章: <a href='http://www.henhaoji.com.cn/Blog/Post/{2}.html'>{3}</a></p>",
			//		c.Name,
			//		c.Message,
			//		c.Blog.Slug,
			//		c.Blog.BlogTitle
			//		);

			//	Studio.Models.Others.MailBag mailBag = new Studio.Models.Others.MailBag();

			//	mailBag.ToMailAddress = c.Email;
			//	mailBag.Subject = subject;
			//	mailBag.Message = message;
			//	mailBag.Send(true);
			//}
			//catch (Exception e)
			//{
			//}
		}

		public IQueryable<BlogComment> GetComments()
		{
			var list = (from l in db.BlogComments
						orderby l.DateCreated descending
						select l);

			return list;
		}

		public IQueryable<Blog> GetBlogsWithPendingComments()
		{
			var r = (from b in db.Blogs
					 where db.BlogComments.Any(c => c.IsPublic == false && c.BlogID == b.BlogID)
					 select b);

			return r;
		}

		// Blog Category

		public void InsertBlogCategory(BlogCategory blogCategory)
		{
			if (string.IsNullOrEmpty(blogCategory.PageTitle))
			{
				blogCategory.PageTitle = blogCategory.CategoryName;
			}
			db.BlogCategories.Add(blogCategory);

            db.SaveChanges();

            blogCategory.Slug = blogCategory.CategoryID.ToString();

            db.SaveChanges();
        }

		public BlogCategory GetBlogCategory(int id)
		{
			return db.BlogCategories.Find(id);
		}

		public void UpdateBlogCategory(BlogCategory blogCategory)
		{
			var bc = GetBlogCategory(blogCategory.CategoryID);
            
			bc.CategoryName = blogCategory.CategoryName;
			bc.PageTitle = blogCategory.PageTitle;
			bc.MetaDescription = blogCategory.MetaDescription;
			bc.MetaKeywords = blogCategory.MetaKeywords;
			bc.LanguageID = blogCategory.LanguageID;	// used for multilanguage sites
			bc.SortOrder = blogCategory.SortOrder;

			db.SaveChanges();
		}

		public void DeleteBlogCategory(int categoryID)
		{
			var bc = GetBlogCategory(categoryID);
			db.BlogCategories.Remove(bc);

			db.SaveChanges();
		}

		public IQueryable<BlogCategory> GetBlogCategories()
		{
			return db.BlogCategories.OrderBy(m => m.SortOrder);
		}

		public IQueryable<BlogCategory> GetBlogCategories(string languageID)
		{
			return GetBlogCategories().Where(m => m.LanguageID == languageID);
		}



		//Blog Tag
		public IQueryable<Anonymous> GetPopularTags()
		{
			var pt = (from p in db.BlogTags
					  group p by new { p.Tag } into t
					  orderby t.Count() descending
                      select new Anonymous { Tag = t.Key.Tag, Num = t.Count() }).Take(10);

			return pt;
		}

		public IQueryable<String> GetPopularTags(string languageID)
		{
			var pt = (from p in db.BlogTags
					  where p.Blog.BlogCategory.LanguageID == languageID
					  group p by new { p.Tag } into t
					  orderby t.Count() descending
					  select t.Key.Tag);

			return pt;
		}

        public IQueryable<BlogTag> GetTags()
        {
            return db.BlogTags;
        }

		public IQueryable<BlogTag> GetBlogTags(int blogID)
		{
			return db.BlogTags.Where(b => b.BlogID == blogID);
		}

		public void SaveBlogTags(Blog blog, List<BlogTag> blogTags)
		{
			var bt = GetBlogTags(blog.BlogID);

			foreach (BlogTag tag in bt)
			{
				DeleteBlogTag(tag);
			}

			foreach (BlogTag tag in blogTags)
			{
				tag.BlogID = blog.BlogID;
				if (!string.IsNullOrEmpty(tag.Tag))
				{
					tag.Tag = Studio.Helpers.Utilities.GenerateSlug(tag.Tag, 20);
					InsertBlogTag(tag);
				}
			}

			db.SaveChanges();
		}

		public void InsertBlogTag(BlogTag blogTag)
		{
			db.BlogTags.Add(blogTag);
		}

		public void DeleteBlogTag(BlogTag blogTag)
		{
			db.BlogTags.Remove(blogTag);
		}

		public PreNextBlog GetPreNextBlog(int id)
		{
			PreNextBlog blog = new PreNextBlog();

			var pre = (from l in db.Blogs
					   where l.BlogID < id
					   orderby l.BlogID descending
					   select new BlogIDName()
					   {
						   ID = l.BlogID,
						   Slug = l.Slug,
						   Title = l.BlogTitle
					   }).FirstOrDefault();

			var next = (from l in db.Blogs
						where l.BlogID > id
						orderby l.BlogID
						select new BlogIDName()
						{
							ID = l.BlogID,
							Slug = l.Slug,
							Title = l.BlogTitle
						}).FirstOrDefault();
			blog.PreBlog = pre;
			blog.NextBlog = next;

			return blog;
		}

		// Others
		public IEnumerable<Archive> GetArchives()
		{
			// Get months list
			DateTime dt = new DateTime(2012, 1, 1);
			List<SelectListItem> months = new List<SelectListItem>();

			for (int i = 1; i <= 12; i++)
			{
				SelectListItem item = new SelectListItem();
				item.Value = i.ToString();
				item.Text = dt.AddMonths(i - 1).ToString("MMMM");
				months.Add(item);
			}

			// get the archives list
			var Archives = new List<Archive>();

			foreach (var item in months)  //this year per month
			{
				var Archive = new Archive();
				Archive.Month = item.Text;
				Archive.Year = DateTime.Now.Year.ToString();
				Archive.Count = GetBlogsByArchive(Archive.Year, Archive.Month, "month").Count();
				if (Archive.Count > 0)
				{
					Archives.Add(Archive);
				}
			}

			for (int i = 1; i <= 5; i++)  //last 5 years
			{
				var Archive = new Archive();
				Archive.Year = DateTime.Now.AddYears(-i).Year.ToString();
				Archive.Month = "";
				Archive.Count = GetBlogsByArchive(Archive.Year, Archive.Month, "year").Count();
				if (Archive.Count > 0)
				{
					Archives.Add(Archive);
				}
			}

			return Archives;
		}

		// TODO: improve this, factorize better
		public IEnumerable<Archive> GetArchives(string languageID)
		{
			// Get months list
			DateTime dt = new DateTime(2012, 1, 1);
			List<SelectListItem> months = new List<SelectListItem>();

			for (int i = 1; i <= 12; i++)
			{
				SelectListItem item = new SelectListItem();
				item.Value = i.ToString();
				item.Text = dt.AddMonths(i - 1).ToString("MMMM");
				months.Add(item);
			}

			// get the archives list
			var Archives = new List<Archive>();

			foreach (var item in months)  //this year per month
			{
				var Archive = new Archive();
				Archive.Month = item.Text;
				Archive.Year = DateTime.Now.Year.ToString();
				Archive.Count = GetBlogsByArchive(Archive.Year, Archive.Month, "month", languageID).Count();
				if (Archive.Count > 0)
				{
					Archives.Add(Archive);
				}
			}

			for (int i = 1; i <= 5; i++)  //last 5 years
			{
				var Archive = new Archive();
				Archive.Year = DateTime.Now.AddYears(-i).Year.ToString();
				Archive.Month = "";
				Archive.Count = GetBlogsByArchive(Archive.Year, Archive.Month, "year", languageID).Count();
				if (Archive.Count > 0)
				{
					Archives.Add(Archive);
				}
			}

			return Archives;
		}

		// Files

		public void UploadBlogPicture(Blog blog, HttpPostedFileBase file)
		{
			var fileName = string.Format("{0}-{1}", blog.BlogID, blog.Slug);

            blog.PictureFile = Studio.Helpers.IO.UploadImageFile(file.InputStream, blog.PictureFolder, fileName, 800, 400, Studio.ImageHelper.ImageSaveType.Cut);
		}

		private void DeleteBlogPicture(Blog blog)
		{
			Studio.Helpers.IO.DeleteFile(blog.PictureFolder, blog.PictureFile);
		}

		// Save

		public void Save()
		{
			db.SaveChanges();
		}
	}
}