using System.Collections.Generic;
using GroceryStore.Models;

namespace GroceryStore.Data
{
    public static class Database
    {
        // Pre-loaded dummy data so you can test immediately
        public static List<Product> Products = new List<Product>()
        {
            new Product { Id = 1, Name = "Milk", Price = 2.50m, Quantity = 20 },
            new Product { Id = 2, Name = "Bread", Price = 1.20m, Quantity = 50 },
            new Product { Id = 3, Name = "Eggs", Price = 3.00m, Quantity = 30 },
            new Product { Id = 4, Name = "Apple", Price = 0.50m, Quantity = 100 }
        };

        public static List<Customer> Customers = new List<Customer>()
        {
            new Customer { Id = 1, Name = "John Doe", Phone = "555-0101" },
            new Customer { Id = 2, Name = "Jane Smith", Phone = "555-0102" }
        };

        public static List<Order> Orders = new List<Order>();
    }
}