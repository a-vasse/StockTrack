using System.Collections.Generic;
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
    }
}