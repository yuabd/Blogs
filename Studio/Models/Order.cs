using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Studio.Models
{
	public class Order
	{
		public int OrderID { get; set; }
		[ForeignKey("Customer")]
		public string CompanyName { get; set; }
		public int UserID { get; set; }
		[Column(TypeName="ntext")]
		public string Description { get; set; }
		[MaxLength(20)]
		[Required]
		public string Status { get; set; }
		public bool NeedInvoice { get; set; }
		public float TotalAmount { get; set; }
		public float DiscountAmount { get; set; }

		public DateTime DateCreated { get; set; }
		public DateTime DateStart { get; set; }
		public DateTime DateFinish { get; set; }


		public virtual ICollection<ProductOrderJoin> ProductOrderJoins { get; set; }
		public virtual Customer Customer { get; set; }
		public virtual User User { get; set; }
	}
}