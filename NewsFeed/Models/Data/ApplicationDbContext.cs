using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsFeed.Models.Identity;

namespace NewsFeed.Models.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<New> News { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<New>().ToTable("New");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Subscription>().ToTable("Subscription");
        }


    }
}
