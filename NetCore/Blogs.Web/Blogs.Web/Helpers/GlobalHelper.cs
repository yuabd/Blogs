using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Blogs.Model.DbModels;

namespace Blogs.Web.Helpers
{
    public class GlobalHelper
	{
		private SiteDataContext _db;
		public GlobalHelper(SiteDataContext db)
		{
			_db = db;
		}

		public List<Links> GetLinks()
		{
			return _db.Link.OrderBy(m => m.SortOrder).ToList();
		}
	}
}
