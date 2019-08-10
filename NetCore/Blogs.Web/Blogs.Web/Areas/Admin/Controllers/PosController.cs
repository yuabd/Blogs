using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Blogs.Model.DbModels;
using Blogs.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Blogs.Web.Areas.Admin.Controllers
{
	[Area("admin"), Authorize]
    public class PosController : Controller
    {
		private SiteDataContext db;
		private IHttpContextAccessor _accessor;
		//private readonly ISchedulerFactory _schedulerFactory;
		//private IScheduler _scheduler;
		private readonly IHostingEnvironment _hostingEnvironment;

		public PosController(SiteDataContext _context,
			IHttpContextAccessor accessor
			//,ISchedulerFactory schedulerFactory
			, IHostingEnvironment hostingEnvironment
			)
		{
			db = _context;
			_accessor = accessor;
			//this._schedulerFactory = schedulerFactory;
			_hostingEnvironment = hostingEnvironment;
		}

		public async Task<IActionResult> MCC(string keywords, int? page)
        {
			var blogs = db.POS_MCC.AsNoTracking();
			if (!string.IsNullOrWhiteSpace(keywords))
			{
				blogs = from l in blogs
						where l.MCC.Contains(keywords)
						|| l.Description.Contains(keywords)
						select l;
			}
			var pblogs = new Paginated<POS_MCC>(page ?? 1, 25, _accessor);
			await pblogs.Init(blogs.OrderByDescending(m => m.DateCreated));
			ViewBag.keywords = keywords;
			return View(pblogs);
		}

		public ActionResult Mcc_Add()
		{
			var model = new POS_MCC();
			model.DateCreated = DateTime.Now;
			model.IsPoint = true;
			model.NoPointBankStr = "无";

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Mcc_Add(POS_MCC model)
		{
			model.VisitCount = 0;

			if (ModelState.IsValid)
			{
				var bankStr = model.NoPointBankStr.Split(';', StringSplitOptions.RemoveEmptyEntries);

				foreach (var item in bankStr)
				{
					var bank = await db.POS_Bank.FirstOrDefaultAsync(m => m.BankName == item);
					if(bank != null)
					{
						await db.POS_MCCBankNoPoint.AddAsync(new POS_MCCBankNoPoint()
						{
							BankID = bank.BankID,
							MCC = model.MCC
						});
					}
				}
				
				await db.POS_MCC.AddAsync(model);


				await db.SaveChangesAsync();

				return Redirect("/Admin/Pos/MCC");
			}

			return View(model);
		}

		public async Task<IActionResult> Mcc_Edit(string id)
		{
			var model =await  db.POS_MCC.FirstOrDefaultAsync(m => m.MCC == id);
			if (model == null)
			{
				return Redirect("/Admin/Pos/Mcc");
			}

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Mcc_Edit(POS_MCC model)
		{
			var dbModel = await db.POS_MCC.FirstOrDefaultAsync(m => m.MCC == model.MCC);

			if(dbModel == null)
			{
				return View(model);
			}

			if (ModelState.IsValid)
			{
				var bankStr = model.NoPointBankStr.Split(';', StringSplitOptions.RemoveEmptyEntries);

				var bankList = await db.POS_MCCBankNoPoint.Where(m => m.MCC == model.MCC).ToListAsync();
				db.POS_MCCBankNoPoint.RemoveRange(bankList);


				dbModel.Description = model.Description;
				dbModel.IsPoint = model.IsPoint;
				dbModel.NoPointBankStr = model.NoPointBankStr;
				dbModel.OldRate = model.OldRate;
				dbModel.Rate = model.Rate;
				dbModel.DateCreated = model.DateCreated;
				
				await db.SaveChangesAsync();
				
				foreach (var item in bankStr)
				{
					var bank = await db.POS_Bank.FirstOrDefaultAsync(m => m.BankName == item);
					if (bank != null)
					{
						await db.POS_MCCBankNoPoint.AddAsync(new POS_MCCBankNoPoint()
						{
							BankID = bank.BankID,
							MCC = model.MCC
						});
					}
				}

				await db.SaveChangesAsync();
				return Redirect("/Admin/Pos/MCC");
			}

			return View(model);
		}

		public async Task<IActionResult> Mcc_Delete(string id, int? page)
		{
			if (!page.HasValue)
				page = 1;

			var dbModel = await db.POS_MCC.FirstOrDefaultAsync(m => m.MCC == id);

			if (dbModel == null)
			{
				return Redirect("/Admin/Pos/Mcc?page=" + page.Value);
			}
			var bankList = await db.POS_MCCBankNoPoint.Where(m => m.MCC == id).ToListAsync();
			db.POS_MCCBankNoPoint.RemoveRange(bankList);

			db.POS_MCC.Remove(dbModel);

			await db.SaveChangesAsync();

			return Redirect("/Admin/Pos/Mcc?page=" + page.Value);
		}

		public async Task<IActionResult> GetBanks()
		{
			var banks = await db.POS_Bank.ToListAsync();

			return Json(banks);
		}
	}
}