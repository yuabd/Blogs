namespace Studio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.BlogComment", "BlogID");
            AddForeignKey("dbo.BlogComment", "BlogID", "dbo.Blog", "BlogID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogComment", "BlogID", "dbo.Blog");
            DropIndex("dbo.BlogComment", new[] { "BlogID" });
        }
    }
}
