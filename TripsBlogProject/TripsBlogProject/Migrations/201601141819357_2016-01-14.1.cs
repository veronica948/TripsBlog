namespace TripsBlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201601141 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "Country_CountryId", "dbo.Countries");
            DropIndex("dbo.Posts", new[] { "Country_CountryId" });
            AddColumn("dbo.Posts", "Country", c => c.String(nullable: false));
            DropColumn("dbo.Countries", "ImageUrl");
            DropColumn("dbo.Posts", "Country_CountryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Country_CountryId", c => c.Int(nullable: false));
            AddColumn("dbo.Countries", "ImageUrl", c => c.String());
            DropColumn("dbo.Posts", "Country");
            CreateIndex("dbo.Posts", "Country_CountryId");
            AddForeignKey("dbo.Posts", "Country_CountryId", "dbo.Countries", "CountryId", cascadeDelete: true);
        }
    }
}
