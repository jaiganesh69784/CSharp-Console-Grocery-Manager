using System;
using System.Collections.Generic;
using System.Linq;
using GroceryStore.Data;
using GroceryStore.Models;

namespace GroceryStore.Services
{
    public class GroceryStoreService
    {
        // --- Product Logic ---
        public List<Product> GetAllProducts() => Database.Products;

        public void AddProduct(string name, decimal price, int qty)
        {
            int nextId = Database.Products.Any() ? Database.Products.Max(p => p.Id) + 1 : 1;
            Database.Products.Add(new Product { Id = nextId, Name = name, Price = price, Quantity = qty });
        }

        // --- Customer Logic ---
        public List<Customer> GetAllCustomers() => Database.Customers;

        public void AddCustomer(string name, string phone)
        {
            int nextId = Database.Customers.Any() ? Database.Customers.Max(c => c.Id) + 1 : 1;
            Database.Customers.Add(new Customer { Id = nextId, Name = name, Phone = phone });
        }

        // --- Order Logic ---
        public List<Order> GetAllOrders() => Database.Orders;

        public string CreateOrder(int customerId, Dictionary<int, int> cart)
        {
            var customer = Database.Customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null) return "Error: Customer not found.";

            // Validate Stock first
            foreach (var item in cart)
            {
                var product = Database.Products.FirstOrDefault(p => p.Id == item.Key);
                if (product == null) return $"Error: Product ID {item.Key} invalid.";
                if (product.Quantity < item.Value) return $"Error: Not enough stock for {product.Name}.";
            }

            // Create Order
            var order = new Order
            {
                OrderId = Database.Orders.Any() ? Database.Orders.Max(o => o.OrderId) + 1 : 1,
                Customer = customer,
                OrderDate = DateTime.Now
            };

            // Process transaction (Reduce stock and add items)
            foreach (var item in cart)
            {
                var product = Database.Products.FirstOrDefault(p => p.Id == item.Key);
                product.Quantity -= item.Value; // Reduce stock
                order.Items.Add(new OrderItem { Product = product, Quantity = item.Value });
            }

            Database.Orders.Add(order);
            return $"Success! Order #{order.OrderId} placed. Total: ${order.TotalAmount:0.00}";
        }
    }
}