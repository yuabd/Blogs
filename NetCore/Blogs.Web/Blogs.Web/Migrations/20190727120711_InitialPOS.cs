using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Blogs.Web.Migrations
{
    public partial class InitialPOS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "POS_Agency",
                columns: table => new
                {
                    AgencyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AgencyCode = table.Column<string>(maxLength: 150, nullable: false),
                    AgencyName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POS_Agency", x => x.AgencyID);
                });

            migrationBuilder.CreateTable(
                name: "POS_Bank",
                columns: table => new
                {
                    BankID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BankName = table.Column<string>(maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 15, nullable: false),
                    ShortName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POS_Bank", x => x.BankID);
                });

            migrationBuilder.CreateTable(
                name: "POS_MCC",
                columns: table => new
                {
                    MCC = table.Column<string>(maxLength: 4, nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    IsPoint = table.Column<bool>(nullable: false),
                    NoPointBankStr = table.Column<string>(maxLength: 300, nullable: false),
                    OldRate = table.Column<decimal>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POS_MCC", x => x.MCC);
                });

            migrationBuilder.CreateTable(
                name: "POS_MerchantArea",
                columns: table => new
                {
                    MerchantAreaCode = table.Column<string>(maxLength: 10, nullable: false),
                    MerchantAreaName = table.Column<string>(maxLength: 100, nullable: false),
                    MerchantProvince = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POS_MerchantArea", x => x.MerchantAreaCode);
                });

            migrationBuilder.CreateTable(
                name: "POS_PaymentLicense",
                columns: table => new
                {
                    LicenseID = table.Column<string>(maxLength: 100, nullable: false),
                    BusType = table.Column<string>(maxLength: 150, nullable: false),
                    CompanyName = table.Column<string>(maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Scope = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POS_PaymentLicense", x => x.LicenseID);
                });

            migrationBuilder.CreateTable(
                name: "POS_MCCBankNoPoint",
                columns: table => new
                {
                    BankID = table.Column<int>(nullable: false),
                    MCC = table.Column<string>(maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POS_MCCBankNoPoint", x => new { x.BankID, x.MCC });
                    table.ForeignKey(
                        name: "FK_POS_MCCBankNoPoint_POS_Bank_BankID",
                        column: x => x.BankID,
                        principalTable: "POS_Bank",
                        principalColumn: "BankID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POS_MCCBankNoPoint_POS_MCC_MCC",
                        column: x => x.MCC,
                        principalTable: "POS_MCC",
                        principalColumn: "MCC",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_POS_MCCBankNoPoint_MCC",
                table: "POS_MCCBankNoPoint",
                column: "MCC");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POS_Agency");

            migrationBuilder.DropTable(
                name: "POS_MCCBankNoPoint");

            migrationBuilder.DropTable(
                name: "POS_MerchantArea");

            migrationBuilder.DropTable(
                name: "POS_PaymentLicense");

            migrationBuilder.DropTable(
                name: "POS_Bank");

            migrationBuilder.DropTable(
                name: "POS_MCC");
        }
    }
}
