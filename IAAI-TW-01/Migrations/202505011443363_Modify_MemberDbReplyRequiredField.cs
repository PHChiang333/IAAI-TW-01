namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_MemberDbReplyRequiredField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MemberDbReplies", "Title", c => c.String(maxLength: 50));
            AlterColumn("dbo.MemberDbReplies", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MemberDbReplies", "Content", c => c.String());
            AlterColumn("dbo.MemberDbReplies", "Title", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
