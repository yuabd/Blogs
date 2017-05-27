using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Studio.Models;
using Studio.Models.Others;

namespace Studio.Helpers
{
	public class ListHelper
	{
		public static IEnumerable<SelectListItem> GetBlogCategoryList()
		{
			using (SiteDataContext db = new SiteDataContext())
			{
				var list = from l in db.BlogCategories.AsEnumerable()
						   orderby l.CategoryName
						   select new SelectListItem { Value = l.CategoryID.ToString(), Text = l.CategoryName };

				return list.ToList();
			}
		}

		//public static List<BlogCategory> GetBlogCategories()
		//{
		//    using (SiteDataContext db = new SiteDataContext())
		//    {
		//        var list = (from l in db.BlogCategories
		//                   orderby l.SortOrder
		//                   select l).ToList();
		//        return list;
		//    }
		//}

		//public static List<Blog> GetBlogList()
		//{
		//    using (SiteDataContext db = new SiteDataContext())
		//    {
		//        var list = from l in db.Blogs.AsEnumerable()
		//                   orderby l.DateCreated descending
		//                   select l;
		//        return list.ToList();
				
		//    }
		//}

		public static IEnumerable<BlogComment> GetBlogComments()
		{
			using (SiteDataContext db = new SiteDataContext())
			{
				var list = from l in db.BlogComments.AsEnumerable()
						   orderby l.DateCreated
						   select l;

				return list.ToList();
			}
		}

		public static IEnumerable<SelectListItem> GetRoleList()
		{
			using (SiteDataContext db = new SiteDataContext())
			{
				var list = from l in db.UserRoles.AsEnumerable()
						   orderby l.RoleID
						   select new SelectListItem { Value = l.RoleID.ToString(), Text = l.RoleID };

				return list.ToList();
			}
		}

		public static IEnumerable<SelectListItem> GetCaseList()
		{
			using (SiteDataContext db = new SiteDataContext())
			{
				var list = from l in db.CaseCategories.AsEnumerable()
						   select new SelectListItem { Value = l.CategoryID.ToString(), Text = l.CategoryName };
				return list.ToList();
			}
		}

		public static IEnumerable<Case> GetCases()
		{
			using (SiteDataContext db = new SiteDataContext())
			{
				var list = from l in db.Cases.AsEnumerable()
						   orderby l.DateCreated descending
						   select l;
				return list.ToList();
			}
		}

		public static IEnumerable<CaseCategory> GetCaseCategories()
		{
			using (SiteDataContext db = new SiteDataContext())
			{
				var list = from l in db.CaseCategories.AsEnumerable()
						   select l;
				return list.ToList();
			}
		}

		public static IEnumerable<Anonymous> GetTags()
		{
			using (SiteDataContext db = new SiteDataContext())
			{
				var pt = from p in db.BlogTags.AsEnumerable()
						 group p by new { p.Tag } into t
						 orderby t.Count() descending
						 select new Anonymous { Num = t.Count(), Tag = t.Key.Tag };

				return pt.ToList();
			}
		}

		public static IEnumerable<SelectListItem> GetWebisteTypeList()
		{
			var list = new List<SelectListItem>();
			var item = new SelectListItem { Text = "门户", Value = "门户" };
			list.Add(item);
			var item1 = new SelectListItem { Text = "论坛", Value = "论坛" };
			list.Add(item1);
			var item2 = new SelectListItem { Text = "其他", Value = "其他" };
			list.Add(item2);

			return list.ToList();
		}

		public static IEnumerable<SelectListItem> GetStatusList()
		{
			var list = new List<SelectListItem>();
			var item = new SelectListItem { Text = "未开始", Value = "未开始" };
			list.Add(item);
			var item1 = new SelectListItem { Text = "进行中", Value = "进行中" };
			list.Add(item1);
			var item2 = new SelectListItem { Text = "完成", Value = "完成" };
			list.Add(item2);

			return list.ToList();
		}

		public static IEnumerable<SelectListItem> GetCustomerTypeList()
		{
			var list = new List<SelectListItem>();
			var item = new SelectListItem { Text = "目标客户", Value = "目标客户" };
			list.Add(item);
			var item1 = new SelectListItem { Text = "意向客户", Value = "意向客户" };
			list.Add(item1);
			var item2 = new SelectListItem { Text = "成交客户", Value = "成交客户" };
			list.Add(item2);

			return list.ToList();
		}

		public static IEnumerable<SelectListItem> GetLeadSourceList()
		{
			var list = new List<SelectListItem>();
			var item = new SelectListItem { Text = "电话销售", Value = "电话销售" };
			list.Add(item);
			var item1 = new SelectListItem { Text = "朋友介绍", Value = "朋友介绍" };
			list.Add(item1);
			var item2 = new SelectListItem { Text = "客户介绍", Value = "客户介绍" };
			list.Add(item2);
			var item3 = new SelectListItem { Text = "搜索引擎", Value = "搜索引擎" };
			list.Add(item3);
			var item4 = new SelectListItem { Text = "论坛发帖", Value = "论坛发帖" };
			list.Add(item4);
			var item5 = new SelectListItem { Text = "公司网站", Value = "公司网站" };
			list.Add(item5);

			return list.ToList();
		}

		public static IEnumerable<SelectListItem> GetIndustryList()
		{
			using (SiteDataContext db = new SiteDataContext())
			{
				var list = from l in db.Industries.AsEnumerable()
						   orderby l.IndustryName
						   select new SelectListItem { Text = l.IndustryName, Value = l.IndustryID.ToString() };

				return list.ToList();
			}
		}

	}
}