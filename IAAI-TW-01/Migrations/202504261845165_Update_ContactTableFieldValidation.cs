namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_ContactTableFieldValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "Tel", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "Tel", c => c.String(maxLength: 50));
        }
    }
}
