using Blogs.Model.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogs.Services.Blog
{
    public class BlogService: IBlogService
	{
		private SiteDataContext db;
		public BlogService()
		{

		}

		public bool AddBlog(Blogs.Model.DbModels.Blog model)
		{
			return true;
		}
    }
}
