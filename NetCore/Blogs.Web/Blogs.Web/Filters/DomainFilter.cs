using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogs.Web.Filters
{
    public class DomainFilter: ActionFilterAttribute
	{
		public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var url = context.HttpContext.Request.Host.Value;//["SERVER_NAME"].ToLower();
			//WriteInLog(url);

			if (url.Equals("http://henhaoji.com.cn")
				|| url.Equals("henhaoji.com.cn"))
			{
				string newurl = "http://www.henhaoji.com.cn" + context.HttpContext.Request.Path.Value;

				context.HttpContext.Response.StatusCode = 301;
				context.HttpContext.Response.Headers.Add("Location", newurl);// = "301 Moved Permanently";
				//context.HttpContext.Response.AddHeader("Location", newurl);
			}

			//var rawUrl = System.Web.HttpContext.Current.Request.RawUrl.ToLower();
			//if (rawUrl.Equals("/company/index.html") || rawUrl.Equals("/company.html") || rawUrl.Equals("/company"))
			//{
			//	string newurl = "http://blog.henhaoji.com.cn";
			//	System.Web.HttpContext.Current.Response.Clear();
			//	System.Web.HttpContext.Current.Response.StatusCode = 301;
			//	System.Web.HttpContext.Current.Response.Status = "301 Moved Permanently";
			//	System.Web.HttpContext.Current.Response.AddHeader("Location", newurl);
			//}

			return base.OnActionExecutionAsync(context, next);
		}
	}
}
