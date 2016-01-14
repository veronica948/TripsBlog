namespace TripsBlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2015061602 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.CountryImages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CountryImages",
                c => new
                    {
                        CountryImageId = c.Int(nullable: false, identity: true),
                        ImageSrc = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CountryImageId);
            
        }
    }
}
