namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTalbe_ExpertModifyFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Experts", "CoverPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Experts", "CoverPath");
        }
    }
}
