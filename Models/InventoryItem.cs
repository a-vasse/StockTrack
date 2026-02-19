namespace StockTrack.Models
{
    public class InventoryItem
    {
        public string CustomerName { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string WarehouseLocation { get; set; } = string.Empty;
    }
}