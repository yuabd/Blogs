using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Studio.Services;
using System.ComponentModel.DataAnnotations.Schema;

namespace Studio.Models
{
	public class Blog
	{
		public int BlogID { get; set; }
		public int CategoryID { get; set; }
		[MaxLength(70)]
		[Required]
		public string BlogTitle { get; set; }
		[Column(TypeName = "ntext")]
		[MaxLength]
		public string BlogContent { get; set; }
		[MaxLength(15)]
		[Required]
		public string AuthorID { get; set; }
		[MaxLength(56)]
		public string PictureFile { get; set; }
		public bool IsPublic { get; set; }
		public DateTime DateCreated { get; set; }
		public int PageVisits { get; set; }
		//SEO
		[MaxLength(70)]
		public string PageTitle { get; set; }
		[MaxLength(150)]
		public string MetaDescription { get; set; }
		[MaxLength(100)]
		public string MetaKeywords { get; set; }
		[MaxLength(56)]	//50 + "-" + 99,000
		public string Slug { get; set; }
        
		public virtual List<BlogTag> BlogTags { get; set; }
		
		public virtual List<BlogComment> BlogComments { get; set; }
		
		public virtual BlogCategory BlogCategory { get; set; }

		[NotMapped]
		public string PictureThumbnail { get { return string.IsNullOrEmpty(PictureFile) ? "default.jpg" : PictureFile; } }
        [NotMapped]
        public string PictureFolder { get { return "/Content/Pictures/Blog"; } }
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