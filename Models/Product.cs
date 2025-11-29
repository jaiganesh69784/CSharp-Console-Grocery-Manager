using System;

namespace GroceryStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } // Current Stock

        public override string ToString()
        {
            return $"[{Id}] {Name} -- ${Price} -- Stock: {Quantity}";
        }
    }
}