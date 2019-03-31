using Blogs.Model.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogs.Web.Areas.Admin.Models
{
	public class BlogViewModel
	{
		public Blog Blog { get; private set; }

		public IEnumerable<BlogTag> BlogTags { get; private set; }

		public IList<BlogCategory> Categories { get; private set; }

		public BlogViewModel(Blog blog, IEnumerable<BlogTag> blogTags, IList<BlogCategory> categories)
		{
			Blog = blog;
			BlogTags = blogTags;
			Categories = categories;
		}
	}
}
