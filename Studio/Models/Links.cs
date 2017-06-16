using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Studio.Models
{
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