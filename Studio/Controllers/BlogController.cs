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
        //BlogService blogService = new BlogService();
        //
        // GET: /Blog/

        //[GenerateStaticFileAttribute]
        //[OutputCache(Duration = 5, VaryByParam = "*")]
        public ActionResult Index(int? page, string keywords)
        {
            var blogs = db.Blogs.Where(m => m.IsPublic == true).OrderByDescending(m => m.DateCreated).ToList();

            if (!string.IsNullOrEmpty(keywords))
            {
                var key = keywords.Split(' ');
                foreach (var item in key)
                {
                    blogs = (from l in blogs
                             where l.BlogTitle.Contains(item) || l.BlogContent.Contains(item)
                             select l).ToList();
                }
            }

            var pBlogs = new Paginated<Blog>(blogs, page ?? 1, 8);

            var categories = new BlogHelp().GetBlogCategories().ToList();

            var popularTags = (from p in db.BlogTags
                               group p by new { p.Tag } into t
                               orderby t.Count() descending
                               select new Anonymous { Tag = t.Key.Tag, Num = t.Count() }).Take(10).ToList();

            var archives = new BlogHelp().GetArchives().ToList();

            var model = new BlogsViewModel(pBlogs, categories, popularTags, archives);
            ViewBag.PageTitle = string.IsNullOrEmpty(keywords) ? "新闻动态" : "搜索结果: " + keywords;
            ViewBag.Blog = "current";

            return View(model);
        }

        //[GenerateStaticFileAttribute]
        public ActionResult Categories(int? id, int? page)
        {
            var ids = id.Uint();
            var blogs = new BlogHelp().GetBlogsByCategory(ids).Where(m => m.IsPublic == true).ToList();

            var pBlogs = new Paginated<Blog>(blogs, page ?? 1, 8);

            var categories = new BlogHelp().GetBlogCategories().ToList();
            var popularTags = new BlogHelp().GetPopularTags().ToList();
            var archives = new BlogHelp().GetArchives().ToList();

            var model = new BlogsViewModel(pBlogs, categories, popularTags, archives);

            ViewBag.PageTitle = Studio.Helpers.LabelHelper.GetBlogCategoryName(ids);
            ViewBag.Blog = "current";

            return View("Index", model);
        }

        //[GenerateStaticFileAttribute]
        public ActionResult Tags(string id, int? page)
        {
            var blogs = new BlogHelp().GetBlogsByTag(id).Where(m => m.IsPublic == true).ToList();
            var pBlogs = new Paginated<Blog>(blogs, page ?? 1, 8);

            var categories = new BlogHelp().GetBlogCategories().ToList();
            var popularTags = new BlogHelp().GetPopularTags().Take(10).ToList();
            var archives = new BlogHelp().GetArchives().ToList();

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

            var blogs = new BlogHelp().GetBlogsByArchive(id, month, type).Where(m => m.IsPublic == true);
            var pBlogs = new Paginated<Blog>(blogs, page ?? 1, 8);

            var categories = new BlogHelp().GetBlogCategories().ToList();
            var popularTags = new BlogHelp().GetPopularTags().Take(10).ToList();
            var archives = new BlogHelp().GetArchives().ToList();

            var model = new BlogsViewModel(pBlogs, categories, popularTags, archives);

            ViewBag.PageTitle = string.Format("{0}{1}", string.IsNullOrEmpty(id) ? "" : (id + "年"), month);

            ViewBag.Blog = "current";
            return View("Index", model);
        }

        //[GenerateStaticFileAttribute]
        public ActionResult Post(string id)
        {
            var blog = new BlogHelp().GetBlog(id);

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

            var blogComments = new BlogHelp().GetBlogComments(blogID).Where(m => m.IsPublic == true).ToList();
            var categories = new BlogHelp().GetBlogCategories().ToList();
            var popularTags = new BlogHelp().GetPopularTags().Take(10).ToList();
            var archives = new BlogHelp().GetArchives().ToList();

            var preNextBlog = new BlogHelp().GetPreNextBlog(blogID);
            var model = new BlogViewModel(blog, new BlogComment(), blogComments, categories, popularTags, archives, preNextBlog);

            ViewBag.Blog = "current";
            return View(model);
        }

        [HttpPost]
        public ActionResult AddComment(BlogComment blogComment)
        {
            var blog = new BlogHelp().GetBlog(blogComment.BlogID);
            var blogComments = new BlogHelp().GetBlogComments(blog.BlogID).ToList();
            var categories = new BlogHelp().GetBlogCategories().ToList();
            var popularTags = new BlogHelp().GetPopularTags().Take(10).ToList();
            var archives = new BlogHelp().GetArchives().ToList();
            var preNextBlog = new BlogHelp().GetPreNextBlog(blog.BlogID);

            if (ModelState.IsValid)
            {
                new BlogHelp().InsertBlogComment(blogComment);

                var template = new Studio.Models.Others.MessageTemplate("/content/post.htm");
                template.Set("ContactName", blogComment.Name);
                template.Set("Email", blogComment.Email);
                template.Set("Message", blogComment.Message);

                var mail = new Studio.Models.Others.MailBag();
                mail.ToMailAddress = blogComment.Email;
                mail.Message = template.Content;
                mail.CcMailAddress = "yuabd1991@gmail.com";
                mail.Subject = template.Subject;
                mail.Send(true);

                return RedirectToAction("Post", new { id = blog.Slug });
            }

            var model = new BlogViewModel(blog, blogComment, blogComments, categories, popularTags, archives, preNextBlog);

            return View("Post", model);
        }

    }
}
