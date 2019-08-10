using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blogs.Model.DbModels
{
	public class POS_MCC
	{
		/// <summary>
		/// MCC
		/// </summary>
		[Key,MaxLength(4)]
		public string MCC { get; set; }
		/// <summary>
		/// 类别名称
		/// </summary>
		[MaxLength(500), Required]
		public string Description { get; set; }
		/// <summary>
		/// 是否有积分
		/// </summary>
		[Required]
		public bool IsPoint { get; set; }
		/// <summary>
		/// 费率
		/// </summary>
		[Required]
		public decimal Rate { get; set; }
		/// <summary>
		/// 费改前费率
		/// </summary>
		[Required]
		public decimal OldRate { get; set; }
		/// <summary>
		/// 无积分的银行
		/// </summary>
		[MaxLength(300), Required]
		public string NoPointBankStr { get; set; }
		/// <summary>
		/// 查询次数
		/// </summary>
		public long VisitCount { get; set; }
		[Required]
		public DateTime DateCreated { get; set; }
	}
}
