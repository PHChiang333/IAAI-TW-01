namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTables_AllowHtmlForSeveralTables_and_MemberAddIsAdmin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "IsAdmin", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "IsAdmin");
        }
    }
}
