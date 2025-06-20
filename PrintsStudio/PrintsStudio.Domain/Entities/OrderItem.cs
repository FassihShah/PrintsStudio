namespace PrintsStudio.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string SelectedDesignTemplateUrl { get; set; }
        // User Selects Design or uploadd
        public List<string> AppliedCustomizations { get; set; }
    }

}
