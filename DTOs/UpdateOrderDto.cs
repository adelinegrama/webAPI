using System;
using Shop.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shop.DTOs;

public class UpdateOrderDto
{
    [Required]
    public OrderStatus Status { get; set; }
    [Required]
    public PaymentMethod PaymentMethod { get; set; }
}
