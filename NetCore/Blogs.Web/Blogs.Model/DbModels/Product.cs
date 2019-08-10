using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blogs.Model.DbModels
{
	public class Product
	{
		public int ID { get; set; }
		[MaxLength(200), Required]
		public string Name { get; set; }
		/// <summary>
		/// 详情
		/// </summary>
		[Required]
		public string Description { get; set; }
		/// <summary>
		/// 分类
		/// </summary>
		[Required]
		public int ProductCategoryID { get; set; }
		/// <summary>
		/// 押金
		/// </summary>
		[Required]
		public decimal ProductPrice { get; set; }

		/// <summary>
		/// 退押金条件
		/// </summary>
		[Required, MaxLength(300)]
		public string ReturnDesc { get; set; }
		/// <summary>
		/// 市场价
		/// </summary>
		[Required]
		public decimal MarketPrice { get; set; }
		/// <summary>
		/// 品牌id
		/// </summary>
		[Required]
		public int ProductBrandID { get; set; }



		public DateTime DateCreated { get; set; }
	}
}
