using Assignment2.Core.Models;
using Assignment2.Core.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Core.Models.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor used by Dependency Injection
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet properties for each entity - represents tables
        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Order> Orders { get; set; } // Use singular 'Order' for the class/DbSet name
        public DbSet<OrderDetail> OrderDetails { get; set; }

        // Optional: Fluent API configuration (alternative/complement to Data Annotations)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Example: Configure unique constraint for User Email if not using Index attribute
            // modelBuilder.Entity<User>()
            //     .HasIndex(u => u.Email)
            //     .IsUnique();

            // Example: Define table names explicitly if needed (EF Core usually pluralizes)
            // modelBuilder.Entity<User>().ToTable("Users");
            // modelBuilder.Entity<Item>().ToTable("Item");
            // modelBuilder.Entity<Agent>().ToTable("Agent");
            // modelBuilder.Entity<Order>().ToTable("Order"); // Be careful with SQL keywords
            // modelBuilder.Entity<OrderDetail>().ToTable("OrderDetail");

            // Example: Configure cascade delete behavior if needed (default is often Cascade for required relationships)
            // modelBuilder.Entity<Order>()
            //     .HasMany(o => o.OrderDetails)
            //     .WithOne(od => od.Order)
            //     .OnDelete(DeleteBehavior.Cascade); // Or Restrict, SetNull, NoAction

            // Configure decimal precision for OrderDetail.UnitAmount if not done via attribute
            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.UnitAmount)
                .HasColumnType("decimal(18, 2)");

        }
    }
}