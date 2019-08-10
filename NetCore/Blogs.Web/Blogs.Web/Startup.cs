using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogs.Model.DbModels;
using Blogs.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Quartz;
using Quartz.Impl;

namespace Blogs.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			var connection = Configuration.GetConnectionString("SqlServer");
			services.AddDbContext<SiteDataContext>(options =>
			{
				options.UseSqlServer(connection, m => m.MigrationsAssembly("Blogs.Web").UseRowNumberForPaging());
			});

			services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			//services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();//注册ISchedulerFactory的实例。

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
				AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
				{
					options.LoginPath = "/Admin/Login";
					options.LogoutPath = "/Admin/Login/Logout";
					options.Cookie.HttpOnly = true;
					options.Cookie.Expiration = TimeSpan.FromDays(150);
					options.AccessDeniedPath = "/Admin/Login/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
					options.SlidingExpiration = true;
					options.Cookie.Name = "_Henhaoji";
				});

			//services.AddIdentity<IdentityUser, IdentityRole>()
			//	.AddEntityFrameworkStores<SiteDataContext>()
			//	.AddDefaultTokenProviders();

			services.AddMvc();

			services.AddResponseCaching();

			services.AddLogging();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles(new StaticFileOptions()
			{
				OnPrepareResponse = ctx =>
				{
					ctx.Context.Response.Headers.Add("cache-control", new[] {
							"public,max-age=2628000" });
					ctx.Context.Response.Headers.Add("Expires", new[] {
							DateTime.UtcNow.AddMonths(1).ToString("R") }); // Format RFC1123

				}
			});
			app.UseCookiePolicy();

			app.UseMiddleware<AuthenticationMiddleware>();
			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");

				routes.MapRoute(
					name: "areas",
					template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
				  );
			});

			app.Use(async (context, next) =>
			{
				context.Response.Headers["Server"] = "Big Server";

				await next();
			});

			app.UseResponseCaching();
			SiteDataContextInitializer.Seed(app.ApplicationServices);

			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug(LogLevel.Debug);
		}
	}
}
