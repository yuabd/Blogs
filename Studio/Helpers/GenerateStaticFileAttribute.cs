using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using System.Text;

namespace Studio.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class GenerateStaticFileAttribute : ActionFilterAttribute
    {
        #region 私有属性

        //private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region 公共属性

        /// <summary>
        /// 过期时间，以小时为单位
        /// </summary>
        public int Expiration { get; set; }

        /// <summary>
        /// 缓存目录
        /// </summary>
        public string CacheDirectory { get; set; }

        /// <summary>
        /// 指定生成的文件名
        /// </summary>
        public string FileName { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public GenerateStaticFileAttribute()
        {
            Expiration = 2;

            CacheDirectory = HttpContext.Current.Server.MapPath("/Html");
        }

        #endregion

        #region 方法

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var fileInfo = GetCacheFileInfo(filterContext);
            //try
            //{
            //    var finalurl = new GlobalHelper().GetUrl(filterContext.HttpContext.Request.RawUrl);

            //    if (fileInfo.Exists && fileInfo.LastWriteTime.AddHours(Expiration) >= DateTime.Now)
            //    {
            //        filterContext.HttpContext.RewritePath(finalurl);
            //    }
            //}
            //catch (Exception e)
            //{
            //}

            if ((fileInfo.Exists && fileInfo.LastWriteTime.AddHours(Expiration) < DateTime.Now) || !fileInfo.Exists)
            {
                if (filterContext.HttpContext.Response.StatusCode == 404)
                {
                    return;
                }
                var deleted = false;

                try
                {
                    if (fileInfo.Exists)
                    {
                        fileInfo.Delete();
                    }

                    deleted = true;
                }
                catch (Exception ex)
                {
                    //logger.Error("删除文件:{0}发生异常:{1}", fileInfo.FullName, ex.StackTrace);
                }

                var created = false;

                try
                {
                    if (!fileInfo.Directory.Exists)
                    {
                        fileInfo.Directory.Create();
                    }

                    created = true;
                }
                catch (IOException ex)
                {
                    //logger.Error("创建目录:{0}发生异常:{1}", fileInfo.DirectoryName, ex.StackTrace);
                }

                if (deleted && created)
                {
                    FileStream fileStream = null;
                    StreamWriter streamWriter = null;

                    try
                    {
                        var viewResult = filterContext.Result as ViewResult;
                        fileStream = new FileStream(fileInfo.FullName, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                        streamWriter = new StreamWriter(fileStream);
                        var viewContext = new ViewContext(filterContext.Controller.ControllerContext, viewResult.View, viewResult.ViewData, viewResult.TempData, streamWriter);
                        viewResult.View.Render(viewContext, streamWriter);
                    }
                    catch (Exception ex)
                    {
                        //logger.Error("生成缓存文件:{0}发生异常:{1}", fileInfo.FullName, ex.StackTrace);
                    }
                    finally
                    {
                        if (streamWriter != null)
                        {
                            streamWriter.Close();
                        }

                        if (fileStream != null)
                        {
                            fileStream.Close();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 生成文件Key
        /// </summary>
        /// <param name="controllerContext">ControllerContext</param>
        /// <returns>文件Key</returns>
        protected virtual string GenerateKey(ControllerContext controllerContext)
        {
            var url = controllerContext.HttpContext.Request.Url.ToString();

            if (string.IsNullOrWhiteSpace(url))
            {
                return null;
            }

            var data = Encoding.Unicode.GetBytes(url);
            var key = Convert.ToBase64String(data, Base64FormattingOptions.None);
            key = HttpUtility.UrlEncode(key);

            return key;
        }

        /// <summary>
        /// 获取静态的文件信息
        /// </summary>
        /// <param name="controllerContext">ControllerContext</param>
        /// <returns>缓存文件信息</returns>
        protected virtual FileInfo GetCacheFileInfo(ControllerContext controllerContext)
        {
            var fileName = string.Empty;

            if (string.IsNullOrWhiteSpace(FileName))
            {
                var key = new GlobalHelper().GetUrl(controllerContext.HttpContext.Request.RawUrl);

                fileName = Path.Combine(CacheDirectory, key);
            }
            else
            {
                fileName = Path.Combine(CacheDirectory, FileName);
            }

            return new FileInfo(HttpContext.Current.Server.MapPath(fileName));
        }

        #endregion
    }
}