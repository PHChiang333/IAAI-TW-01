namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_MemberRelativesRequired02 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MemberServices", "ServiceAt", c => c.String(maxLength: 50));
            AlterColumn("dbo.MemberServices", "PositionName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MemberServices", "PositionName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.MemberServices", "ServiceAt", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
