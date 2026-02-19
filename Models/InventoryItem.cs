namespace StockTrack.Models
{
    public class InventoryItem
    {
        public string CustomerName { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Zone { get; set; } = string.Empty;
        public string Bin { get; set; } = string.Empty;
    }
}