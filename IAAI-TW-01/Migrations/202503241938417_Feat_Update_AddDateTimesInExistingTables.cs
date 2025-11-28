namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Feat_Update_AddDateTimesInExistingTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "CreateAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Contacts", "UpdateAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Contacts", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Contacts", "DeleteAt", c => c.DateTime());
            AddColumn("dbo.Knowlodges", "CreateAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Knowlodges", "UpdateAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Knowlodges", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Knowlodges", "DeleteAt", c => c.DateTime());
            AddColumn("dbo.News", "CreateAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.News", "UpdateAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.News", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.News", "DeleteAt", c => c.DateTime());
            AddColumn("dbo.NewsImgs", "CreateAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.NewsImgs", "UpdateAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.NewsImgs", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.NewsImgs", "DeleteAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NewsImgs", "DeleteAt");
            DropColumn("dbo.NewsImgs", "IsDeleted");
            DropColumn("dbo.NewsImgs", "UpdateAt");
            DropColumn("dbo.NewsImgs", "CreateAt");
            DropColumn("dbo.News", "DeleteAt");
            DropColumn("dbo.News", "IsDeleted");
            DropColumn("dbo.News", "UpdateAt");
            DropColumn("dbo.News", "CreateAt");
            DropColumn("dbo.Knowlodges", "DeleteAt");
            DropColumn("dbo.Knowlodges", "IsDeleted");
            DropColumn("dbo.Knowlodges", "UpdateAt");
            DropColumn("dbo.Knowlodges", "CreateAt");
            DropColumn("dbo.Contacts", "DeleteAt");
            DropColumn("dbo.Contacts", "IsDeleted");
            DropColumn("dbo.Contacts", "UpdateAt");
            DropColumn("dbo.Contacts", "CreateAt");
        }
    }
}
