namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_MemberTablesAddHash : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 50),
                        ParentId = c.Int(),
                        Code = c.String(maxLength: 5),
                        Url = c.String(maxLength: 500),
                        ControllerName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Permissions", t => t.ParentId)
                .Index(t => t.ParentId);
            
            AddColumn("dbo.Members", "PasswordHash", c => c.String(maxLength: 100));
            AddColumn("dbo.Members", "NewPermission", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Permissions", "ParentId", "dbo.Permissions");
            DropIndex("dbo.Permissions", new[] { "ParentId" });
            DropColumn("dbo.Members", "NewPermission");
            DropColumn("dbo.Members", "PasswordHash");
            DropTable("dbo.Permissions");
        }
    }
}
