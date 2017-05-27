using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Studio.Models
{
	public class Task
	{
		[Key]
		public int TaskID { get; set; }
		public int UserID { get; set; }
		//[MaxLength(30)]
		//[Required]
		//public string TaskName { get; set; }
		[MaxLength(20)]
		[Required]
		public string Status { get; set; }
		[MaxLength(20)]
		[Required]
		public string Type { get; set; }
		[MaxLength(30)]
		public string ID { get; set; }//储存客户ID或者项目ID
		[Column(TypeName="ntext")]
		[Required]
		public string Description { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateEnd { get; set; }
		public int SortOrder { get; set; }
	}
}