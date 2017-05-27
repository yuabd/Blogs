using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Studio.Models
{
	public class Project
	{
		public int ProjectID { get; set; }
		public int UserID { get; set; }
		[MaxLength(50)]
		[Required]
		public string ProjectName { get; set; }
		[MaxLength(20)]
		[Required]
		public string Status { get; set; }
		[Column(TypeName="ntext")]
		public string Description { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime Deadline { get; set; }


	}
}