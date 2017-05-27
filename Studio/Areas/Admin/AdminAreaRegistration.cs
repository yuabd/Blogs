using System.Web.Mvc;

namespace Studio.Areas.Admin
{
	public class AdminAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "Admin";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"Admin_default",
				"Admin/{controller}/{action}/{id}",
				new { controllers = "Blog" ,action = "Index", id = UrlParameter.Optional },
				new[] { "Studio.Areas.Admin.Controllers" }
			);
		}
	}
}
