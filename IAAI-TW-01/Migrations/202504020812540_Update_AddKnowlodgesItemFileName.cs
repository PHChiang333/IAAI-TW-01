namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_AddKnowlodgesItemFileName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Knowlodges", "FileName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Knowlodges", "FileName");
        }
    }
}
