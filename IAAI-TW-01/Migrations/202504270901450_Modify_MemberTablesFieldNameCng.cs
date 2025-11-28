namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_MemberTablesFieldNameCng : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberInfoes", "IsInternationalMember", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MemberInfoes", "IsInternationalMember");
        }
    }
}
