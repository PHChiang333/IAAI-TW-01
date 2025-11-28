namespace IAAI_TW_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_TableAdminMemberRenamed : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 100),
                        PasswordSalt = c.String(maxLength: 100),
                        PasswordHash = c.String(),
                        Permission = c.String(maxLength: 500),
                        IsTopAccess = c.Boolean(nullable: false),
                        NewPermission = c.String(maxLength: 500),
                        CreateAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleteAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AdminMembers");
        }
    }
}
