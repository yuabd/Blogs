using Blogs.Model.DbModels;
using Blogs.Model.ViewModels.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogs.Web.Models
{
	public class BlogViewModel
	{
		public Blog Blog { get; private set; }

		//public List<BlogComment> BlogComments { get; private set; }

		//public BlogComment BlogComment { get; private set; }

		//public List<BlogCategory> Categories { get; private set; }

		public List<Anonymous> PopularTags { get; private set; }

		//public List<Archive> Archives { get; private set; }

		public PreNextBlog PreNextBlog { get; set; }

		public BlogViewModel(
			Blog blog,
			//BlogComment blogComment,
			//List<BlogComment> blogComments,
			//List<BlogCategory> categories,
			List<Anonymous> popularTags,
			//List<Archive> archives, 
			PreNextBlog preNextBlog)
		{
			Blog = blog;
			//BlogComments = blogComments;
			//BlogComment = blogComment;
			//Categories = categories;
			PopularTags = popularTags;
			//Archives = archives;
			PreNextBlog = preNextBlog;
		}
	}

	public class PreNextBlog
	{
		public BlogIDName PreBlog { get; set; }

		public BlogIDName NextBlog { get; set; }
	}

	public class BlogIDName
	{
		public int ID { get; set; }

		public string Title { get; set; }

		public string Slug { get; set; }
	}
}
