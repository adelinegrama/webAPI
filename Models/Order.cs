using System;
using Shop.Enums;

namespace Shop.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    public decimal TotalPrice { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}
