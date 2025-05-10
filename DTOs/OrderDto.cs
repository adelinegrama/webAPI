using System;
using Shop.Enums;

namespace Shop.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public OrderStatus Status { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
}
