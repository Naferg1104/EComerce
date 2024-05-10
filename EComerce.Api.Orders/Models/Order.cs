﻿namespace EComerce.Api.Orders.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
