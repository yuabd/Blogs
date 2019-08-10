using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogs.Model.DbModels;
using Blogs.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Blogs.Web.Controllers
{
	public class MCCController : Controller
	{
		private SiteDataContext db;
		private IHttpContextAccessor _accessor;

		public MCCController(SiteDataContext _context, IHttpContextAccessor accessor)
		{
			db = _context;
			_accessor = accessor;
		}

		public IActionResult Index()
		{
			return View();
		}

		//[ResponseCache(Duration = 3600)]
		[HttpPost]
		public async Task<IActionResult> Result(string mcc)
		{
			if (string.IsNullOrWhiteSpace(mcc))
			{
				TempData["Error"] = "请输入搜索内容";
				TempData["id"] = mcc;
				return Redirect("/mcc");
			}

			int c = 0;
			int.TryParse(mcc, out c);
			if (c > 0)
			{
				if (mcc.Length < 3)
				{
					TempData["Error"] = "请输入至少3个数字";
					TempData["id"] = mcc;
					return Redirect("/mcc");
				}
			}
			else
			{
				if (mcc.Length < 2)
				{
					TempData["Error"] = "请输入至少2个文字";
					TempData["id"] = mcc;

					return Redirect("/mcc");
				}
			}

			mcc = mcc.Trim();

			var mccs = await (from l in db.POS_MCC.AsNoTracking()
							  where l.MCC.Contains(mcc) || l.Description.Contains(mcc)
							  select l).ToListAsync();

			var licenses = await (from l in db.POS_PaymentLicense.AsNoTracking()
								  where l.CompanyName.Contains(mcc) || l.LicenseID == mcc
								  select l).ToListAsync();

			var areas = await (from l in db.POS_MerchantArea.AsNoTracking()
							   where l.MerchantAreaCode.Contains(mcc) || l.MerchantAreaName.Contains(mcc)
							   select l).ToListAsync();

			var agency = await (from l in db.POS_Agency.AsNoTracking()
								where l.AgencyCode.Contains(mcc) || l.AgencyName.Contains(mcc)
								select l).ToListAsync();

			var model = new MccViewModel();
			model.Agency = agency;
			model.Areas = areas;
			model.Licenses = licenses;
			model.Mccs = mccs;

			ViewBag.Id = mcc;

			return View(model);
		}

		//public ActionResult Temp()
		//{
		//	return View();
		//}

		//public async Task<IActionResult> TempSave(IFormFile filedata)
		//{
		//	string saveUrl = DateTime.Now.ToString("yyyyMM") + @"\" + filedata.FileName;
		//	var folderPath = "tempData";
		//	string FileServerPaht = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderPath, saveUrl);
		//	using (var stream = new FileStream(FileServerPaht, FileMode.Create))
		//	{
		//		//再把文件保存的文件夹中
		//		filedata.OpenReadStream().CopyTo(stream);
		//	}
		//	var list = new List<POS_PaymentLicense>();
		//	using (FileStream fs = new FileStream(FileServerPaht, FileMode.Open, FileAccess.Read))
		//	{
		//		HSSFWorkbook hssfworkbook = new HSSFWorkbook(fs);
		//		ISheet sheet = hssfworkbook.GetSheetAt(0);

		//		//数据
		//		for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
		//		{
		//			list.Add(new POS_PaymentLicense()
		//			{
		//				LicenseID = GetValueTypeForXLS(sheet.GetRow(i).GetCell(0) as HSSFCell).ToString().Trim(),
		//				CompanyName = GetValueTypeForXLS(sheet.GetRow(i).GetCell(1) as HSSFCell).ToString().Trim(),
		//				BusType = GetValueTypeForXLS(sheet.GetRow(i).GetCell(4) as HSSFCell).ToString().Trim(),
		//				Date = Convert.ToDateTime(GetValueTypeForXLS(sheet.GetRow(i).GetCell(2) as HSSFCell).ToString()),
		//				VisitCount = 0,
		//				Scope = GetValueTypeForXLS(sheet.GetRow(i).GetCell(3) as HSSFCell).ToString().Trim(),
		//				StartDate = Convert.ToDateTime(GetValueTypeForXLS(sheet.GetRow(i).GetCell(5) as HSSFCell).ToString()),
		//				EndDate = Convert.ToDateTime(GetValueTypeForXLS(sheet.GetRow(i).GetCell(6) as HSSFCell).ToString()),
		//				Remark = GetValueTypeForXLS(sheet.GetRow(i).GetCell(7) as HSSFCell) + ""

		//			});
		//		}
		//	}

		//	await db.POS_PaymentLicense.AddRangeAsync(list);
		//	await db.SaveChangesAsync();

		//	return Json(new { });
		//}


		//private object GetValueTypeForXLS(HSSFCell cell)
		//{
		//	if (cell == null)
		//		return null;
		//	switch (cell.CellType)
		//	{
		//		case CellType.Blank: //BLANK:
		//			return null;
		//		case CellType.Boolean: //BOOLEAN:
		//			return cell.BooleanCellValue;
		//		case CellType.Numeric: //NUMERIC:
		//			return cell.NumericCellValue;
		//		case CellType.String: //STRING:
		//			return cell.StringCellValue;
		//		case CellType.Error: //ERROR:
		//			return cell.ErrorCellValue;
		//		case CellType.Formula: //FORMULA:
		//		default:
		//			return cell.StringCellValue;
		//	}
		//}
	}

}