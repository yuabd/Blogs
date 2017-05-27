using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Studio.Models
{
	public class WebsiteUser
	{
		[MaxLength(20)]
		[Key]
		public string UserName { get; set; }
		public int WebsiteID { get; set; }
		[MaxLength(20)]
		public string Password { get; set; }

		public virtual Website Website { get; set; }
	}
}