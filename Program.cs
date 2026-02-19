using System;
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
            var customer = Console.ReadLine();

            Console.Write("SKU: ");
            var sku = Console.ReadLine();

            Console.Write("Quantity: ");
            var quantity = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Warehouse Location: ");
            var location = Console.ReadLine();

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

            foreach (var item in items)
            {
                Console.WriteLine($"{item.CustomerName} | {item.SKU} | Qty: {item.Quantity} | {item.WarehouseLocation}");
            }
        }
    }
}