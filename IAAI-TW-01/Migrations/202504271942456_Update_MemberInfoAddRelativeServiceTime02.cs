namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_MemberInfoAddRelativeServiceTime02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberInfoes", "RelativeServicedTime", c => c.DateTime());
            AddColumn("dbo.MemberInfoes", "RelativeServicedTimeYear", c => c.Int());
            AddColumn("dbo.MemberInfoes", "RelativeServicedTimeMonth", c => c.Int());
            DropColumn("dbo.MemberInfoes", "RelativeServicedTimeEnd");
            DropColumn("dbo.MemberInfoes", "RelativeServicedTimeEndYear");
            DropColumn("dbo.MemberInfoes", "RelativeServicedTimeEndMonth");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MemberInfoes", "RelativeServicedTimeEndMonth", c => c.Int());
            AddColumn("dbo.MemberInfoes", "RelativeServicedTimeEndYear", c => c.Int());
            AddColumn("dbo.MemberInfoes", "RelativeServicedTimeEnd", c => c.DateTime());
            DropColumn("dbo.MemberInfoes", "RelativeServicedTimeMonth");
            DropColumn("dbo.MemberInfoes", "RelativeServicedTimeYear");
            DropColumn("dbo.MemberInfoes", "RelativeServicedTime");
        }
    }
}
