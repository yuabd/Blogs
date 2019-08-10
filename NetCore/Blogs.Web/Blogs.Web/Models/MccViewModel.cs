using Blogs.Model.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogs.Web.Models
{
    public class MccViewModel
    {
		/// <summary>
		/// mcc
		/// </summary>
		public List<POS_MCC> Mccs { get; set; }
		/// <summary>
		/// 商户地区码
		/// </summary>
		public List<POS_MerchantArea> Areas { get; set; }
		/// <summary>
		/// 支付牌照
		/// </summary>
		public List<POS_PaymentLicense> Licenses { get; set; }
		/// <summary>
		/// 收单机构
		/// </summary>
		public List<POS_Agency> Agency { get; set; }
	}
}
