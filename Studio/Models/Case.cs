using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Studio.Models
{
	public class Case
	{
		public int CaseID { get; set; }
		public int CategoryID { get; set; }
		[MaxLength(50)]
		[Required]
		public string Name { get; set; }
		[MaxLength(300)]
		public string ShortDescription { get; set; }
		[MaxLength(600)]
		public string Description { get; set; }
		[MaxLength(50)]
		[Required]
		public string Website { get; set; }
		public DateTime DateCreated { get; set; }
		[MaxLength(60)]
		public string PictureFile { get; set; }

		public CaseCategory CaseCategory { get; set; }

		// extended
		public string PictureFolder { get { return "/content/case"; } }
		public string PictureThumbnail { get { return string.IsNullOrEmpty(PictureFile) ? "default.jpg" : PictureFile; } }
	}
}