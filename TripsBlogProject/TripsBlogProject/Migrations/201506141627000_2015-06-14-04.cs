namespace TripsBlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2015061404 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        ImageUrl_CountryImageId = c.Int(),
                    })
                .PrimaryKey(t => t.CountryId)
                .ForeignKey("dbo.CountryImages", t => t.ImageUrl_CountryImageId)
                .Index(t => t.ImageUrl_CountryImageId);
            
            CreateTable(
                "dbo.CountryImages",
                c => new
                    {
                        CountryImageId = c.Int(nullable: false, identity: true),
                        ImageSrc = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CountryImageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Countries", "ImageUrl_CountryImageId", "dbo.CountryImages");
            DropIndex("dbo.Countries", new[] { "ImageUrl_CountryImageId" });
            DropTable("dbo.CountryImages");
            DropTable("dbo.Countries");
        }
    }
}
