using System;
using System.Collections.Generic;
using System.Text;

namespace Blogs.Model.DbModels
{
	public class BlogTag
	{
		[Key, Column(Order = 1)]
		public int BlogID { get; set; }
		[Key, Column(Order = 2)]
		[MaxLength(20)]
		public string Tag { get; set; }

		public virtual Blog Blog { get; set; }
	}
}
