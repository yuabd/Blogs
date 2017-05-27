using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Studio.Models.Others;
using Studio.Models;

namespace Studio.Models.CRM
{
	public class WebsiteViewModel
	{
		public Website Website { get; set; }
		public Paginated<WebsiteDetail> WebsiteDetails { get; set; }
		public Paginated<WebsiteUser> WebsiteUsers { get; set; }

		public WebsiteViewModel(Website website, Paginated<WebsiteDetail> websiteDetails, Paginated<WebsiteUser> websiteUsers)
		{
			Website = website;
			WebsiteDetails = websiteDetails;
			WebsiteUsers = websiteUsers;
		}
	}
}