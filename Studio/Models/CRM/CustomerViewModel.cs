using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Studio.Models.Others;

namespace Studio.Models.CRM
{
	public class CustomerViewModel
	{
		public Customer Customer { get; set; }
		public IEnumerable<Task> Tasks { get; set; }

		public CustomerViewModel(Customer customer, IEnumerable<Task> tasks)
		{
			Customer = customer;
			Tasks = tasks;
		}
	}
}