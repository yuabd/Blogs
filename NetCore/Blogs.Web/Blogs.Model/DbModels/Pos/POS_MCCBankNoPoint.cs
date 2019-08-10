using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blogs.Model.DbModels
{
    public class POS_MCCBankNoPoint
	{
		public int BankID { get; set; }
		[MaxLength(4)]
		public string MCC { get; set; }

		public virtual POS_Bank POS_Bank { get; set; }

		public virtual POS_MCC POS_MCC { get; set; }
	}
}
