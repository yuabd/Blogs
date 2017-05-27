using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Studio.Services;
using Studio.Models.Others;

namespace Studio.Models.Site
{
	public class BlogViewModel
	{
		public Blog Blog { get; private set; }

		public IEnumerable<BlogComment> BlogComments { get; private set; }

		public BlogComment BlogComment { get; private set; }

		public IEnumerable<BlogCategory> Categories { get; private set; }

		public IEnumerable<Anonymous> PopularTags { get; private set; }

		public IEnumerable<Archive> Archives { get; private set; }

		public PreNextBlog PreNextBlog { get; set; }

		public BlogViewModel(
			Blog blog,
			BlogComment blogComment,
			IEnumerable<BlogComment> blogComments,
			IEnumerable<BlogCategory> categories,
			IEnumerable<Anonymous> popularTags,
			IEnumerable<Archive> archives, PreNextBlog preNextBlog)
		{
			Blog = blog;
			BlogComments = blogComments;
			BlogComment = blogComment;
			Categories = categories;
			PopularTags = popularTags;
			Archives = archives;
			PreNextBlog = preNextBlog;
		}
	}
}