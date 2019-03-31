﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Blogs.Model.DbModels
{
	public class SiteDataContext : DbContext
	{
		public SiteDataContext(DbContextOptions<SiteDataContext> options)
			: base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{

			base.OnConfiguring(optionsBuilder);
		}

		public DbSet<Blog> Blog { get; set; }
		public DbSet<BlogComment> BlogComment { get; set; }
		public DbSet<BlogCategory> BlogCategory { get; set; }
		public DbSet<BlogTag> BlogTag { get; set; }
		public DbSet<User> User { get; set; }
		public DbSet<UserRole> UserRole { get; set; }
		public DbSet<UserRoleJoin> UserRoleJoin { get; set; }
		public DbSet<UserProfile> UserProfile { get; set; }

		//public DbSet<Magnet> Magnets { get; set; }
		//public DbSet<MagnetFile> MagnetFiles { get; set; }

		public DbSet<Links> Link { get; set; }

		// Twist our database
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			modelBuilder.Entity<BlogTag>().HasKey(m => new { m.BlogID, m.Tag });
			modelBuilder.Entity<UserRoleJoin>().HasKey(m => new { m.UserID, m.RoleID });

			base.OnModelCreating(modelBuilder);
		}
	}

	public static class SiteDataContextInitializer
	{
		public static void Seed(IServiceProvider scopedProvider)
		{
			// users
			//var user = new User { UserID = 10000, Password = "DAC177D41FD48F8627BD10D2689E8544", DateCreated = DateTime.Now, DateLastLogin = DateTime.Now };

			//         context.Users.Add(user);

			//         var roles = new List<UserRole>
			//         {
			//             new UserRole { RoleID = "Administrator" },
			//             new UserRole { RoleID = "Editor" },
			//             new UserRole { RoleID = "Customer" },
			//             new UserRole { RoleID = "Guest" }
			//         };

			//         roles.ForEach(m => context.UserRoles.Add(m));

			//         var userRoleJoin = new UserRoleJoin { UserID = 10000, RoleID = "Administrator" };
			//         context.UserRoleJoins.Add(userRoleJoin);

			//         var categories = new List<BlogCategory>{
			//             new BlogCategory { CategoryName ="技术讨论", LanguageID="zh",

			//                 Blogs = new List<Blog>{
			//                     new Blog{ AuthorID = "admin", BlogContent ="<p>先声明一个类：<pre class='line mt-10 q-content' accuse='qContent' style='margin-top: 10px; margin-bottom: 0px; padding: 0px; font-family: arial, 'courier new', courier, 宋体, monospace; white-space: pre-wrap; word-wrap: break-word; word-break: break-all; color: rgb(51, 51, 51); font-size: 14px; line-height: 24px;'>public class BlogTag</pre><pre class='line mt-10 q-content' accuse='qContent' style='margin-top: 10px; margin-bottom: 0px; padding: 0px; font-family: arial, 'courier new', courier, 宋体, monospace; white-space: pre-wrap; word-wrap: break-word; word-break: break-all; color: rgb(51, 51, 51); font-size: 14px; line-height: 24px;'>{<span style='font-family: Arial, Verdana, sans-serif; font-size: 12px; white-space: pre;'>		</span><span style='font-family: Arial, Verdana, sans-serif; font-size: 12px;'>public int BlogID { get; set; }</span></pre></p><p>	<span style='white-space:pre'>		</span>public string Tag { get; set; }<br />	</p><p>	}</p><p>	以下是方法：</p><p>	&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;class MyComparer : IEqualityComparer&lt;BlogTag&gt;</p><span style='white-space:pre'>		</span>{<br /><span style='white-space:pre'>			</span>public bool Equals(BlogTag x, BlogTag y)<br /><span style='white-space:pre'>			</span>{<br /><span style='white-space:pre'>				</span>if (x == null &amp;&amp; y == null)<br /><span style='white-space:pre'>				</span>{<br /><span style='white-space:pre'>					</span>return false;<br /><span style='white-space:pre'>				</span>}<br /><span style='white-space:pre'>				</span>else<br /><span style='white-space:pre'>				</span>{<br /><span style='white-space:pre'>					</span>return x.Tag == y.Tag;<br /><span style='white-space:pre'>				</span>}<br /><span style='white-space:pre'>			</span>}<br /><br /><br /><span style='white-space:pre'>			</span>public int GetHashCode(BlogTag obj)<br /><span style='white-space:pre'>			</span>{<br /><span style='white-space:pre'>				</span>return obj.Tag.GetHashCode();<br /><span style='white-space:pre'>			</span>}<br /><span style='white-space:pre'>		</span>}<br /><br />效果,取出所有的BlogTag，去除掉其中Tag字段相同的记录，并放回IEnumerable<br /><span style='white-space:pre'>		</span>public IEnumerable&lt;BlogTag&gt; GetTags()<br /><span style='white-space:pre'>		</span>{<br /><p>	<span style='white-space:pre'>			</span>var tags = (from l in db.BlogTags<span style='white-space:pre'>						</span></p><p>	&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; select l).ToList().Distinct(new MyComparer());</p><span style='white-space:pre'>			</span>return tags;<br /><p>	<span style='white-space:pre'>		</span>}</p><p>	<br />	</p>"
			//                 ,
			//                         BlogTitle = "Linq 如何去除重复的记录，并返回IEnumerable", DateCreated = DateTime.Now, IsPublic = true, PageTitle = "Linq 如何去除重复的记录，并返回IEnumerable", Slug = "Linq-如何去除重复的记录-并返回IEnumerable", PageVisits = 0}
			//                         }, SortOrder = 0, Slug = "技术讨论"
			//             }
			//         };

			//         categories.ForEach(m => context.BlogCategories.Add(m));

			//         context.SaveChanges();
		}
	}
}