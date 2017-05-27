using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Studio.Services;
using Studio.Models;
using Studio.Models.Others;
using Studio.Models.CRM;

namespace Studio.Areas.CRM.Controllers
{
	[Authorize(Roles = "Administrator")]
    public class CustomerController : Controller
    {
		private CustomerService customerService = new CustomerService();
		private TaskService taskService = new TaskService();
        //
        // GET: /Customer/

        public ActionResult Index(string id)
        {
			ViewBag.Customer = "active";
			var customers = new List<Customer>();
			if (!string.IsNullOrEmpty(id))
			{
				customers = customerService.GetCustomers(id).ToList();
			}
			else
			{
				customers = customerService.GetCustomers().ToList();
			}
			
			ViewBag.Type = id;

            return View(customers);
        }

		public ActionResult Add()
		{
			ViewBag.Customer = "active";
			var customer = new Customer();
			customer.DateCreated = DateTime.Now;
			customer.UserID = 10000; //TODO

			return View(customer);
		}


		[HttpPost]
		public ActionResult Add(Customer customer)
		{
			if (ModelState.IsValid)
			{
				customerService.InsertCustomer(customer);
				customerService.Save();

				return RedirectToAction("Index");
			}

			return View(customer);
		}

		public ActionResult Edit(string id)
		{
			var customer = customerService.GetCustomer(id);

			return View(customer);
		}

		[HttpPost]
		public ActionResult Edit(Customer customer)
		{
			if (ModelState.IsValid)
			{
				customerService.UpdateCustomer(customer);
				customerService.Save();

				return RedirectToAction("Index");
			}

			return View(customer);
		}

		public ActionResult Delete(string id)
		{
			customerService.DeleteCustomer(id);
			customerService.Save();

			return RedirectToAction("Index");
		}

		public ActionResult Detail(string id)
		{
			var customer = customerService.GetCustomer(id);
			var tasks = taskService.GetTasks(id).ToList();
			
			var model = new CustomerViewModel(customer, tasks);

			return View(model);
		}

		public ActionResult AddTask(string id)
		{
			var task = new Task();
			task.DateCreated = DateTime.Now;
			task.UserID = GlobalHelper.UserID;
			task.DateEnd = DateTime.Now;
			task.Type = "客户";
			task.ID = id;

			return View(task);
		}

		[HttpPost]
		public ActionResult AddTask(Task task)
		{
			if (ModelState.IsValid)
			{
				taskService.InsertTask(task);
				taskService.Save();

				return RedirectToAction("Detail", new { id = task.ID });
			}

			return View(task);
		}

		//public ActionResult EditTask(int id)
		//{
		//    var task = taskService.GetTask(id);

		//    return View(task);
		//}

		//public ActionResult EditTask(Task task)
		//{
		//    if (ModelState.IsValid)
		//    {
		//        taskService.UpdateTask(task);
		//        taskService.Save();

		//        return RedirectToAction("Edit", new { id = task.ID });
		//    }

		//    return View(task);
		//}
    }
}
