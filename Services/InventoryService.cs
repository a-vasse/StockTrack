using System.Collections.Generic;
using System.Linq;
using StockTrack.Models;
using System.Text.Json;
using System;
using System.IO;

namespace StockTrack.Services
{
    public class InventoryService
    {
        private readonly List<InventoryItem> _items = new();

       public void AddItem(InventoryItem item)
        {
            _items.Add(item);
            Log($"Added inventory: {item.CustomerName} | {item.SKU} | Qty: {item.Quantity} | Location: {item.Zone}{item.Bin}");
        }
        public List<InventoryItem> GetAllItems()
        {
            return _items;
        }

    public bool TransferItem(string customerName, string sku, string newZone, string newBin)
        {
            var item = _items.FirstOrDefault(i => i.CustomerName == customerName && i.SKU == sku);
            if (item == null)
                return false;

            var oldLocation = $"{item.Zone}{item.Bin}";
            item.Zone = newZone;
            item.Bin = newBin;

            Log($"Transferred inventory: {customerName} | {sku} from {oldLocation} to {newZone}{newBin}");
            return true;
        }

        private const string FileName = "inventory.json";

        public void Load()
        {
            if (File.Exists(FileName))
            {
                var json = File.ReadAllText(FileName);
                var items = JsonSerializer.Deserialize<List<InventoryItem>>(json);
                if (items != null)
                    _items.AddRange(items);
            }
        }

        public void Save()
        {
            var json = JsonSerializer.Serialize(_items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FileName, json);
            Log("Saved inventory to disk");
        }
        private const string LogFile = "inventory.log";

        private void Log(string message)
        {
            var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
            File.AppendAllText(LogFile, logEntry + Environment.NewLine);
        }
    }
}