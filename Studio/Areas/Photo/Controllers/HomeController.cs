using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Studio.Models;
using Studio.Services;
using System.IO;

namespace Studio.Areas.Photo.Controllers
{
	public class HomeController : Controller
	{
		private PhotoService photoService = new PhotoService();
		//
		// GET: /Photo/Home/

		public ActionResult Index(int? page)
		{
			var photos = photoService.GetPhotoList();
			var model = new Studio.Models.Others.Paginated<PhotoEntity>(photos, page ?? 1, 24);

			return View(model);
		}

		public ActionResult GetValidateCode()
		{
			GlobalHelper vCode = new GlobalHelper();
			string code = vCode.CreateValidateCode(4);
			Session["ValidateCode"] = code;
			byte[] bytes = vCode.CreateValidateGraphic(code);

			return File(bytes, @"image/jpeg");
		}

		public ActionResult Upload()
		{
			return View();
		}

		public ActionResult UploadJson(PhotoEntity param, string validCode)
		{
			if (validCode != Session["ValidateCode"].ToString())
			{
				return Json(-10);
			}

			var result = photoService.InsertPicture(param);

			return Json(result);
		}

		public ActionResult Vote(int id)
		{
			var result = photoService.InsertPhotoVote(id, GlobalHelper.GetWebClientIp());

			return Json(result);
		}

		public ActionResult Detail(int id)
		{
			var model = photoService.GetPhotoByID(id);

			return View(model);
		}

		public ActionResult UploadImage()
		{
			using (PhotoService sa = new PhotoService())
			{
				HttpPostedFileBase file = Request.Files["Filedata"];

				var param = new PhotoDetailEntity();

				var picture = sa.InsertPhotoDetail(param);
				if (picture > 0)
				{
					var guid = Guid.NewGuid();

					string saveUrl = DateTime.Now.ToString("yyyyMM") + @"\" + guid + "-" + picture + ".jpg";

					string url = System.Web.HttpContext.Current.Server.MapPath("/Content/Pictures/Vote/" + saveUrl);

					string directory = Path.GetDirectoryName(url);
					if (directory != null && !Directory.Exists(directory))
					{
						Directory.CreateDirectory(directory);
					}

					if (file != null) file.SaveAs(url);

					file = null;

					return Json(new { queueID = 1, ID = picture, imgUrl = saveUrl.Replace(@"\", "/") });
				}
				else
				{
					return null;
				}
			}
		}

	}
}
