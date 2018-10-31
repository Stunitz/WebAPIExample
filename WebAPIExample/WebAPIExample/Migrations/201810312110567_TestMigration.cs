namespace WebAPIExample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Gender = c.Int(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        PricePerUnit = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Purchase",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        productId = c.Long(nullable: false),
                        customerId = c.Long(nullable: false),
                        amount = c.Int(nullable: false),
                        datePurchase = c.DateTime(nullable: false),
                        pricePerUnity = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Purchase");
            DropTable("dbo.Product");
            DropTable("dbo.Customer");
        }
    }
}
