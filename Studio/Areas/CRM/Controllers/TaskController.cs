using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Studio.Models;
using Studio.Services;
using Studio.Models.Others;

namespace Studio.Areas.CRM.Controllers
{
	[Authorize(Roles = "Administrator")]
    public class TaskController : Controller
    {
		TaskService taskService = new TaskService();
        //
        // GET: /CRM/Task/

        public ActionResult Index(int? page, string id)
        {
			var tasks = new List<Task>();
			if (id == "pendding")
			{
				tasks = taskService.GetTasks().Where(m => m.Type != "客户" && m.DateCreated <= DateTime.Now  && m.DateEnd >= DateTime.Now).ToList();
			}
			else
			{
				tasks = taskService.GetTasks().Where(m => m.Type != "客户").ToList();
			}
			
			var model = new Paginated<Task>(tasks, page ?? 1, 10);

            return View(model);
        }

		public ActionResult Add()
		{
			var task = new Task();
			task.DateCreated = DateTime.Now;
			task.UserID = GlobalHelper.UserID;
			task.DateEnd = DateTime.Now;
			
			return View(task);
		}

		[HttpPost]
		public ActionResult Add(Task task)
		{
			if (ModelState.IsValid)
			{
				taskService.InsertTask(task);
				taskService.Save();

				return RedirectToAction("Index");
			}

			return View(task);
		}

		public ActionResult Edit(int id)
		{
			var task = taskService.GetTask(id);

			return View(task);
		}

		[HttpPost]
		public ActionResult Edit(Task task)
		{
			if (ModelState.IsValid)
			{
				taskService.UpdateTask(task);
				taskService.Save();

				return RedirectToAction("Index");
			}

			return View(task);
		}

		public ActionResult Delete(int id)
		{
			taskService.DeleteTask(id);
			taskService.Save();

			return RedirectToAction("Index");
		}
    }
}
