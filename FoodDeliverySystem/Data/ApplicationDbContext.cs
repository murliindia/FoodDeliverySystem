// ApplicationDbContext.cs
namespace FoodDeliverySystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using FoodDeliverySystem.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Cuisine> Cuisines { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and keys
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Dish)
                .WithMany()
                .HasForeignKey(oi => oi.DishId);

            modelBuilder.Entity<Dish>()
                .HasOne(d => d.Hotel)
                .WithMany(h => h.Dishes)
                .HasForeignKey(d => d.HotelId);

            modelBuilder.Entity<Dish>()
                .HasOne(d => d.Cuisine)
                .WithMany(c => c.Dishes)
                .HasForeignKey(d => d.CuisineId);
        }
    }

}