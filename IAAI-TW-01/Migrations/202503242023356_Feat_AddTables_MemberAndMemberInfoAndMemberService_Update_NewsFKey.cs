namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Feat_AddTables_MemberAndMemberInfoAndMemberService_Update_NewsFKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.News", "NewsImgId", "dbo.NewsImgs");
            DropIndex("dbo.News", new[] { "NewsImgId" });
            CreateTable(
                "dbo.MemberInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Gender = c.Int(),
                        Birth = c.DateTime(),
                        MemberType = c.Int(),
                        ContactAddress = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        ServiceAt = c.String(maxLength: 50),
                        PositionName = c.String(maxLength: 50),
                        TopLevelEducation = c.String(maxLength: 50),
                        MemberId = c.Int(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleteAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 100),
                        PasswordSalt = c.String(maxLength: 100),
                        Permission = c.String(maxLength: 500),
                        CreateAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleteAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MemberServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceAt = c.String(nullable: false, maxLength: 50),
                        PositionName = c.String(nullable: false, maxLength: 50),
                        ServicedTimeStart = c.DateTime(),
                        ServicedTimeEnd = c.DateTime(),
                        ServicedTimePeriod = c.DateTime(),
                        MemberId = c.Int(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleteAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId);
            
            AddColumn("dbo.Experts", "ServiceAt", c => c.String());
            AddColumn("dbo.NewsImgs", "NewsId", c => c.Int(nullable: false));
            CreateIndex("dbo.NewsImgs", "NewsId");
            AddForeignKey("dbo.NewsImgs", "NewsId", "dbo.News", "Id", cascadeDelete: true);
            DropColumn("dbo.Experts", "ServeAt");
            DropColumn("dbo.News", "NewsImgId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.News", "NewsImgId", c => c.Int(nullable: false));
            AddColumn("dbo.Experts", "ServeAt", c => c.String());
            DropForeignKey("dbo.NewsImgs", "NewsId", "dbo.News");
            DropForeignKey("dbo.MemberServices", "MemberId", "dbo.Members");
            DropForeignKey("dbo.MemberInfoes", "MemberId", "dbo.Members");
            DropIndex("dbo.NewsImgs", new[] { "NewsId" });
            DropIndex("dbo.MemberServices", new[] { "MemberId" });
            DropIndex("dbo.MemberInfoes", new[] { "MemberId" });
            DropColumn("dbo.NewsImgs", "NewsId");
            DropColumn("dbo.Experts", "ServiceAt");
            DropTable("dbo.MemberServices");
            DropTable("dbo.Members");
            DropTable("dbo.MemberInfoes");
            CreateIndex("dbo.News", "NewsImgId");
            AddForeignKey("dbo.News", "NewsImgId", "dbo.NewsImgs", "Id", cascadeDelete: true);
        }
    }
}
