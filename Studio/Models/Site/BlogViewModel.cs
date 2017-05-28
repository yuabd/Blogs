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

		public List<BlogComment> BlogComments { get; private set; }

		public BlogComment BlogComment { get; private set; }

		public List<BlogCategory> Categories { get; private set; }

		public List<Anonymous> PopularTags { get; private set; }

		public List<Archive> Archives { get; private set; }

		public PreNextBlog PreNextBlog { get; set; }

		public BlogViewModel(
			Blog blog,
			BlogComment blogComment,
            List<BlogComment> blogComments,
            List<BlogCategory> categories,
            List<Anonymous> popularTags,
            List<Archive> archives, PreNextBlog preNextBlog)
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