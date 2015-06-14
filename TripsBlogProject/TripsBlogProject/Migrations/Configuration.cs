namespace TripsBlogProject.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TripsBlogProject.Models;
    using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

    internal sealed class Configuration : DbMigrationsConfiguration<TripsBlogProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TripsBlogProject.Models.ApplicationDbContext context)
        {
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
            //var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>());

            //-------Add roles ----------//
            //var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            //rm.Create(new IdentityRole("Admin"));
            //rm.Create(new IdentityRole("Moderator"));
            //rm.Create(new IdentityRole("User"));
            //userManager.AddToRole(userManager.FindByEmail("admin@gmail.com").Id, "Admin");
            //userManager.AddToRole(userManager.FindByEmail("admin@gmail.com").Id, "Moderator");

            //------------Add countries ---------------//
            //string[] lines = System.IO.File.ReadAllLines(@"D:\Veronica\TripsBlog\TripsBlogProject\TripsBlogProject\Additional\countries.txt");
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
        }
    }
}
