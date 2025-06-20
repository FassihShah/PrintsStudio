namespace PrintsStudio.Client.Models
{
    public class CartViewModel
    {
        public List<OrderItem> OrderItems { get; set; } = new();
        public decimal TotalPrice => OrderItems.Sum(item => item.Quantity * item.UnitPrice);
    }
}
