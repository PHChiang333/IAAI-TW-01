namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTalbe_ExpertModifyFields_AddCoverNameAndCHangeDisplayName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Experts", "CoverName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Experts", "CoverName");
        }
    }
}
