using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        public decimal TotalAmount => Items.Sum(i => i.SubTotal);

        public override string ToString()
        {
            return $"Order #{OrderId} - {Customer.Name} - Total: ${TotalAmount:0.00} - {OrderDate.ToShortDateString()}";
        }
    }

    public class OrderItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal => Product.Price * Quantity;
    }
}