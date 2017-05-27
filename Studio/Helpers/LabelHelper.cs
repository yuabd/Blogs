using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Studio.Models;

namespace Studio.Helpers
{
	public class LabelHelper
	{
		public static string GetIndustryName(int industryID)
		{
			using (SiteDataContext db = new SiteDataContext())
			{
				var industry = db.Industries.Find(industryID);

				return industry != null ? industry.IndustryName : "";
			}
		}

		public static string GetBlogCategoryName(int categoryID)
		{
			using (SiteDataContext db = new SiteDataContext())
			{
				var category = db.BlogCategories.FirstOrDefault(p => p.CategoryID == categoryID);

				return category == null ? "新闻动态" : category.CategoryName;
			}
		}

		public static string GetBlogCategoryName(string slug)
		{
			using (SiteDataContext db = new SiteDataContext())
			{
				var category = db.BlogCategories.FirstOrDefault(p => p.Slug == slug);

				return category == null ? "新闻动态" : category.CategoryName;
			}
		}

		public static Blog GetBlog(int blogID)
		{
			using (SiteDataContext db = new SiteDataContext())
			{
				return db.Blogs.Find(blogID);
			}
		}
	}
}