using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blogs.Model.DbModels
{
    public class POS_MerchantArea
    {
		[Key]
		public int ID { get; set; }
		/// <summary>
		/// 地区编码
		/// </summary>
		[MaxLength(10), Required]
		public string MerchantAreaCode { get; set; }
		/// <summary>
		/// 地区名称
		/// </summary>
		[MaxLength(100), Required]
		public string MerchantAreaName { get; set; }
		/// <summary>
		/// 所属省份
		/// </summary>
		[MaxLength(100), Required]
		public string MerchantProvince { get; set; }

		public long VisitCount { get; set; }
	}
}
