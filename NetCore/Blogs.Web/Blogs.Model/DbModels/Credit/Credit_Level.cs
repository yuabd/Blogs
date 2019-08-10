using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blogs.Model.DbModels
{
    public class Credit_Level
    {
		public int ID { get; set; }
		[Required, MaxLength(200)]
		public string Name { get; set; }

		public int Sort { get; set; }
	}
}
