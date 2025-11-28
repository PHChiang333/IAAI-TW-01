namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_ModifyTable_SupervisorName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Superviors", newName: "Supervisors");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Supervisors", newName: "Superviors");
        }
    }
}
