using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Studio.Models
{
	public class Industry
	{
		public int IndustryID { get; set; }
		[Required]
		[MaxLength(20)]
		public string IndustryName { get; set; }
	}
}