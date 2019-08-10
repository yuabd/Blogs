using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blogs.Model.DbModels
{
	public class Credit_Card
	{
		public int ID { get; set; }
		[MaxLength(200), Required]
		public string Name { get; set; }
		[MaxLength(500), Required]
		public string ShortDescription { get; set; }
		[MaxLength(200)]
		public string ListDescription { get; set; }
		[Required, DefaultValue(0)]
		public decimal Score { get; set; }
		/// <summary>
		/// 年费规则
		/// </summary>
		[MaxLength(300), Required]
		public string FeeDescription { get; set; }
		/// <summary>
		/// 信用卡积分规则
		/// </summary>
		[MaxLength(300), Required]
		public string PointDescription { get; set; }
		/// <summary>
		/// 卡片权益描述
		/// </summary>
		[MaxLength(4000), Required]
		public string CardDescription { get; set; }

		public int ApplyCount { get; set; }

		public int VisitCount { get; set; }

	}
}
