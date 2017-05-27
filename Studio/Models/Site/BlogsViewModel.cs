using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Studio.Helpers;
using Studio.Models;
using Studio.Models.Others;
using Studio.Services;

namespace Studio.Models.Site
{
	public class BlogsViewModel
	{
		public Paginated<Blog> Blogs { get; private set; }

		public IEnumerable<BlogCategory> Categories { get; private set; }

		public IEnumerable<Anonymous> PopularTags { get; private set; }

		public IEnumerable<Archive> Archives { get; private set; }

		public BlogsViewModel(
			Paginated<Blog> blogs,
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