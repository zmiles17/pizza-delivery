using Microsoft.EntityFrameworkCore;
using PizzaDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDelivery.Repos
{
    public class PizzaDeliveryDbContext : DbContext
    {
       public DbSet<Store> Stores { get; set; }
       public DbSet<Order> Orders { get; set; }
       public DbSet<Customer> Customers { get; set; }
       public DbSet<Inventory> Inventories { get; set; }
       public DbSet<Item> Items { get; set; }
       public DbSet<Ingredient> Ingredients { get; set; }
       public DbSet<OrderItem> OrderItems { get; set; }
       public DbSet<ItemIngredient> ItemIngredients { get; set; }
       public PizzaDeliveryDbContext(DbContextOptions<PizzaDeliveryDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<OrderItem>().HasKey(o => new { o.OrderId, o.ItemId });
            modelBuilder.Entity<ItemIngredient>().HasKey(i => new { i.ItemId, i.IngredientId });
            modelBuilder.Entity<Item>().Property(i => i.Price).HasPrecision(4, 2).IsRequired();
            modelBuilder.Entity<Item>().Property(i => i.Name).IsRequired();
            modelBuilder.Entity<Ingredient>().HasIndex(i => i.Name).IsUnique();
            modelBuilder.Entity<Item>().HasIndex(i => i.Name).IsUnique();
            modelBuilder.Entity<ItemIngredient>().Property(ig => ig.Quantity).HasPrecision(4, 2).IsRequired();
            modelBuilder.Entity<Inventory>().Property(inv => inv.Quantity).HasPrecision(10, 2).IsRequired();
        }
    }
}
