using System;
using Shop.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shop.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OrderStatus Status { get; set; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>(); // id и количество товара
        
    [Column(TypeName = "decimal(18,2)")]  
    public decimal TotalPrice { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PaymentMethod PaymentMethod { get; set; }
}
