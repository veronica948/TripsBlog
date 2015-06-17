namespace TripsBlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2015061701 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Posts", new[] { "country_CountryId" });
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Posts", "Country_CountryId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Posts", new[] { "Country_CountryId" });
            DropColumn("dbo.AspNetUsers", "Discriminator");
            CreateIndex("dbo.Posts", "country_CountryId");
        }
    }
}
