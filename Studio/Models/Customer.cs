using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Studio.Models
{
	public class Customer
	{
		[Key]
		[MaxLength(40)]
		public string CompanyName { get; set; } //公司名
		public int UserID { get; set; } //客户所有者
		public int IndustryID { get; set; } //行业
		[MaxLength(30)]
		[Required]
		public string LeadSource { get; set; } //来源
		[Required]
		[MaxLength(10)]
		public string Type { get; set; }  //如意向客户、成交客户
		[Required]
		[MaxLength(30)]
		public string ContactName { get; set; } //联系人姓名
		[MaxLength(6)]
		[Required]
		public string Gender { get; set; } //性别
		[MaxLength(15)]
		[Required]
		public string Phone { get; set; } //电话
		[MaxLength(100)]
		[Required]
		public string Address { get; set; }
		[MaxLength(50)]
		public string Email { get; set; }
		[MaxLength(20)]
		[Required]
		public string Status { get; set; }
		public DateTime DateCreated { get; set; }

		public virtual User User { get; set; }
	}
}