using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Blogs.Web.Migrations
{
    public partial class AddAreaID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "POS_MerchantArea",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_POS_MerchantArea_ID",
                table: "POS_MerchantArea",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_POS_MerchantArea_ID",
                table: "POS_MerchantArea");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "POS_MerchantArea");
        }
    }
}
