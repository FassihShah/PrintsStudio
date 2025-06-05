namespace PrintsStudio.Domain.Entities
{
    public enum OrderStatus
    {
        Pending,
        InProduction,
        Shipped,
        Delivered,
        Cancelled
    }

    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItem> Items { get; set; }
        public string DeliveryAddress { get; set; }
    }
}
