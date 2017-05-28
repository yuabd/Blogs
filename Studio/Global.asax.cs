using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using Studio.Models;
using System.Web.Security;
using System.IO;
using Studio.Services;
using Studio.App_Start;
using System.Web.Optimization;

namespace Studio
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    "Default", // 路由名称
            //    "{controller}/{action}/{id}", // 带有参数的 URL
            //    new { controller = "Company", action = "Index", id = UrlParameter.Optional }, // 参数默认值
            //    new[] { "Studio.Controllers" }
            //);

            //routes.MapRoute("NoAction", "{controller}.html", new { controller = "Company", action = "index", id = UrlParameter.Optional }, new[] { "Studio.Controllers" });//无Action的匹配
            routes.MapRoute("NoID", "{controller}/{action}.html", new { controller = "Home", action = "index", id = UrlParameter.Optional }, new[] { "Studio.Controllers" });//无ID的匹配
            routes.MapRoute("Html", "{controller}/{action}/{id}.html", new { controller = "Home", action = "index", id = UrlParameter.Optional }, new[] { "Studio.Controllers" });//html的匹配
            //routes.MapRoute("Default", "{controller}/{action}/{id}.html", new { controller = "Home", action = "index", id = UrlParameter.Optional }, new[] { "Studio.Controllers" });//默认匹配
            routes.MapRoute("Root", "", new { controller = "Home", action = "index", id = UrlParameter.Optional }, new[] { "Studio.Controllers" });//根目录匹配


            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "index", id = UrlParameter.Optional }, new[] { "Studio.Controllers" });//默认匹配
            //routes.MapRoute("Admin", "{areas}/{controller}/{action}/{id}", new { controller = "Blog", action = "index", id = UrlParameter.Optional }, new[] { "Studio.Areas.Admin.Controllers" });//根目录匹配
        }

        protected void Application_Start()
        {
            Database.SetInitializer<SiteDataContext>(new SiteDataContextInitializer());

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //protected void Application_Error(object s, EventArgs e)
        //{
        //    Exception ex = Server.GetLastError();

        //    if (ex.GetType().Name == "HttpException")
        //    {

        //        HttpException exception = (HttpException)ex;

        //        if (exception.GetHttpCode() == 404)
        //        {
        //            Server.Transfer("/Company/NotFound.html");
        //        }
        //    }
        //}

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket ticket = id.Ticket;

                        // Get the stored user-data, in this case, our roles
                        string userData = ticket.UserData;
                        string[] roles = userData.Split(',');
                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(id, roles);
                    }
                }
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //StaticContentRewrite();
        }

        /// <summary>
        /// 处理静态发布内容
        /// </summary>
        private void StaticContentRewrite()
        {
            var url = Request.RawUrl;

            if ((url.ToLower().IndexOf(".html") > 0 || Context.Request.FilePath == "/"))
            {
                if (url.ToLower().Contains("/blog/post"))
                {
                    var ii = url.Split('/').LastOrDefault();

                    new BlogService().GetBlog(ii.Split('.')[0]);
                }
                
                var finalurl = new GlobalHelper().GetUrl(url);

                var file = new FileInfo(Server.MapPath(finalurl));

                if (file.Exists && file.LastWriteTime.AddHours(2) >= DateTime.Now)
                {
                    Context.Response.StatusCode = 200;
                    Context.RewritePath(finalurl);
                }
            }
        }
    }
}