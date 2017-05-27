using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Studio.Models;
using Studio.Services;

namespace Studio.Controllers
{
    public class UploadController : Controller
    {
        //
        // GET: /Upload/

		public ActionResult UploadImage()
		{
			using (SiteService sa = new SiteService())
			{
				HttpPostedFileBase file = Request.Files["Filedata"];

				var param = new Picture();
				param.Type = PictureType.CasePicture;
				param.ParentID = Convert.ToInt32(Request["ID"]);

				var picture = sa.InsertPicture(param);
				if (picture.Result > 0)
				{
                    var fileName = Guid.NewGuid().ToString();
                    var saveUrl = Studio.Helpers.IO.UploadImageFile(file.InputStream, "/Content/Pictures/Case/", fileName, 800, 600, Studio.ImageHelper.ImageSaveType.Cut);

					return Json(new { queueID = 1, ID = picture.Result, imgUrl = saveUrl.Replace(@"\", "/") });
				}
				else
				{
					return null;
				}
			}
		}

    }
}
