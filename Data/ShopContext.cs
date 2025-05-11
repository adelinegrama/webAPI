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
       base.OnModelCreating(builder);

        // связь OrderItem и Product
        builder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // связь OrderItem и Order
        builder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
