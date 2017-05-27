using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Studio.Models;
using Studio.Services;
using System.Web.Script.Serialization;
using Studio.Models.Others;
using Studio.Models.Admin;

namespace Studio.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class BlogController : Controller
    {
        //BlogService blogService = new BlogService();

        //
        // GET: /Admin/Blog/

        public ActionResult Index(int? page)
        {
            var blogs = new BlogHelp().GetBlogs().ToList();
            var pblogs = new Paginated<Blog>(blogs, page ?? 1, 25);

            return View(pblogs);
        }

        public ActionResult Add()
        {
            var blog = new Blog();
            blog.AuthorID = GlobalHelper.UserName();
            blog.DateCreated = DateTime.Now;

            IEnumerable<BlogTag> blogTags = null;

            var model = new BlogViewModel(blog, blogTags);

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Blog blog, List<BlogTag> blogTags)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["PictureFile"];

                if (string.IsNullOrEmpty(blog.MetaKeywords))
                {
                    foreach (var tag in blogTags)
                    {
                        if (!string.IsNullOrEmpty(tag.Tag))
                            blog.MetaKeywords += tag.Tag + ",";
                    }
                }

                new BlogHelp().InsertBlog(blog, file);


                new BlogHelp().SaveBlogTags(blog, blogTags);

                return RedirectToAction("Edit", new { id = blog.BlogID });
            }
            else
            {
                var model = new BlogViewModel(blog, blogTags);
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            var blog = new BlogHelp().GetBlog(id);
            var blogTags = new BlogHelp().GetBlogTags(id);

            var model = new BlogViewModel(blog, blogTags);

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Blog blog, List<BlogTag> blogTags)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["PictureFile"];

                if (string.IsNullOrEmpty(blog.MetaKeywords))
                {
                    foreach (var tag in blogTags)
                    {
                        if (!string.IsNullOrEmpty(tag.Tag))
                            blog.MetaKeywords += tag.Tag + ",";
                    }
                }

                new BlogHelp().UpdateBlog(blog, file);
                new BlogHelp().SaveBlogTags(blog, blogTags);

                return RedirectToAction("Index");
            }
            else
            {
                var model = new BlogViewModel(blog, blogTags);
                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {
            new BlogHelp().DeleteBlog(id);

            return RedirectToAction("Index");
        }

        //
        // Comments

        public ActionResult PendingComments()
        {
            var blogs = new BlogHelp().GetBlogsWithPendingComments().ToList();
            return View(blogs);
        }

        // TODO: is this being used?
        [HttpPost]
        public string AddComment(BlogComment comment)
        {
            if (ModelState.IsValid)
            {
                new BlogHelp().InsertBlogComment(comment);

                return "Thank you for your comment";
            }
            else
            {
                return "Error";
            }
        }

        public ActionResult ApproveComment(int id)
        {
            new BlogHelp().ApproveBlogComment(id);
            return RedirectToAction("PendingComments");
        }

        public ActionResult DeleteComment(int id)
        {
            new BlogHelp().DeleteBlogComment(id);

            return RedirectToAction("PendingComments");
        }

        //
        // GET: /Admin/Blog/Categories

        public ActionResult Categories()
        {
            var categories = new BlogHelp().GetBlogCategories().ToList();

            return View(categories);
        }

        [HttpPost]
        public ActionResult Categories(BlogCategory category)
        {
            if (ModelState.IsValid)
            {
                if (category.CategoryID > 0)
                {
                    new BlogHelp().UpdateBlogCategory(category);
                }
                else
                {
                    new BlogHelp().InsertBlogCategory(category);
                }

                return RedirectToAction("Categories");
            }
            else
            {
                return RedirectToAction("Categories");
            }
        }

        public ActionResult DeleteCategory(int id)
        {
            new BlogHelp().DeleteBlogCategory(id);
            //blogService.Save();

            return RedirectToAction("Categories");
        }

        public ActionResult UploadPicture(HttpPostedFileBase filedata)
        {
            xheditorModel model = new xheditorModel();

            try
            {
                if (filedata.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var file = Studio.Helpers.IO.UploadImageFile(filedata.InputStream, "/Content/Pictures/Blog", fileName, 800, 400, Studio.ImageHelper.ImageSaveType.Original);

                    model.msg = "/Content/Pictures/Blog" + file;
                }

                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                return this.Content(javaScriptSerializer.Serialize(model));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                filedata = null;
            }
        }
    }
}
