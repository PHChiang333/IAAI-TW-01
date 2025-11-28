namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Feat_AddTableContactConnection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Gender = c.Int(nullable: false),
                        Tel = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Contacts");
        }
    }
}
