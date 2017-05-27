using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Studio.Models
{
	public class ProductOrderJoin
	{
		[Key, Column(Order = 1)]
		public int OrderID { get; set; }
		[Key, Column(Order = 2)]
		public int ProductID { get; set; }

		public virtual Order Order { get; set; }
		public virtual Product Product { get; set; }
	}
}