using Blogs.Model.DbModels;
using Blogs.Model.ViewModels.Others;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogs.Model.ViewModels
{
	public class BlogsViewModel
	{
		public IList<Blog> Blogs { get; private set; }

		public IEnumerable<BlogCategory> Categories { get; private set; }

		public IEnumerable<Anonymous> PopularTags { get; private set; }

		public IEnumerable<Archive> Archives { get; private set; }

		public BlogsViewModel(
			IList<Blog> blogs,
			IEnumerable<BlogCategory> categories,
			IEnumerable<Anonymous> popularTags,
			IEnumerable<Archive> archives
			)
		{
			Blogs = blogs;
			Categories = categories;
			PopularTags = popularTags;
			Archives = archives;
		}
	}
}
