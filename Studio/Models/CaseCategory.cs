using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Studio.Models
{
	public class CaseCategory
	{
		[Key]
		public int CategoryID { get; set; }
		[MaxLength(20)]
		[Required]
		public string CategoryName { get; set; }
		[MaxLength(300)]
		public string CategoryDescription { get; set; }

		public IQueryable<Case> Cases { get; set; }
	}
}