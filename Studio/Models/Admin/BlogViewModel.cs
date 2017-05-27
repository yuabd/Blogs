using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Studio.Models.Admin
{
	public class BlogViewModel
	{
		public Blog Blog { get; private set; }

		public IEnumerable<BlogTag> BlogTags { get; private set; }

		public BlogViewModel(Blog blog, IEnumerable<BlogTag> blogTags)
		{
			Blog = blog;
			BlogTags = blogTags;
		}
	}
}