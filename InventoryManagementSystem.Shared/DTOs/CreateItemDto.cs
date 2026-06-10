namespace InventoryManagementSystem.Shared.DTOs
{
    public class CreateItemDto
    {
        public string Sku { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int CategoryId { get; set; }
    }
}