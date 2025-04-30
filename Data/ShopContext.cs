using System;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Data;

public class ShopContext : DbContext
{
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Order> Orders{ get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;

    public ShopContext(DbContextOptions<ShopContext> options) : base(options)   
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Составной ключ для OrderItem
        builder.Entity<OrderItem>()
            .HasKey(oi => new { oi.Id, oi.ProductId });

        // Связь Order 1:N OrderItem
        builder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.Id)
            .OnDelete(DeleteBehavior.Cascade);

        // Связь Product 1:N OrderItem
        builder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
