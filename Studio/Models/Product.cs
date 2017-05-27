using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Studio.Models
{
	public class Product
	{
		public int ProductID { get; set; }
		[MaxLength(40)]
		[Required]
		public string ProductName { get; set; }
		[MaxLength(500)]
		//[Required]
		public string Description { get; set; }
		public int UnitPrice { get; set; }
		public int SortOrder { get; set; }

		public virtual ICollection<ProductOrderJoin> ProductOrderJoins { get; set; }
	}
}