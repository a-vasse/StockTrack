namespace StockTrack.Models
{
    public class InventoryItem
    {
        public string CustomerName { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public string WarehouseLocation { get; set; }
    }
}