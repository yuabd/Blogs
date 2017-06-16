using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Studio.Controllers
{
    public class DownloadController : Controller
    {
        // GET: DownloadDefault
        public ActionResult Hosts()
        {
            var xml = XDocument.Load(Server.MapPath("~/SiteSettings.xml"));
            XAttribute field;
            field = (from m in xml.Descendants("downloadCount") select m.Attribute("value")).SingleOrDefault();
            field.SetValue((int.Parse(field.Value) + 1));
            xml.Save(Server.MapPath("~/SiteSettings.xml"));

            return File(new FileStream(HttpContext.Server.MapPath("/Content/Hosts/Hosts.zip"), FileMode.Open), "application/octet-stream", Server.UrlEncode("hosts.zip"));
        }
    }
}