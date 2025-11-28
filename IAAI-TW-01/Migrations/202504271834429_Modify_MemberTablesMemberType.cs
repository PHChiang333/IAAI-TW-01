namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_MemberTablesMemberType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MemberInfoes", "MemberType", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MemberInfoes", "MemberType", c => c.Int());
        }
    }
}
