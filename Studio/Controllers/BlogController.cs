using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Studio.Services;
using Studio.Models;
using Studio.Models.Site;
using Studio.Models.Others;
using Studio.Helpers;

namespace Studio.Controllers
{
    [DomainFilter]
    public class BlogController : BaseController
    {
        private BlogService bs = new BlogService();

        // GET: /Blog/
        //[GenerateStaticFileAttribute]
        public ActionResult Index(int? page, string keywords)
        {
            var blogs = bs.GetBlogs().Where(m => m.IsPublic == true);

            if (!string.IsNullOrEmpty(keywords))
            {
                var key = keywords.Split(' ');
                foreach (var item in key)
                {
                    blogs = (from l in blogs
                             where l.BlogTitle.Contains(item) || l.BlogContent.Contains(item)
                             select l);
                }
            }

            ViewBag.Count = blogs.Select(m => m.BlogID).Count();

            var pBlogs = new Paginated<Blog>(blogs.ToList(), page ?? 1, 8);

            var popularTags = bs.GetPopularTags().ToList();

            var model = new BlogsViewModel(pBlogs, null, popularTags, null);

            ViewBag.PageTitle = string.IsNullOrEmpty(keywords) ? "All Post" : "搜索结果: " + keywords;
            if (page.HasValue)
            {
                ViewBag.PageTitle += "_第" + page + "页";
            }

            return View(model);
        }

        //[GenerateStaticFileAttribute]
        public ActionResult Categories(int? id, int? page)
        {
            var ids = id.Uint();
            var blogs = bs.GetBlogsByCategory(ids).Where(m => m.IsPublic == true);

            ViewBag.Count = blogs.Select(m => m.BlogID).Count();

            var pBlogs = new Paginated<Blog>(blogs, page ?? 1, 8);

            var popularTags = bs.GetPopularTags().ToList();

            var model = new BlogsViewModel(pBlogs, null, popularTags, null);

            ViewBag.PageTitle = bs.GetBlogCategory(id.Value).CategoryName;
            //ViewBag.Blog = "current";

            if (page.HasValue)
            {
                ViewBag.PageTitle += "_第" + page + "页";
            }
            return View("Index", model);
        }

        //[GenerateStaticFileAttribute]
        public ActionResult Tags(string id, int? page)
        {
            var blogs = bs.GetBlogsByTag(id).Where(m => m.IsPublic == true);

            ViewBag.Count = blogs.Select(m => m.BlogID).Count();

            var pBlogs = new Paginated<Blog>(blogs, page ?? 1, 8);

            var popularTags = bs.GetPopularTags().ToList();

            var model = new BlogsViewModel(pBlogs, null, popularTags, null);

            ViewBag.PageTitle = "Tag: " + id;
            if (page.HasValue)
            {
                ViewBag.PageTitle += "_第" + page + "页";
            }

            return View("Index", model);
        }

        //[GenerateStaticFileAttribute]
        public ActionResult Archives(string id, string month, int? page)
        {
            string type = "month";

            if (string.IsNullOrEmpty(month))
            {
                type = "year";
            }

            if (string.IsNullOrEmpty(id))
            {
                id = DateTime.Now.Year.ToString();
            }

            var blogs = bs.GetBlogsByArchive(id, month, type).Where(m => m.IsPublic == true);

            ViewBag.Count = blogs.Select(m => m.BlogID).Count();

            var pBlogs = new Paginated<Blog>(blogs, page ?? 1, 8);

            //var categories = bs.GetBlogCategories().ToList();
            //var popularTags = bs.GetPopularTags().Take(10).ToList();
            //var archives = bs.GetArchives().ToList();
            var popularTags = bs.GetPopularTags().ToList();

            var model = new BlogsViewModel(pBlogs, null, popularTags, null);

            ViewBag.PageTitle = string.Format("{0}{1}", string.IsNullOrEmpty(id) ? "" : (id + "年"), month);
            if (page.HasValue)
            {
                ViewBag.PageTitle += "_第" + page + "页";
            }
            return View("Index", model);
        }

        //[GenerateStaticFileAttribute]
        public ActionResult Post(string id)
        {
            var blog = bs.GetBlog(id);

            if (blog == null)
            {
                //string newurl = "http://www.henhaoji.com.cn" + System.Web.HttpContext.Current.Request.RawUrl;
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.StatusCode = 404;
                System.Web.HttpContext.Current.Response.Status = "404 Moved Permanently";
                //System.Web.HttpContext.Current.Response.AddHeader("Location", "");
                //Response.Redirect("/404.html");
                //return View("NotFound");
                Response.End();
            }

            //var blogComment = new BlogComment();
            var blogID = blog == null ? 0 : blog.BlogID;
            //blogComment.BlogID = blogID;
            //blogComment.IsPublic = true;
            //blogComment.ValidationCodeSource = DateTime.Now.Millisecond.ToString();

            ViewBag.CommentCount = bs.GetBlogComments(blogID).Where(m => m.IsPublic == true).Select(m => m.BlogID).Count();
            //var categories = bs.GetBlogCategories().ToList();
            var popularTags = bs.GetPopularTags().Take(10).ToList();
            //var archives = bs.GetArchives().ToList();

            var preNextBlog = bs.GetPreNextBlog(blogID);
            var model = new BlogViewModel(blog, new BlogComment(), null, null, popularTags, null, preNextBlog);

            ViewBag.Blog = "current";
            return View(model);
        }

        public ActionResult Captcha()
        {
            Captcha captcha = new Captcha(85, 32, 5, 25f);
            Session["Captcha"] = captcha.Text;
            return File(captcha.ImageData, "image/jpeg");
        }

        public ActionResult GetApprovedCommentOfPost(int id)
        {
            var comments = bs.GetComments().Where(m => m.IsPublic == true && m.BlogID == id).ToList();

            return View(comments);
        }

        [HttpPost]
        public ActionResult AddComment(BlogComment blogComment, string CaptchaCode)
        {
            try
            {
                if (Session["Captcha"] != null && Session["Captcha"].ToString() == CaptchaCode)
                {
                    bs.InsertBlogComment(blogComment);

                    return Json(new { code = 1 }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { code = -1 }, JsonRequestBehavior.AllowGet);

                throw new Exception();
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}
