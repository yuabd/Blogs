using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blogs.Model.DbModels
{
    public class POS_PaymentLicense
    {
		/// <summary>
		/// 支付许可证编号
		/// </summary>
		[Key,MaxLength(100)]
		public string LicenseID { get; set; }
		/// <summary>
		/// 支付公司名称
		/// </summary>
		[Required, MaxLength(100)]
		public string CompanyName { get; set; }
		/// <summary>
		/// 发证日期
		/// </summary>
		[Required]
		public DateTime Date { get; set; }
		/// <summary>
		/// 业务类型
		/// </summary>
		[Required,MaxLength(300)]
		public string BusType { get; set; }
		/// <summary>
		/// 业务范围
		/// </summary>
		[MaxLength(500), Required]
		public string Scope { get; set; }
		
		public long VisitCount { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		[MaxLength(500)]
		public string Remark { get; set; }
	}
}
