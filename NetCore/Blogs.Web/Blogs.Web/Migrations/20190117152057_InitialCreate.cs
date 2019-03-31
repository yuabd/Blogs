using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Blogs.Web.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogCategory",
                columns: table => new
                {
                    CategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(maxLength: 25, nullable: false),
                    LanguageID = table.Column<string>(maxLength: 2, nullable: true),
                    MetaDescription = table.Column<string>(maxLength: 150, nullable: true),
                    MetaKeywords = table.Column<string>(maxLength: 100, nullable: true),
                    PageTitle = table.Column<string>(maxLength: 70, nullable: true),
                    Slug = table.Column<string>(maxLength: 56, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategory", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Link",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Contact = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    LinkUrl = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    PictureFile = table.Column<string>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Link", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastLogin = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    RoleID = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    BlogID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorID = table.Column<string>(maxLength: 15, nullable: false),
                    BlogContent = table.Column<string>(type: "ntext", nullable: true),
                    BlogTitle = table.Column<string>(maxLength: 70, nullable: false),
                    CategoryID = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    IsPublic = table.Column<bool>(nullable: false),
                    MetaDescription = table.Column<string>(maxLength: 150, nullable: true),
                    MetaKeywords = table.Column<string>(maxLength: 100, nullable: true),
                    PageTitle = table.Column<string>(maxLength: 70, nullable: true),
                    PageVisits = table.Column<int>(nullable: false),
                    PictureFile = table.Column<string>(maxLength: 56, nullable: true),
                    Slug = table.Column<string>(maxLength: 56, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.BlogID);
                    table.ForeignKey(
                        name: "FK_Blog_BlogCategory_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "BlogCategory",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 400, nullable: false),
                    EmergencyContact = table.Column<string>(maxLength: 15, nullable: false),
                    EmergencyPhone = table.Column<string>(maxLength: 15, nullable: false),
                    Gender = table.Column<string>(maxLength: 2, nullable: true),
                    IDCode = table.Column<string>(maxLength: 18, nullable: false),
                    Phone = table.Column<string>(maxLength: 15, nullable: false),
                    PictureFile = table.Column<string>(maxLength: 50, nullable: true),
                    Position = table.Column<string>(maxLength: 20, nullable: false),
                    School = table.Column<string>(maxLength: 100, nullable: false),
                    UserName = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_UserProfile_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleJoin",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false),
                    RoleID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleJoin", x => new { x.UserID, x.RoleID });
                    table.ForeignKey(
                        name: "FK_UserRoleJoin_UserRole_RoleID",
                        column: x => x.RoleID,
                        principalTable: "UserRole",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleJoin_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogComment",
                columns: table => new
                {
                    CommentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BlogID = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 60, nullable: false),
                    IsPublic = table.Column<bool>(nullable: false),
                    Message = table.Column<string>(maxLength: 1000, nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Website = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogComment", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_BlogComment_Blog_BlogID",
                        column: x => x.BlogID,
                        principalTable: "Blog",
                        principalColumn: "BlogID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogTag",
                columns: table => new
                {
                    BlogID = table.Column<int>(nullable: false),
                    Tag = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTag", x => new { x.BlogID, x.Tag });
                    table.ForeignKey(
                        name: "FK_BlogTag_Blog_BlogID",
                        column: x => x.BlogID,
                        principalTable: "Blog",
                        principalColumn: "BlogID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blog_CategoryID",
                table: "Blog",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComment_BlogID",
                table: "BlogComment",
                column: "BlogID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleJoin_RoleID",
                table: "UserRoleJoin",
                column: "RoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogComment");

            migrationBuilder.DropTable(
                name: "BlogTag");

            migrationBuilder.DropTable(
                name: "Link");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "UserRoleJoin");

            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "BlogCategory");
        }
    }
}
