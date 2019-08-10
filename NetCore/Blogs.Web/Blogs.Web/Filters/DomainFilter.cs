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

			if (url.ToLower().Equals("http://henhaoji.com.cn"))
			{
				string newurl = "http://www.henhaoji.com.cn"  + context.HttpContext.Request.Path.Value + context.HttpContext.Request.QueryString.Value;

				context.HttpContext.Response.StatusCode = 301;
				context.HttpContext.Response.Headers.Add("Location", newurl);
			}

			if (context.HttpContext.Request.Path.Value == "/")
			{
				var path = context.HttpContext.Request.QueryString.Value;

				if (path.ToLower().Contains("hmsr=bt"))
				{
					string newurl = "http://www.henhaoji.com.cn";

					context.HttpContext.Response.StatusCode = 301;
					context.HttpContext.Response.Headers.Add("Location", newurl);
				}
			}

			return base.OnActionExecutionAsync(context, next);
		}
	}
}
