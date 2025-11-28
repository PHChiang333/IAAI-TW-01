namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_MemberRelativesRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MemberInfoes", "Gender", c => c.String(nullable: false));
            AlterColumn("dbo.MemberInfoes", "Birth", c => c.DateTime(nullable: false));
            AlterColumn("dbo.MemberInfoes", "MemberType", c => c.String(nullable: false));
            AlterColumn("dbo.MemberInfoes", "ContactAddress", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.MemberInfoes", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.MemberInfoes", "ServiceAt", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.MemberInfoes", "PositionName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.MemberInfoes", "TopLevelEducation", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MemberInfoes", "TopLevelEducation", c => c.String(maxLength: 50));
            AlterColumn("dbo.MemberInfoes", "PositionName", c => c.String(maxLength: 50));
            AlterColumn("dbo.MemberInfoes", "ServiceAt", c => c.String(maxLength: 50));
            AlterColumn("dbo.MemberInfoes", "Email", c => c.String(maxLength: 50));
            AlterColumn("dbo.MemberInfoes", "ContactAddress", c => c.String(maxLength: 50));
            AlterColumn("dbo.MemberInfoes", "MemberType", c => c.String());
            AlterColumn("dbo.MemberInfoes", "Birth", c => c.DateTime());
            AlterColumn("dbo.MemberInfoes", "Gender", c => c.Int());
        }
    }
}
