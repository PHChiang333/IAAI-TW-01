namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_GenderTypeCngIntoString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Supervisors", "Gender", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Supervisors", "Gender", c => c.Int());
        }
    }
}
