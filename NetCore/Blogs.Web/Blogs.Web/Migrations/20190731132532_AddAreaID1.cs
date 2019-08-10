using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Blogs.Web.Migrations
{
    public partial class AddAreaID1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_POS_MerchantArea_ID",
                table: "POS_MerchantArea");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_POS_MerchantArea_ID",
                table: "POS_MerchantArea",
                column: "ID");
        }
    }
}
