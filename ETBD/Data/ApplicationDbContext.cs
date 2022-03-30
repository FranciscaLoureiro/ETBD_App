using ETBD.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ETBDApp.Data
{
    
    public class ApplicationDbContext : IdentityDbContext
    {
        
        public DbSet<Category> Category { get; set; }
        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        /*
        public DbSet<Entities.Action> Action { get; set; }
        
        public DbSet<Profile> Profile { get; set; }
        public DbSet<Food> Food { get; set; }
        */
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

  
}
