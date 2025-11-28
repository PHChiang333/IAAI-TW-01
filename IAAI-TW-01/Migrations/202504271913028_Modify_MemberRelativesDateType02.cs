namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_MemberRelativesDateType02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberServices", "ServicedTimeStartYear", c => c.Int());
            AddColumn("dbo.MemberServices", "ServicedTimeStartMonth", c => c.Int());
            AddColumn("dbo.MemberServices", "ServicedTimeEndYear", c => c.Int());
            AddColumn("dbo.MemberServices", "ServicedTimeEndMonth", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MemberServices", "ServicedTimeEndMonth");
            DropColumn("dbo.MemberServices", "ServicedTimeEndYear");
            DropColumn("dbo.MemberServices", "ServicedTimeStartMonth");
            DropColumn("dbo.MemberServices", "ServicedTimeStartYear");
        }
    }
}
