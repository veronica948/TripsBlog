using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TripsBlogProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public List<Post> Posts { get; set; }
        //List<IdentityRole> Roles { get; set; }
        
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<TripsBlogProject.Models.Country> Countries { get; set; }

        public System.Data.Entity.DbSet<TripsBlogProject.Models.Post> Posts { get; set; }

    }

    public class UserWithRoles 
    {
        public ApplicationUser User { get; set; }
        public List<string> UserRoles { get; set; }

    }
    public class UserRolesViewModel
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }

    }
    public class CheckRolesListBoxItem     {         
        public string RoleId { get; set; }         
        public string RoleName { get; set; }         
        public bool IsCheck { get; set; }     
   }
}
