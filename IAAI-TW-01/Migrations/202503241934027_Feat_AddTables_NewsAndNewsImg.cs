namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Feat_AddTables_NewsAndNewsImg : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Content = c.String(),
                        CoverName = c.String(maxLength: 50),
                        CoverPath = c.String(),
                        NewsImgId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NewsImgs", t => t.NewsImgId, cascadeDelete: true)
                .Index(t => t.NewsImgId);
            
            CreateTable(
                "dbo.NewsImgs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ImgPath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "NewsImgId", "dbo.NewsImgs");
            DropIndex("dbo.News", new[] { "NewsImgId" });
            DropTable("dbo.NewsImgs");
            DropTable("dbo.News");
        }
    }
}
