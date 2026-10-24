using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MywebApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> products { get; set; }

        public DbSet<ApplicationUser> applicationUsers { get; set; }

        public DbSet<Cart> Carts { get; set; }

        //private DbSet<ApplicationUser> applicationUsers;

        //public DbSet<ApplicationUser> GetApplicationUsers()
        //{
        //    return applicationUsers;
        //}

        //public void SetApplicationUsers(DbSet<ApplicationUser> value)
        //{
        //    applicationUsers = value;
        //}
    }
}