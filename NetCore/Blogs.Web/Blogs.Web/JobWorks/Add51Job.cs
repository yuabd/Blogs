using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogs.Model.DbModels;
using HtmlAgilityPack;
using Quartz;

namespace Blogs.Web.JobWorks
{
	[PersistJobDataAfterExecution]
	public class Add51Job : IJob
	{
		private SiteDataContext db;
		public Add51Job(SiteDataContext _context)
		{
			try
			{
				db = _context;
			}
			catch (System.Exception ex)
			{
				using (StreamWriter sw = new StreamWriter(@"C:\Users\yuabd\Desktop\1.txt", true, Encoding.UTF8))
				{
					sw.WriteAsync(ex.Message);
				}
			}
		}

		public Task Execute(IJobExecutionContext context)
		{
			var msg = "";
			try
			{
				var jobData = context.JobDetail.JobDataMap;
				//var triggerData = context.Trigger.JobDataMap;
				var data = context.MergedJobDataMap;

				//var value1 = jobData.GetInt("key1");
				//var value2 = jobData.GetString("key2");
				var value = data.GetInt("key1");

				var dateString = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
				//Random random = new Random();

				jobData["key1"] = value + 1;//这里面给key赋值，下次再进来执行的时候，获取的值为更新的值，而不是原始值
											//jobData["key2"] = dateString;

				using (StreamWriter sw = new StreamWriter(@"C:\Users\yuabd\Desktop\1.txt", true, Encoding.UTF8))
				{
					sw.WriteLine($"{dateString} value1:{value}");
				}

				var url = "https://www.u51.com/zhishi/credit/p" + value;
				HtmlWeb web = new HtmlWeb();
				//1.支持从web或本地path加载html
				var htmlDoc = web.Load(url);
				var post_listnode = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='article-list w660']/div");
				//Console.WriteLine("Node Name: " + post_listnode.Name + "\n" + post_listnode.OuterHtml);
				var postitemsNodes = post_listnode.SelectNodes("./section[@class='article-card']");
				var articles = new List<Blog>();
				//var digitRegex = @"[^0-9]+";
				foreach (var item in postitemsNodes)
				{
					var article = new Blog();
					var post_item_bodynode = item.SelectSingleNode("div[@class='content']");
					//var diggnumnode = post_item_bodynode.SelectSingleNode("*//span[@class='num']");
					//body
					var titlenode = post_item_bodynode.SelectSingleNode("./a");
					var summarynode = post_item_bodynode.SelectSingleNode("p[@class='desc']");
					//foot
					var footnode = post_item_bodynode.SelectSingleNode("div[@class='bottom']");
					var authornode = footnode.ChildNodes[0];
					//var commentnode = footnode.SelectSingleNode("span[@class='article_comment']");
					var viewnode = footnode.SelectSingleNode("label[@class='num']");
					var pic = item.SelectSingleNode("a/img");

					article.CategoryID = 1;
					article.AuthorID = "admin";
					article.BlogContent = summarynode.OuterHtml;
					article.BlogTitle = titlenode.OuterHtml;
					article.DateCreated = DateTime.Now;
					article.IsPublic = true;
					article.MetaDescription = summarynode.OuterHtml;
					article.MetaKeywords = titlenode.OuterHtml;
					article.PageTitle = titlenode.OuterHtml;
					article.PageVisits = 0;
					article.PictureFile = pic.Attributes["src"].Value;
					//article.PictureThumbnail = article.PictureFile;
					article.Slug = article.BlogID.ToString();

					articles.Add(article);
				}

				//return Task.Run(() =>
				//{
				//	db.AddAsync(articles);
				//});
			}
			catch (System.Exception ex)
			{
				msg = ex.Message;
			}

			return Task.Run(() =>
			{
				using (StreamWriter sw = new StreamWriter(@"C:\Users\yuabd\Desktop\1.txt", true, Encoding.UTF8))
				{
					sw.WriteAsync(msg);
				}
			});
		}
	}
}
