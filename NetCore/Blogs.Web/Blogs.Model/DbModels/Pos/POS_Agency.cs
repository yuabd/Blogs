using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blogs.Model.DbModels
{
    public class POS_Agency
    {
		[Key]
		public int AgencyID { get; set; }
		[MaxLength(100), Required]
		public string AgencyName { get; set; }
		[MaxLength(150), Required]
		public string AgencyCode { get; set; }
	}
}
