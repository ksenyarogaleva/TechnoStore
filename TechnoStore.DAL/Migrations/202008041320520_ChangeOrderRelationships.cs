namespace TechnoStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOrderRelationships : DbMigration
    {
        public override void Up()
        {
            
            DropForeignKey("dbo.OrderDetails", "Id", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "Id" });
            DropPrimaryKey("dbo.OrderDetails");
            AddColumn("dbo.Orders", "TechnicId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "OrderDetailsId", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderDetails", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.OrderDetails", "Id");
            CreateIndex("dbo.Orders", "TechnicId");
            CreateIndex("dbo.Orders", "OrderDetailsId");
            AddForeignKey("dbo.Orders", "TechnicId", "dbo.Technics", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "OrderDetailsId", "dbo.OrderDetails", "Id", cascadeDelete: true);
            DropColumn("dbo.Orders", "TotalSum");
        }
        
        public override void Down()
        {
            
            AddColumn("dbo.Orders", "TotalSum", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.Orders", "OrderDetailsId", "dbo.OrderDetails");
            DropForeignKey("dbo.Orders", "TechnicId", "dbo.Technics");
            DropIndex("dbo.Orders", new[] { "OrderDetailsId" });
            DropIndex("dbo.Orders", new[] { "TechnicId" });
            DropPrimaryKey("dbo.OrderDetails");
            AlterColumn("dbo.OrderDetails", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "OrderDetailsId");
            DropColumn("dbo.Orders", "TechnicId");
            AddPrimaryKey("dbo.OrderDetails", "Id");
            CreateIndex("dbo.OrderDetails", "Id");
            AddForeignKey("dbo.OrderDetails", "Id", "dbo.Orders", "Id");
        }
    }
}
