using InventoryManagementSystem.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Api.Data
{
    public class AppDbContext : DbContext
    {
        // The constructor passes the configuration settings (like connection string) to the base DbContext
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // These DbSets tell EF Core which models should become database tables
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Category> Categories { get; set; }

        // This method allows us to seed initial data right as the database is being built
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Let's seed two categories so our UI isn't completely blank when we first build it
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Semiconductor Sensors", Description = "High-precision components for automated wafer manufacturing." },
                new Category { Id = 2, Name = "Medical Imaging Spares", Description = "Critical spare parts for CT and MRI sub-systems." }
            );
        }
    }
}
