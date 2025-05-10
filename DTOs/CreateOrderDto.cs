using System;
using Shop.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shop.DTOs;

public class CreateOrderDto
{
    [Required]
    public DateTime DateTime { get; set; }
    [Required]
    public OrderStatus Status { get; set; }
    [Required]
    public PaymentMethod PaymentMethod { get; set; }
    [Required]
    [MinLength(1, ErrorMessage = "At least one order item is required.")]
    public List<CreateOrderItemDto> Items { get; set; } = new();
}
