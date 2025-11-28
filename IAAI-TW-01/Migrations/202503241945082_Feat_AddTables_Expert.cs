namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Feat_AddTables_Expert : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Experts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ServeAt = c.String(),
                        History = c.String(),
                        CreateAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleteAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Contacts", "Gender", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "Gender", c => c.Int(nullable: false));
            DropTable("dbo.Experts");
        }
    }
}
