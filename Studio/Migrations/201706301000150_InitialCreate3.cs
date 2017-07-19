namespace Studio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MagnetFile",
                c => new
                    {
                        Hash = c.String(nullable: false, maxLength: 128),
                        MagnetHash = c.String(maxLength: 128),
                        Name = c.String(),
                        Size = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Hash)
                .ForeignKey("dbo.Magnet", t => t.MagnetHash)
                .Index(t => t.MagnetHash);
            
            CreateTable(
                "dbo.Magnet",
                c => new
                    {
                        Hash = c.String(nullable: false, maxLength: 128),
                        FilePath = c.String(),
                        Count = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        FileSize = c.Long(nullable: false),
                        Keywords = c.String(),
                        TotalFiles = c.Int(nullable: false),
                        MagnetLink = c.String(),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.Hash);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MagnetFile", "MagnetHash", "dbo.Magnet");
            DropIndex("dbo.MagnetFile", new[] { "MagnetHash" });
            DropTable("dbo.Magnet");
            DropTable("dbo.MagnetFile");
        }
    }
}
