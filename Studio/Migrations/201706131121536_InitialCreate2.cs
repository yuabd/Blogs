namespace Studio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Case", "CategoryID", "dbo.CaseCategory");
            DropForeignKey("dbo.Customer", "UserID", "dbo.User");
            DropForeignKey("dbo.Order", "CompanyName", "dbo.Customer");
            DropForeignKey("dbo.ProductOrderJoin", "OrderID", "dbo.Order");
            DropForeignKey("dbo.ProductOrderJoin", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Order", "UserID", "dbo.User");
            DropForeignKey("dbo.WebsiteDetail", "WebsiteID", "dbo.Website");
            DropForeignKey("dbo.WebsiteUser", "WebsiteID", "dbo.Website");
            DropIndex("dbo.Case", new[] { "CategoryID" });
            DropIndex("dbo.Customer", new[] { "UserID" });
            DropIndex("dbo.Order", new[] { "CompanyName" });
            DropIndex("dbo.Order", new[] { "UserID" });
            DropIndex("dbo.ProductOrderJoin", new[] { "OrderID" });
            DropIndex("dbo.ProductOrderJoin", new[] { "ProductID" });
            DropIndex("dbo.WebsiteDetail", new[] { "WebsiteID" });
            DropIndex("dbo.WebsiteUser", new[] { "WebsiteID" });
            DropTable("dbo.CaseCategory");
            DropTable("dbo.Case");
            DropTable("dbo.Customer");
            DropTable("dbo.Industry");
            DropTable("dbo.Order");
            DropTable("dbo.ProductOrderJoin");
            DropTable("dbo.Product");
            DropTable("dbo.PhotoDetail");
            DropTable("dbo.Photo");
            DropTable("dbo.PhotoVote");
            DropTable("dbo.Picture");
            DropTable("dbo.Task");
            DropTable("dbo.WebsiteDetail");
            DropTable("dbo.Website");
            DropTable("dbo.WebsiteUser");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.WebsiteUser",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 20),
                        WebsiteID = c.Int(nullable: false),
                        Password = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.UserName);
            
            CreateTable(
                "dbo.Website",
                c => new
                    {
                        WebsiteID = c.Int(nullable: false, identity: true),
                        IndustryID = c.Int(nullable: false),
                        WebsiteName = c.String(nullable: false, maxLength: 20),
                        Type = c.String(nullable: false, maxLength: 20),
                        WebsiteUrl = c.String(nullable: false, maxLength: 300),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.WebsiteID);
            
            CreateTable(
                "dbo.WebsiteDetail",
                c => new
                    {
                        LinkID = c.Int(nullable: false, identity: true),
                        Link = c.String(maxLength: 300),
                        WebsiteID = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LinkID);
            
            CreateTable(
                "dbo.Task",
                c => new
                    {
                        TaskID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Status = c.String(nullable: false, maxLength: 20),
                        Type = c.String(nullable: false, maxLength: 20),
                        ID = c.String(maxLength: 30),
                        Description = c.String(nullable: false, storeType: "ntext"),
                        DateCreated = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(nullable: false),
                        SortOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskID);
            
            CreateTable(
                "dbo.Picture",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ParentID = c.Int(nullable: false),
                        PictureFile = c.String(),
                        Type = c.Int(nullable: false),
                        IsDefault = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PhotoVote",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PhotoID = c.Int(nullable: false),
                        IP = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Photo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PhotoName = c.String(),
                        Description = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        Author = c.String(),
                        Guid = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PhotoDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PhotoID = c.Int(nullable: false),
                        PictureUrl = c.String(),
                        IsDefault = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 40),
                        Description = c.String(maxLength: 500),
                        UnitPrice = c.Int(nullable: false),
                        SortOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.ProductOrderJoin",
                c => new
                    {
                        OrderID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderID, t.ProductID });
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(maxLength: 40),
                        UserID = c.Int(nullable: false),
                        Description = c.String(storeType: "ntext"),
                        Status = c.String(nullable: false, maxLength: 20),
                        NeedInvoice = c.Boolean(nullable: false),
                        TotalAmount = c.Single(nullable: false),
                        DiscountAmount = c.Single(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateStart = c.DateTime(nullable: false),
                        DateFinish = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID);
            
            CreateTable(
                "dbo.Industry",
                c => new
                    {
                        IndustryID = c.Int(nullable: false, identity: true),
                        IndustryName = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.IndustryID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CompanyName = c.String(nullable: false, maxLength: 40),
                        UserID = c.Int(nullable: false),
                        IndustryID = c.Int(nullable: false),
                        LeadSource = c.String(nullable: false, maxLength: 30),
                        Type = c.String(nullable: false, maxLength: 10),
                        ContactName = c.String(nullable: false, maxLength: 30),
                        Gender = c.String(nullable: false, maxLength: 6),
                        Phone = c.String(nullable: false, maxLength: 15),
                        Address = c.String(nullable: false, maxLength: 100),
                        Email = c.String(maxLength: 50),
                        Status = c.String(nullable: false, maxLength: 20),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyName);
            
            CreateTable(
                "dbo.Case",
                c => new
                    {
                        CaseID = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        ShortDescription = c.String(maxLength: 300),
                        Description = c.String(maxLength: 600),
                        Website = c.String(nullable: false, maxLength: 50),
                        DateCreated = c.DateTime(nullable: false),
                        PictureFile = c.String(maxLength: 60),
                    })
                .PrimaryKey(t => t.CaseID);
            
            CreateTable(
                "dbo.CaseCategory",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 20),
                        CategoryDescription = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateIndex("dbo.WebsiteUser", "WebsiteID");
            CreateIndex("dbo.WebsiteDetail", "WebsiteID");
            CreateIndex("dbo.ProductOrderJoin", "ProductID");
            CreateIndex("dbo.ProductOrderJoin", "OrderID");
            CreateIndex("dbo.Order", "UserID");
            CreateIndex("dbo.Order", "CompanyName");
            CreateIndex("dbo.Customer", "UserID");
            CreateIndex("dbo.Case", "CategoryID");
            AddForeignKey("dbo.WebsiteUser", "WebsiteID", "dbo.Website", "WebsiteID", cascadeDelete: true);
            AddForeignKey("dbo.WebsiteDetail", "WebsiteID", "dbo.Website", "WebsiteID", cascadeDelete: true);
            AddForeignKey("dbo.Order", "UserID", "dbo.User", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.ProductOrderJoin", "ProductID", "dbo.Product", "ProductID", cascadeDelete: true);
            AddForeignKey("dbo.ProductOrderJoin", "OrderID", "dbo.Order", "OrderID", cascadeDelete: true);
            AddForeignKey("dbo.Order", "CompanyName", "dbo.Customer", "CompanyName");
            AddForeignKey("dbo.Customer", "UserID", "dbo.User", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.Case", "CategoryID", "dbo.CaseCategory", "CategoryID", cascadeDelete: true);
        }
    }
}
