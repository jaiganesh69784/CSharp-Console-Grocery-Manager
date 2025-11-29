using System;

namespace GroceryStore.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name} ({Phone})";
        }
    }
}