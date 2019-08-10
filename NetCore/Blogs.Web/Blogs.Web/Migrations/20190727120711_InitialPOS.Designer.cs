﻿// <auto-generated />
using Blogs.Model.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Blogs.Web.Migrations
{
    [DbContext(typeof(SiteDataContext))]
    [Migration("20190727120711_InitialPOS")]
    partial class InitialPOS
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Blogs.Model.DbModels.Blog", b =>
                {
                    b.Property<int>("BlogID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorID")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("BlogContent")
                        .HasColumnType("ntext");

                    b.Property<string>("BlogTitle")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<int>("CategoryID");

                    b.Property<DateTime>("DateCreated");

                    b.Property<bool>("IsPublic");

                    b.Property<string>("MetaDescription")
                        .HasMaxLength(150);

                    b.Property<string>("MetaKeywords")
                        .HasMaxLength(100);

                    b.Property<string>("PageTitle")
                        .HasMaxLength(70);

                    b.Property<int>("PageVisits");

                    b.Property<string>("PictureFile")
                        .HasMaxLength(56);

                    b.Property<string>("Slug")
                        .HasMaxLength(56);

                    b.HasKey("BlogID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Blog");
                });

            modelBuilder.Entity("Blogs.Model.DbModels.BlogCategory", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("LanguageID")
                        .HasMaxLength(2);

                    b.Property<string>("MetaDescription")
                        .HasMaxLength(150);

                    b.Property<string>("MetaKeywords")
                        .HasMaxLength(100);

                    b.Property<string>("PageTitle")
                        .HasMaxLength(70);

                    b.Property<string>("Slug")
                        .HasMaxLength(56);

                    b.Property<int>("SortOrder");

                    b.HasKey("CategoryID");

                    b.ToTable("BlogCategory");
                });

            modelBuilder.Entity("Blogs.Model.DbModels.BlogComment", b =>
                {
                    b.Property<int>("CommentID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BlogID");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<bool>("IsPublic");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Website")
                        .HasMaxLength(100);

                    b.HasKey("CommentID");

                    b.HasIndex("BlogID");

                    b.ToTable("BlogComment");
                });

            modelBuilder.Entity("Blogs.Model.DbModels.BlogTag", b =>
                {
                    b.Property<int>("BlogID");

                    b.Property<string>("Tag")
                        .HasMaxLength(20);

                    b.HasKey("BlogID", "Tag");

                    b.ToTable("BlogTag");
                });

            modelBuilder.Entity("Blogs.Model.DbModels.Links", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Contact");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<string>("Description");

                    b.Property<string>("Email");

                    b.Property<string>("LinkUrl")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("PictureFile");

                    b.Property<int?>("SortOrder")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Link");
                });

            modelBuilder.Entity("Blogs.Model.DbModels.POS_Agency", b =>
                {
                    b.Property<int>("AgencyID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AgencyCode")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("AgencyName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("AgencyID");

                    b.ToTable("POS_Agency");
                });

            modelBuilder.Entity("Blogs.Model.DbModels.POS_Bank", b =>
                {
                    b.Property<int>("BankID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("BankID");

                    b.ToTable("POS_Bank");
                });

            modelBuilder.Entity("Blogs.Model.DbModels.POS_MCC", b =>
                {
                    b.Property<string>("MCC")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<bool>("IsPoint");

                    b.Property<string>("NoPointBankStr")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<decimal>("OldRate");

                    b.Property<decimal>("Rate");

                    b.HasKey("MCC");

                    b.ToTable("POS_MCC");
                });

            modelBuilder.Entity("Blogs.Model.DbModels.POS_MCCBankNoPoint", b =>
                {
                    b.Property<int>("BankID");

                    b.Property<string>("MCC")
                        .HasMaxLength(4);

                    b.HasKey("BankID", "MCC");

                    b.HasIndex("MCC");

                    b.ToTable("POS_MCCBankNoPoint");
                });

            modelBuilder.Entity("Blogs.Model.DbModels.POS_MerchantArea", b =>
                {
                    b.Property<string>("MerchantAreaCode")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10);

                    b.Property<string>("MerchantAreaName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("MerchantProvince")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("MerchantAreaCode");

                    b.ToTable("POS_MerchantArea");
                });

            modelBuilder.Entity("Blogs.Model.DbModels.POS_PaymentLicense", b =>
                {
                    b.Property<string>("LicenseID")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100);

                    b.Property<string>("BusType")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("Date");

                    b.Property<string>("Scope")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("LicenseID");

                    b.ToTable("POS_PaymentLicense");
                });

            modelBuilder.Entity("Blogs.Model.DbModels.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateLastLogin");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("UserID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Blogs.Model.DbModels.UserProfile", b =>
                {
                    b.Property<int>("UserID");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(400);

                    b.Property<string>("EmergencyContact")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("EmergencyPhone")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("Gender")
                        .HasMaxLength(2);

                    b.Property<string>("IDCode")
                        .IsRequired()
                        .HasMaxLength(18);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("PictureFile")
                        .HasMaxLength(50);

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("School")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("UserID");

                    b.ToTable("UserProfile");
                });

            modelBuilder.Entity("Blogs.Model.DbModels.UserRole", b =>
                {
                    b.Property<string>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(15);

                    b.HasKey("RoleID");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Blogs.Model.DbModels.UserRoleJoin", b =>
                {
                    b.Property<int>("UserID");

                    b.Property<string>("RoleID");

                    b.HasKey("UserID", "RoleID");

                    b.HasIndex("RoleID");

                    b.ToTable("UserRoleJoin");
                });

            modelBuilder.Entity("Blogs.Model.DbModels.Blog", b =>
                {
                    b.HasOne("Blogs.Model.DbModels.BlogCategory", "BlogCategory")
                        .WithMany("Blogs")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Blogs.Model.DbModels.BlogComment", b =>
                {
                    b.HasOne("Blogs.Model.DbModels.Blog", "Blog")
                        .WithMany("BlogComments")
                        .HasForeignKey("BlogID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Blogs.Model.DbModels.BlogTag", b =>
                {
                    b.HasOne("Blogs.Model.DbModels.Blog", "Blog")
                        .WithMany("BlogTags")
                        .HasForeignKey("BlogID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Blogs.Model.DbModels.POS_MCCBankNoPoint", b =>
                {
                    b.HasOne("Blogs.Model.DbModels.POS_Bank", "POS_Bank")
                        .WithMany()
                        .HasForeignKey("BankID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Blogs.Model.DbModels.POS_MCC", "POS_MCC")
                        .WithMany()
                        .HasForeignKey("MCC")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Blogs.Model.DbModels.UserProfile", b =>
                {
                    b.HasOne("Blogs.Model.DbModels.User", "User")
                        .WithOne("UserProfile")
                        .HasForeignKey("Blogs.Model.DbModels.UserProfile", "UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Blogs.Model.DbModels.UserRoleJoin", b =>
                {
                    b.HasOne("Blogs.Model.DbModels.UserRole", "UserRole")
                        .WithMany("UserRoleJoins")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Blogs.Model.DbModels.User", "User")
                        .WithMany("UserRoleJoins")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
