namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_MemberTablesAddHash02 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "PasswordHash", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "PasswordHash", c => c.String(maxLength: 100));
        }
    }
}
