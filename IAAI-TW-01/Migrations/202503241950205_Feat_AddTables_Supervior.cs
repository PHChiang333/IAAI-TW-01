namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Feat_AddTables_Supervior : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Superviors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PositionName = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 50),
                        Gender = c.Int(),
                        ServiceHistory = c.String(),
                        Term = c.Int(),
                        CreateAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleteAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Superviors");
        }
    }
}
