using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Studio.Models;

namespace Studio.Services
{
	public class TaskService
	{
		SiteDataContext db = new SiteDataContext();

		//public void InsertTask(Task task)
		//{
		//	db.Tasks.Add(task);
		//}

		//public Task GetTask(int taskID)
		//{
		//	return db.Tasks.Find(taskID);
		//}

		//public void UpdateTask(Task task)
		//{
		//	var t = GetTask(task.TaskID);
		//	t.Status = task.Status;
		//	t.Description = task.Description;
		//	t.DateCreated = task.DateCreated;
		//	t.DateEnd = task.DateEnd;
		//}

		//public void DeleteTask(int taskID)
		//{
		//	var task = GetTask(taskID);
		//	db.Tasks.Remove(task);
		//}

		//public IQueryable<Task> GetTasks()
		//{
		//	return db.Tasks.Where(m => m.UserID == GlobalHelper.UserID).OrderByDescending(m => m.DateCreated);
		//}

		////用于客户
		//public IQueryable<Task> GetTasks(string companyName)
		//{
		//	return db.Tasks.Where(m => m.UserID == GlobalHelper.UserID && m.ID == companyName).OrderByDescending(m => m.DateCreated);
		//}

		public void Save()
		{
			db.SaveChanges();
		}

		~TaskService()
		{
			db.Dispose();
		}
	}
}