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

            inventoryService.Load();

            while (true)
            {
                Console.WriteLine("\n--- LogistiCorp StockTrack ---");
                Console.WriteLine("1. Add Inventory");
                Console.WriteLine("2. View Inventory");
                Console.WriteLine("3. Transfer Inventory");
                Console.WriteLine("4. Exit");


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
                        TransferInventory(inventoryService);
                        break;
                    case "4":
                        inventoryService.Save();
                        Console.WriteLine("Inventory saved. Goodbye!");
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

            Console.Write("Zone (e.g., A, B, C): ");
            var zone = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(zone))
            {
                Console.WriteLine("Zone cannot be empty.");
                return;
            }

            Console.Write("Bin (e.g., 1, 2, 3): ");
            var bin = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(bin))
            {
                Console.WriteLine("Bin cannot be empty.");
                return;
            }

            var item = new InventoryItem
            {
                CustomerName = customer,
                SKU = sku,
                Quantity = quantity,
                Zone = zone,
                Bin = bin
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
                    Console.WriteLine($"  {item.SKU} | Qty: {item.Quantity} | Location: {item.Zone}{item.Bin}");
                }
            }
        }

        static void TransferInventory(InventoryService service)
        {
            Console.Write("Customer: ");
            var customer = Console.ReadLine() ?? string.Empty;

            Console.Write("SKU: ");
            var sku = Console.ReadLine() ?? string.Empty;

            Console.Write("New Zone: ");
            var newZone = Console.ReadLine() ?? string.Empty;

            Console.Write("New Bin: ");
            var newBin = Console.ReadLine() ?? string.Empty;

            bool success = service.TransferItem(customer, sku, newZone, newBin);

            if (success)
                Console.WriteLine("Inventory transferred successfully!");
            else
                Console.WriteLine("Item not found.");
        }
    }
}