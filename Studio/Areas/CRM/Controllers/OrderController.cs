using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Studio.Services;
using Studio.Models;
using Studio.Models.Others;

namespace Studio.Areas.CRM.Controllers
{
	[Authorize(Roles = "Administrator")]
    public class OrderController : Controller
    {
		private OrderService orderService = new OrderService();
        //
        // GET: /CRM/Order/

        public ActionResult Index(int? page)
        {
			var orders = orderService.GetOrders(GlobalHelper.UserID).ToList();
			var model = new Paginated<Order>(orders, page ?? 1, 10);

            return View(model);
        }

    }
}
