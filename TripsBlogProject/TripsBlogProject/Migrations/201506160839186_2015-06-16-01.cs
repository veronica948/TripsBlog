namespace TripsBlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2015061601 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Countries", "ImageUrl_CountryImageId", "dbo.CountryImages");
            DropIndex("dbo.Countries", new[] { "ImageUrl_CountryImageId" });
            AddColumn("dbo.Countries", "ImageUrl", c => c.String());
            DropColumn("dbo.Countries", "ImageUrl_CountryImageId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Countries", "ImageUrl_CountryImageId", c => c.Int());
            DropColumn("dbo.Countries", "ImageUrl");
            CreateIndex("dbo.Countries", "ImageUrl_CountryImageId");
            AddForeignKey("dbo.Countries", "ImageUrl_CountryImageId", "dbo.CountryImages", "CountryImageId");
        }
    }
}
