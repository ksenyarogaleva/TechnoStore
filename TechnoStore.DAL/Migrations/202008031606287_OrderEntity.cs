namespace TechnoStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientProfileId = c.Int(nullable: false),
                        ClientProfile_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientProfiles", t => t.ClientProfile_Id)
                .Index(t => t.ClientProfile_Id);
            
            CreateTable(
                "dbo.OrderTechnics",
                c => new
                    {
                        Order_Id = c.Int(nullable: false),
                        Technic_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_Id, t.Technic_Id })
                .ForeignKey("dbo.Orders", t => t.Order_Id, cascadeDelete: true)
                .ForeignKey("dbo.Technics", t => t.Technic_Id, cascadeDelete: true)
                .Index(t => t.Order_Id)
                .Index(t => t.Technic_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderTechnics", "Technic_Id", "dbo.Technics");
            DropForeignKey("dbo.OrderTechnics", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Orders", "ClientProfile_Id", "dbo.ClientProfiles");
            DropIndex("dbo.OrderTechnics", new[] { "Technic_Id" });
            DropIndex("dbo.OrderTechnics", new[] { "Order_Id" });
            DropIndex("dbo.Orders", new[] { "ClientProfile_Id" });
            DropTable("dbo.OrderTechnics");
            DropTable("dbo.Orders");
        }
    }
}
