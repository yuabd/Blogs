using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Studio.Services;
using Studio.Models.Others;
using Studio.Models;
using Studio.Models.Site;
using Studio.Helpers;

namespace Studio.Controllers
{
    [TestFilter]
    public class CompanyController : Controller
    {
        //private SiteService siteService = new SiteService();
        //
        // GET: /Company/
        [GenerateStaticFileAttribute]
        public ActionResult Index()
        {
            //if (System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToLower().Equals("henhaoji.com.cn"))
            //{
            //    string newurl = "http://www.henhaoji.com.cn" + System.Web.HttpContext.Current.Request.RawUrl;
            //    System.Web.HttpContext.Current.Response.Clear();
            //    System.Web.HttpContext.Current.Response.StatusCode = 301;
            //    System.Web.HttpContext.Current.Response.Status = "301 Moved Permanently";
            //    System.Web.HttpContext.Current.Response.AddHeader("Location", newurl);
            //}
            ViewBag.Links = new SiteHelp().GetLinks();

            ViewBag.Home = "home";
            ViewBag.Index = "current";

            return View();
        }

        [GenerateStaticFileAttribute]
        public ActionResult About()
        {
            ViewBag.About = "current";

            return View();
        }

        [GenerateStaticFileAttribute]
        public ActionResult Case(int? page, int? id)
        {
            ViewBag.Case = "current";
            ViewBag.Category = new SiteHelp().GetCaseCategories().ToList();

            IEnumerable<Case> cases = null;
            if (id != null)
            {
                cases = new SiteHelp().GetCases(id).ToList();
            }
            else
            {
                cases = new SiteHelp().GetCases().ToList();
            }
            var model = new Paginated<Case>(cases, page ?? 1, 12);

            return View(model);
        }

        [GenerateStaticFileAttribute]
        public ActionResult CaseDetail(int id)
        {
            ViewBag.Case = "current";
            var _case = new SiteHelp().GetCase(id);

            return View(_case);
        }

        [GenerateStaticFileAttribute]
        public ActionResult Service()
        {
            ViewBag.Service = "current";
            return View();
        }

        [GenerateStaticFileAttribute]
        public ActionResult Hostting()
        {

            return View();
        }

        [GenerateStaticFileAttribute]
        public ActionResult DingZhi()
        {
            return View();
        }

        [GenerateStaticFileAttribute]
        public ActionResult Contact()
        {
            ViewBag.Contact = "current";

            var contact = new ContactViewModel();
            contact.ValidationCodeSource = DateTime.Now.Millisecond.ToString();

            return View(contact);
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel contact)
        {
            if (ModelState.IsValid)
            {
                var template = new Studio.Models.Others.MessageTemplate("/content/contact.htm");
                template.Set("ContactName", contact.ContactName);
                template.Set("Email", contact.Email);
                template.Set("Message", contact.Message);

                var mail = new Studio.Models.Others.MailBag();
                mail.ToMailAddress = contact.Email;
                mail.Message = template.Content;
                mail.CcMailAddress = "yuabd1991@gmail.com";
                mail.Subject = template.Subject;
                mail.Send(true);

                ViewBag.ThankYou = true;

                var contact1 = new ContactViewModel();
                contact1.ValidationCodeSource = DateTime.Now.Millisecond.ToString();

                return View(contact1);
            }

            return View(contact);
        }

        //[GenerateStaticFileAttribute(Expiration = 24)]
        //public ActionResult NotFound()
        //{
        //    Response.StatusCode = 404;
        //    Response.Status = "404 Not Found";

        //    return View();
        //}

        //[GenerateStaticFileAttribute(Expiration = 24)]
        //public ActionResult Error()
        //{
        //    Response.StatusCode = 500;

        //    return View();
        //}

        public ActionResult Sitemap()
        {
            return View();
        }
    }
}
