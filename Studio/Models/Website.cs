using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Studio.Models
{
	public class Website
	{
		public int WebsiteID { get; set; }
		public int IndustryID { get; set; }
		[MaxLength(20)]
		[Required]
		public string WebsiteName { get; set; }
		[MaxLength(20)]
		[Required]
		public string Type { get; set; }
		[Required]
		[MaxLength(300)]
		public string WebsiteUrl { get; set; }
		public DateTime DateCreated { get; set; }

		public virtual ICollection<WebsiteDetail> WebsiteDetails { get; set; }
		public virtual ICollection<WebsiteUser> WebsiteUsers { get; set; }
	}

	public class Links
	{
		[Key]
		public int ID { get; set; }

		//public int? LinkCategoryID { get; set; }
		[Required(ErrorMessage = "必填"), MaxLength(100)]
		public string LinkUrl { get; set; }
		[Required(ErrorMessage = "必填")]
		public int? SortOrder { get; set; }
		[Required(ErrorMessage = "必填"), MaxLength(100)]
		public string Name { get; set; }

		public string PictureFile { get; set; }

		public string Description { get; set; }

		public string Contact { get; set; }

		public string Email { get; set; }

		public DateTime? DateCreated { get; set; }

		//public string UpdateUser { get; set; }
	}
}