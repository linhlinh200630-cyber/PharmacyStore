namespace PharmacyStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitPharmacy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ProductName = c.String(),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderDetailId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        CustomerName = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderId);
            
            AlterColumn("dbo.Products", "Name", c => c.String());
            AlterColumn("dbo.Products", "Unit", c => c.String());
            AlterColumn("dbo.Products", "Description", c => c.String());
            AlterColumn("dbo.Products", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            AlterColumn("dbo.Products", "ImageUrl", c => c.String(maxLength: 512));
            AlterColumn("dbo.Products", "Description", c => c.String(maxLength: 2048));
            AlterColumn("dbo.Products", "Unit", c => c.String(maxLength: 64));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 180));
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
        }
    }
}
