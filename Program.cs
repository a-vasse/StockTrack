using System;
using System.Linq;
using StockTrack.Models;
using StockTrack.Services;

namespace StockTrack
{
    class Program
    {
        static void Main(string[] args)
        {
            var inventoryService = new InventoryService();

            while (true)
            {
                Console.WriteLine("\n--- LogistiCorp StockTrack ---");
                Console.WriteLine("1. Add Inventory");
                Console.WriteLine("2. View Inventory");
                Console.WriteLine("3. Exit");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddInventory(inventoryService);
                        break;
                    case "2":
                        ViewInventory(inventoryService);
                        break;
                    case "3":
                        return;
                }
            }
        }

        static void AddInventory(InventoryService service)
        {
            Console.Write("Customer: ");
            var customer = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(customer))
            {
                Console.WriteLine("Customer cannot be empty.");
                return;
            }

            Console.Write("SKU: ");
            var sku = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(sku))
            {
                Console.WriteLine("SKU cannot be empty.");
                return;
            }

            Console.Write("Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity < 0)
            {
                Console.WriteLine("Invalid quantity.");
                return;
            }

            Console.Write("Warehouse Location: ");
            var location = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(location))
            {
                Console.WriteLine("Location cannot be empty.");
                return;
            }

            var item = new InventoryItem
            {
                CustomerName = customer,
                SKU = sku,
                Quantity = quantity,
                WarehouseLocation = location
            };

            service.AddItem(item);

            Console.WriteLine("Inventory added!");
        }

        static void ViewInventory(InventoryService service)
        {
            var items = service.GetAllItems();

            if (items.Count == 0)
            {
                Console.WriteLine("No inventory found.");
                return;
            }

            var grouped = items.GroupBy(i => i.CustomerName);

            foreach (var group in grouped)
            {
                Console.WriteLine($"\nCustomer: {group.Key}");

                foreach (var item in group)
                {
                    Console.WriteLine($"  {item.SKU} | Qty: {item.Quantity} | {item.WarehouseLocation}");
                }
            }
        }
    }
}