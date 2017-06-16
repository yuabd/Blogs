using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Studio.Models;

namespace Studio.Services
{
	public class WebsiteService
	{
		SiteDataContext db = new SiteDataContext();

		//public void InsertWebsite(Website website)
		//{
		//	db.Websites.Add(website);
		//}

		//public Website GetWebsite(int websiteID)
		//{
		//	return db.Websites.Find(websiteID);
		//}

		//public void UpdateWebsite(Website website)
		//{
		//	var w = GetWebsite(website.WebsiteID);

		//	w.IndustryID = website.IndustryID;
		//	w.Type = website.Type;
		//	w.WebsiteUrl = website.WebsiteUrl;
		//	w.WebsiteName = website.WebsiteName;
		//	w.DateCreated = website.DateCreated;
		//}

		//public void DeleteWebsite(int websiteID)
		//{
		//	var website = GetWebsite(websiteID);
		//	db.Websites.Remove(website);
		//}

		//public IQueryable<Website> GetWebsites()
		//{
		//	return db.Websites.OrderByDescending(m => m.DateCreated);
		//}

		//public void InsertWebsiteDetail(WebsiteDetail websiteDetail)
		//{
		//	db.WebsiteDetails.Add(websiteDetail);
		//}

		//public WebsiteDetail GetWebsiteDetail(int linkID)
		//{
		//	return db.WebsiteDetails.Find(linkID);
		//}

		////public void UpdateWebsiteDetail(WebsiteDetail websiteDetail)
		////{
		////    var wd = GetWebsiteDetail(websiteDetail.Link);
		////}

		//public void DeleteWebsiteDetail(int linkID)
		//{
		//	var link = GetWebsiteDetail(linkID);
		//	db.WebsiteDetails.Remove(link);
		//}

		//public IQueryable<WebsiteDetail> GetWebsiteDetails()
		//{
		//	return db.WebsiteDetails.OrderByDescending(m => m.DateCreated);
		//}

		//public IQueryable<WebsiteDetail> GetWebsiteDetails(int websiteID)
		//{
		//	return db.WebsiteDetails.Where(m => m.WebsiteID == websiteID).OrderByDescending(m => m.DateCreated);
		//}

		//public void InsertWebsiteUser(WebsiteUser websiteUser)
		//{
		//	db.WebsiteUsers.Add(websiteUser);
		//}

		//public WebsiteUser GetWebsiteUser(string userName)
		//{
		//	return db.WebsiteUsers.Find(userName);
		//}

		//public void UpdateWebsiteUser(WebsiteUser websiteUser)
		//{
		//	var wu = GetWebsiteUser(websiteUser.UserName);
		//	wu.Password = websiteUser.Password;
		//}

		//public void DeleteWebsiteUser(string userName)
		//{
		//	var websiteUser = GetWebsiteUser(userName);

		//	db.WebsiteUsers.Remove(websiteUser);
		//}

		//public IQueryable<WebsiteUser> GetWebsiteUsers()
		//{
		//	return db.WebsiteUsers;
		//}

		//public IQueryable<WebsiteUser> GetWebsiteUsers(int websiteID)
		//{
		//	return db.WebsiteUsers.Where(m => m.WebsiteID == websiteID);
		//}

		public void Save()
		{
			db.SaveChanges();
		}

		~WebsiteService()
		{
			db.Dispose();
		}
	}
}