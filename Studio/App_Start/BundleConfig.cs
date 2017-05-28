using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Studio.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/bootstrap").Include(
                "~/App_Themes/home/css/bootstrap.min.css",
                "~/App_Themes/home/css/style.css")
                );

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/App_Themes/home/scripts/jquery-{version}.js",
                "~/App_Themes/home/scripts/bootstrap.min.js",
                "~/App_Themes/home/scripts/jquery.unobtrusive-ajax.min.js")
                );
        }
    }
}