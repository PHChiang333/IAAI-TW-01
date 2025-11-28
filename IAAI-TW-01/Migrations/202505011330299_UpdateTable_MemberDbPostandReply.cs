namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTable_MemberDbPostandReply : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberDbPosts", "AuthorId", c => c.Int(nullable: false));
            AddColumn("dbo.MemberDbReplies", "AuthorId", c => c.Int(nullable: false));
            AddColumn("dbo.MemberDbReplies", "Author", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.MemberDbReplies", "CoverName");
            DropColumn("dbo.MemberDbReplies", "CoverPath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MemberDbReplies", "CoverPath", c => c.String());
            AddColumn("dbo.MemberDbReplies", "CoverName", c => c.String(maxLength: 50));
            DropColumn("dbo.MemberDbReplies", "Author");
            DropColumn("dbo.MemberDbReplies", "AuthorId");
            DropColumn("dbo.MemberDbPosts", "AuthorId");
        }
    }
}
