using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Blogs.Model.DbModels;
using Blogs.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Blogs.Web.Areas.Admin.Controllers
{
	[Area("admin")]
    public class LoginController : Controller
    {
		private SiteDataContext db;

		public LoginController(SiteDataContext _db)
		{
			db = _db;
		}

		public IActionResult Index()
        {
			ViewBag.Cookies = HttpContext.Request.Cookies;

			return View();
        }

		[HttpPost]
		public async Task<IActionResult> Do(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				ViewBag.LoginError = "账号或密码错误！";
				if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.PasswordHash))
				{
					return View(nameof(LoginController.Index), model);
				}

				var enPass = EncryptPassword(model.PasswordHash);

				var id = 0;
				int.TryParse(model.UserName, out id);

				if (id == 0)
				{
					return View(nameof(LoginController.Index), model);
				}

				var user = db.User.Where(m => m.UserID == id && m.Password == enPass).FirstOrDefault();

				if(user == null)
				{
					return View(nameof(LoginController.Index), model);
				}

				var identity = new
					ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);//一定要声明AuthenticationScheme
				identity.AddClaim(new Claim(ClaimTypes.Name, user.UserID.ToString()));
				identity.AddClaim(new Claim(ClaimTypes.Sid, user.UserID.ToString()));

				await HttpContext.SignInAsync(identity.AuthenticationType, new ClaimsPrincipal(identity), 
					new Microsoft.AspNetCore.Authentication.AuthenticationProperties
				{
					IsPersistent = true,
					ExpiresUtc = DateTimeOffset.Now.Add(TimeSpan.FromDays(7)), // 有效时间
					AllowRefresh = false
				});

				return Redirect("/Admin/Blog");
			}
			else
			{
				return View(nameof(LoginController.Index), model);
			}
		}

		private string EncryptPassword(string clearPassword)
		{
			using (var md5 = MD5.Create())
			{
				var result = md5.ComputeHash(Encoding.UTF8.GetBytes(clearPassword));
				var strResult = BitConverter.ToString(result);

				return strResult.Replace("-","").ToLowerInvariant();
			}
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			return RedirectToAction(nameof(Web.Controllers.HomeController.Index), "Home");
		}

		public IActionResult On()
		{
			return Json(new
			{
				User.Identity.IsAuthenticated,
				User.Identity.AuthenticationType,
				Claims = User.Claims.Select(c => new { c.Type, c.Value })
			});
		}
			}
}