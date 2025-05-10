using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.DTOs;

public class UpdateProductDto
{
    [Required]
    [StringLength(100, MinimumLength = 4)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string Brand { get; set; } = null!;

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be positive.")]
    public decimal Price { get; set; }
}
