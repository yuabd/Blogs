using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Studio.Models;

namespace Studio.Services
{
	public class OrderService
	{
		private SiteDataContext db = new SiteDataContext();
		//public void InsertOrder(Order order)
		//{
		//	db.Orders.Add(order);
		//}

		//public Order GetOrder(int orderID)
		//{
		//	return db.Orders.Find(orderID);
		//}

		//public void UpdateOrder(Order order)
		//{
		//	var o = GetOrder(order.OrderID);
			
		//	o.DateCreated = order.DateCreated;
		//	o.Description = order.Description;
		//	o.DateFinish = order.DateFinish;
		//	o.DateStart = order.DateStart;
		//	o.DiscountAmount = order.DiscountAmount;
		//	o.NeedInvoice = order.NeedInvoice;
		//	o.TotalAmount = order.TotalAmount;
		//	o.Status = order.Status;
		//}

		//public void DeleteOrder(int orderID)
		//{
		//	var order = GetOrder(orderID);

		//	db.Orders.Remove(order);
		//}

		//public IQueryable<Order> GetOrders()
		//{
		//	return db.Orders.OrderByDescending(m => m.DateCreated);
		//}

		//public IQueryable<Order> GetOrders(int userID)
		//{
		//	return db.Orders.Where(m => m.UserID == userID).OrderByDescending(m => m.DateCreated);
		//}

		public void Save()
		{
			db.SaveChanges();
		}

		~OrderService()
		{
			db.Dispose();
		}
	}
}