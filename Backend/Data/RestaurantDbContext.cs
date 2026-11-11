using Microsoft.EntityFrameworkCore;
using RestaurantOrderingAPI.Models;

namespace RestaurantOrderingAPI.Data
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
            
    
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
         public DbSet<FoodItem> FoodItems { get; set; }
         public DbSet<Order> orders { get; set; }
         public DbSet<OrderItem> OrderItems { get; set; }

    }
}