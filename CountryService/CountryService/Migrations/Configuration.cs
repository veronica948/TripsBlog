namespace CountryService.Migrations
{
    using CountryService.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CountryService.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CountryService.Models.ApplicationDbContext context)
        {
            //------------Add countries ---------------//
            //string[] lines = System.IO.File.ReadAllLines(@"D:\StudyProject\TripsBlog\CountryService\CountryService\Additional\countries.txt");
            //Console.Write(lines.Length);
            //int i = 0;
            //foreach (string line in lines)
            //{
            //    // Use a tab to indent each line of the file.
            //    Console.WriteLine("\t" + line);
            //    context.Countries.Add(new Country { Name = line });
            //    i++;
            //}
            //context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
