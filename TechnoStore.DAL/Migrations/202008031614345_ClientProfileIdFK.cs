namespace TechnoStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientProfileIdFK : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Orders", new[] { "ClientProfile_Id" });
            DropColumn("dbo.Orders", "ClientProfileId");
            RenameColumn(table: "dbo.Orders", name: "ClientProfile_Id", newName: "ClientProfileId");
            AlterColumn("dbo.Orders", "ClientProfileId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Orders", "ClientProfileId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Orders", new[] { "ClientProfileId" });
            AlterColumn("dbo.Orders", "ClientProfileId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Orders", name: "ClientProfileId", newName: "ClientProfile_Id");
            AddColumn("dbo.Orders", "ClientProfileId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "ClientProfile_Id");
        }
    }
}
