namespace CallManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialEntitiesCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Divisions",
                c => new
                    {
                        DivisionId = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DivisionId)
                .ForeignKey("dbo.Divisions", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        DivisionId = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Login = c.String(),
                        Email = c.String(),
                        HeadOfDivision = c.Boolean(nullable: false),
                        Man = c.Boolean(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Divisions", t => t.DivisionId, cascadeDelete: false)
                .Index(t => t.DivisionId);
            
            CreateTable(
                "dbo.Tarifs",
                c => new
                    {
                        TarifId = c.Int(nullable: false, identity: true),
                        Operator = c.String(),
                        IncCostInNet = c.Double(nullable: false),
                        OutCostInNet = c.Double(nullable: false),
                        IncCostOutNet = c.Double(nullable: false),
                        OutCostOutNet = c.Double(nullable: false),
                        MonthlyCost = c.Double(nullable: false),
                        SecondTarification = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TarifId);
            
            CreateTable(
                "dbo.Calls",
                c => new
                    {
                        CallId = c.Int(nullable: false, identity: true),
                        FromId = c.Int(nullable: false),
                        ToId = c.Int(nullable: false),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CallId)
                .ForeignKey("dbo.Phones", t => t.FromId, cascadeDelete: false)
                .ForeignKey("dbo.Phones", t => t.ToId, cascadeDelete: false)
                .Index(t => t.FromId)
                .Index(t => t.ToId);
            
            AddForeignKey("dbo.Phones", "DivisionId", "dbo.Divisions", "DivisionId", cascadeDelete: false);
            AddForeignKey("dbo.Phones", "UserId", "dbo.Users", "UserId", cascadeDelete: false);
            AddForeignKey("dbo.Phones", "TarifId", "dbo.Tarifs", "TarifId", cascadeDelete: false);
            CreateIndex("dbo.Phones", "DivisionId");
            CreateIndex("dbo.Phones", "UserId");
            CreateIndex("dbo.Phones", "TarifId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Calls", new[] { "ToId" });
            DropIndex("dbo.Calls", new[] { "FromId" });
            DropIndex("dbo.Users", new[] { "DivisionId" });
            DropIndex("dbo.Divisions", new[] { "ParentId" });
            DropIndex("dbo.Phones", new[] { "TarifId" });
            DropIndex("dbo.Phones", new[] { "UserId" });
            DropIndex("dbo.Phones", new[] { "DivisionId" });
            DropForeignKey("dbo.Calls", "ToId", "dbo.Phones");
            DropForeignKey("dbo.Calls", "FromId", "dbo.Phones");
            DropForeignKey("dbo.Users", "DivisionId", "dbo.Divisions");
            DropForeignKey("dbo.Divisions", "ParentId", "dbo.Divisions");
            DropForeignKey("dbo.Phones", "TarifId", "dbo.Tarifs");
            DropForeignKey("dbo.Phones", "UserId", "dbo.Users");
            DropForeignKey("dbo.Phones", "DivisionId", "dbo.Divisions");
            DropTable("dbo.Calls");
            DropTable("dbo.Tarifs");
            DropTable("dbo.Users");
            DropTable("dbo.Divisions");
        }
    }
}
