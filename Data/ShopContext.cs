using System;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Data;

public class ShopContext : DbContext
{
    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-OFSA363\\SQLEXPRESS;Database=ShopDB;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    public ShopContext(DbContextOptions<ShopContext> options) : base(options)   
    {

    }
}
