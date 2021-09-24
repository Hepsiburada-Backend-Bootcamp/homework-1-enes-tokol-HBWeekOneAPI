using HBWeekOneAPI.EntityFramework.Configurations;
using HBWeekOneAPI.EntityFramework.Seeds;
using HBWeekOneAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HBWeekOneAPI.EntityFramework
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }
        
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductSeed());
        }
    }
}