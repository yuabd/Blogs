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
    [TestFilter]
    public class BlogController : BaseController
    {
        private BlogService bs = new BlogService();

        // GET: /Blog/
        [GenerateStaticFileAttribute]
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

            var pBlogs = new Paginated<Blog>(blogs.ToList(), page ?? 1, 8);

            var categories = bs.GetBlogCategories().ToList();

            var popularTags = (from p in bs.GetTags()
                               group p by new { p.Tag } into t
                               orderby t.Count() descending
                               select new Anonymous { Tag = t.Key.Tag, Num = t.Count() }).Take(10).ToList();

            var archives = bs.GetArchives().ToList();

            var model = new BlogsViewModel(pBlogs, categories, popularTags, archives);
            ViewBag.PageTitle = string.IsNullOrEmpty(keywords) ? "All Post" : "搜索结果: " + keywords;
            ViewBag.Blog = "current";

            return View(model);
        }

        //[GenerateStaticFileAttribute]
        public ActionResult Categories(int? id, int? page)
        {
            var ids = id.Uint();
            var blogs = bs.GetBlogsByCategory(ids).Where(m => m.IsPublic == true);

            var pBlogs = new Paginated<Blog>(blogs, page ?? 1, 8);

            var categories = bs.GetBlogCategories().ToList();
            var popularTags = bs.GetPopularTags().ToList();
            var archives = bs.GetArchives().ToList();

            var model = new BlogsViewModel(pBlogs, categories, popularTags, archives);

            ViewBag.PageTitle = bs.GetBlogCategory(id.Value).CategoryName;//.GetBlogCategoryName(ids);
            ViewBag.Blog = "current";

            return View("Index", model);
        }

        //[GenerateStaticFileAttribute]
        public ActionResult Tags(string id, int? page)
        {
            var blogs = bs.GetBlogsByTag(id).Where(m => m.IsPublic == true);
            var pBlogs = new Paginated<Blog>(blogs, page ?? 1, 8);

            var categories = bs.GetBlogCategories().ToList();
            var popularTags = bs.GetPopularTags().Take(10).ToList();
            var archives = bs.GetArchives().ToList();

            var model = new BlogsViewModel(pBlogs, categories, popularTags, archives);

            ViewBag.PageTitle = "Tag: " + id;
            ViewBag.Blog = "current";

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
            var pBlogs = new Paginated<Blog>(blogs, page ?? 1, 8);

            var categories = bs.GetBlogCategories().ToList();
            var popularTags = bs.GetPopularTags().Take(10).ToList();
            var archives = bs.GetArchives().ToList();

            var model = new BlogsViewModel(pBlogs, categories, popularTags, archives);

            ViewBag.PageTitle = string.Format("{0}{1}", string.IsNullOrEmpty(id) ? "" : (id + "年"), month);

            ViewBag.Blog = "current";
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
                return View("NotFound");
            }

            //var blogComment = new BlogComment();
            var blogID = blog == null ? 0 : blog.BlogID;
            //blogComment.BlogID = blogID;
            //blogComment.IsPublic = true;
            //blogComment.ValidationCodeSource = DateTime.Now.Millisecond.ToString();

            var blogComments = bs.GetBlogComments(blogID).Where(m => m.IsPublic == true).ToList();
            var categories = bs.GetBlogCategories().ToList();
            var popularTags = bs.GetPopularTags().Take(10).ToList();
            var archives = bs.GetArchives().ToList();

            var preNextBlog = bs.GetPreNextBlog(blogID);
            var model = new BlogViewModel(blog, new BlogComment(), blogComments, categories, popularTags, archives, preNextBlog);

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

        public ActionResult AddComment(BlogComment blogComment)
        {
            //var blog = bs.GetBlog(blogComment.BlogID);
            //var blogComments = bs.GetBlogComments(blog.BlogID).ToList();
            //var categories = bs.GetBlogCategories().ToList();
            //var popularTags = bs.GetPopularTags().Take(10).ToList();
            //var archives = bs.GetArchives().ToList();
            //var preNextBlog = bs.GetPreNextBlog(blog.BlogID);

            if (ModelState.IsValid)
            {
                bs.InsertBlogComment(blogComment);

                //var template = new Studio.Models.Others.MessageTemplate("/content/post.htm");
                //template.Set("ContactName", blogComment.Name);
                //template.Set("Email", blogComment.Email);
                //template.Set("Message", blogComment.Message);

                //var mail = new Studio.Models.Others.MailBag();
                //mail.ToMailAddress = blogComment.Email;
                //mail.Message = template.Content;
                //mail.CcMailAddress = "yuabd1991@gmail.com";
                //mail.Subject = template.Subject;
                //mail.Send(true);
            }

            return RedirectToAction("GetApprovedCommentOfPost", new { id = blogComment.BlogID });


            //var model = new BlogViewModel(blog, blogComment, blogComments, categories, popularTags, archives, preNextBlog);

            //return View("Post", model);
        }

    }
}
