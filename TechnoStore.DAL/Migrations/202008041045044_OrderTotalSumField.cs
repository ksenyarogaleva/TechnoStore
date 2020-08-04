namespace TechnoStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderTotalSumField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "TotalSum", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "TotalSum");
        }
    }
}
