using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Studio.Models;
using Studio.Services;
using Studio.Models.Site;

namespace Studio.Controllers
{
	[TestFilter]
    public class AccountController : Controller
    {
		private MembershipService membershipService = new MembershipService();

        //
        // GET: /Account/

        public ActionResult Login(string returnUrl)
		{
			LoginViewModel model = new LoginViewModel();

			return View(model);
		}

		[HttpPost]
		public ActionResult Login(LoginViewModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				var loginMessage = membershipService.Login(model.UserID, model.Password, model.RememberMe);

				if (loginMessage == "OK")
				{
					if (!string.IsNullOrEmpty(returnUrl))
						return Redirect(returnUrl.ToString());
					else
						return RedirectToAction("Index", "Blog", new { area = "Admin" });
				}
				else
				{
					ViewBag.LoginError = loginMessage;
					return View(model);
				}
			}
			else
			{
				return View(model);
			}
		}

		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			Session.Abandon();

			return Redirect("/");
		}

    }
}
