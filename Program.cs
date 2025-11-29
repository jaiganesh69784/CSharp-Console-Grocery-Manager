using System;
using System.Collections.Generic;
using GroceryStore.Services;

namespace GroceryStore
{
    class Program
    {
        static GroceryStoreService service = new GroceryStoreService();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== 🛒 SUPERMARKET MANAGER ===");
                Console.WriteLine("1. View Products");
                Console.WriteLine("2. Add New Product");
                Console.WriteLine("3. View Customers");
                Console.WriteLine("4. Add New Customer");
                Console.WriteLine("5. Create Order (Checkout)");
                Console.WriteLine("6. View Order History");
                Console.WriteLine("0. Exit");
                Console.Write("\nSelect Option: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1": ViewProducts(); break;
                    case "2": AddProduct(); break;
                    case "3": ViewCustomers(); break;
                    case "4": AddCustomer(); break;
                    case "5": CreateOrder(); break;
                    case "6": ViewOrders(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid option!"); break;
                }

                Console.WriteLine("\nPress Enter to return to menu...");
                Console.ReadLine();
            }
        }

        static void ViewProducts()
        {
            Console.WriteLine("\n--- Product List ---");
            foreach (var p in service.GetAllProducts()) Console.WriteLine(p);
        }

        static void AddProduct()
        {
            Console.WriteLine("\n--- Add Product ---");
            Console.Write("Name: "); string name = Console.ReadLine();
            Console.Write("Price: "); decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Quantity: "); int qty = int.Parse(Console.ReadLine());
            service.AddProduct(name, price, qty);
            Console.WriteLine("Product Added!");
        }

        static void ViewCustomers()
        {
            Console.WriteLine("\n--- Customer List ---");
            foreach (var c in service.GetAllCustomers()) Console.WriteLine(c);
        }

        static void AddCustomer()
        {
            Console.WriteLine("\n--- Add Customer ---");
            Console.Write("Name: "); string name = Console.ReadLine();
            Console.Write("Phone: "); string phone = Console.ReadLine();
            service.AddCustomer(name, phone);
            Console.WriteLine("Customer Added!");
        }

        static void CreateOrder()
        {
            Console.WriteLine("\n--- New Order ---");
            ViewCustomers();
            Console.Write("Enter Customer ID: ");
            if (!int.TryParse(Console.ReadLine(), out int custId)) return;

            Dictionary<int, int> cart = new Dictionary<int, int>();

            while (true)
            {
                Console.Clear();
                ViewProducts();
                Console.WriteLine("\n(Enter '0' to finish selection)");
                Console.Write("Enter Product ID to Buy: ");
                int pId = int.Parse(Console.ReadLine());
                if (pId == 0) break;

                Console.Write("Enter Quantity: ");
                int qty = int.Parse(Console.ReadLine());

                if (cart.ContainsKey(pId)) cart[pId] += qty;
                else cart.Add(pId, qty);
            }

            if (cart.Count > 0)
            {
                Console.WriteLine(service.CreateOrder(custId, cart));
            }
        }

        static void ViewOrders()
        {
            Console.WriteLine("\n--- Order History ---");
            var orders = service.GetAllOrders();
            if (orders.Count == 0) Console.WriteLine("No orders found.");

            foreach (var order in orders)
            {
                Console.WriteLine("-----------------------------");
                Console.WriteLine(order);
                Console.WriteLine("Items:");
                foreach(var item in order.Items)
                {
                    Console.WriteLine($" - {item.Product.Name} x {item.Quantity} = ${item.SubTotal:0.00}");
                }
            }
        }
    }
}