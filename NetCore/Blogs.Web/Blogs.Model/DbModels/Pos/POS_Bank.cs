using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blogs.Model.DbModels
{
    public class POS_Bank
    {
		[Key]
		public int BankID { get; set; }
		/// <summary>
		/// 银行名字
		/// </summary>
		[MaxLength(100), Required]
		public string BankName { get; set; }
		/// <summary>
		/// 银行简称
		/// </summary>
		[MaxLength(50), Required]
		public string ShortName { get; set; }
		/// <summary>
		/// 描述
		/// </summary>
		[MaxLength(500)]
		public string Description { get; set; }
		/// <summary>
		/// 客服号
		/// </summary>
		[MaxLength(15), Required]
		public string PhoneNumber { get; set; }
		/// <summary>
		/// 图标
		/// </summary>
		[MaxLength(200)]
		public string Pic { get; set; }

		[Required]
		public DateTime DateCreated { get; set; }
		
	}
}
