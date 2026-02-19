using System.Collections.Generic;
using System.Linq;
using StockTrack.Models;

namespace StockTrack.Services
{
    public class InventoryService
    {
        private readonly List<InventoryItem> _items = new();

        public void AddItem(InventoryItem item)
        {
            _items.Add(item);
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

            item.Zone = newZone;
            item.Bin = newBin;
            return true;
        }
    }
}