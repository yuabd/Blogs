using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Blogs.Web.Migrations
{
    public partial class AddVisitCount_Pos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "VisitCount",
                table: "POS_PaymentLicense",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "VisitCount",
                table: "POS_MerchantArea",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "VisitCount",
                table: "POS_MCC",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisitCount",
                table: "POS_PaymentLicense");

            migrationBuilder.DropColumn(
                name: "VisitCount",
                table: "POS_MerchantArea");

            migrationBuilder.DropColumn(
                name: "VisitCount",
                table: "POS_MCC");
        }
    }
}
