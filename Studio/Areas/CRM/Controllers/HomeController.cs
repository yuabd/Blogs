using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Studio.Areas.CRM.Controllers
{
	[Authorize(Roles = "Administrator")]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
			ViewBag.Home = "active";

            return View();
        }

    }
}
