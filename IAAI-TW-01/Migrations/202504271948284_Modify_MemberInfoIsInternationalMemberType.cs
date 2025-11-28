namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_MemberInfoIsInternationalMemberType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MemberInfoes", "IsInternationalMember", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MemberInfoes", "IsInternationalMember", c => c.Boolean());
        }
    }
}
