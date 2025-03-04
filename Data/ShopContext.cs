using System;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Data;

public class ShopContext : DbContext
{
    public DbSet<Product> Products { get; set; } = null!;

    public ShopContext(DbContextOptions<ShopContext> options) : base(options)   
    {

    }
}
