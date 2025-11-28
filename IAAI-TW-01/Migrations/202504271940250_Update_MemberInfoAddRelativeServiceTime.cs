namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_MemberInfoAddRelativeServiceTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberInfoes", "RelativeServicedTimeEnd", c => c.DateTime());
            AddColumn("dbo.MemberInfoes", "RelativeServicedTimeEndYear", c => c.Int());
            AddColumn("dbo.MemberInfoes", "RelativeServicedTimeEndMonth", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MemberInfoes", "RelativeServicedTimeEndMonth");
            DropColumn("dbo.MemberInfoes", "RelativeServicedTimeEndYear");
            DropColumn("dbo.MemberInfoes", "RelativeServicedTimeEnd");
        }
    }
}
