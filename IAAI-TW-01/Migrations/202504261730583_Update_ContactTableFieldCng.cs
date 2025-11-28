namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_ContactTableFieldCng : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "Gender", c => c.String());
            AlterColumn("dbo.Contacts", "Email", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "Email", c => c.String(maxLength: 50));
            AlterColumn("dbo.Contacts", "Gender", c => c.Int());
        }
    }
}
