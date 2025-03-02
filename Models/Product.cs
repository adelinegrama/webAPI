using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models;

public class Product
{
    public int Id { get; set; } 
    public string? Name { get; set; }
    public string? Brand { get; set; }  

    [Column(TypeName = "decimal(18,2)")]  
    public decimal Price { get; set; }  
}
