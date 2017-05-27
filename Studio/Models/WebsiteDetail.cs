using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Studio.Models
{
	public class WebsiteDetail
	{
		[Key]
		public int LinkID { get; set; }
		[MaxLength(300)]		
		public string Link { get; set; }
		public int WebsiteID { get; set; }
		public DateTime DateCreated { get; set; }

		public virtual Website Website { get; set; }
	}
}