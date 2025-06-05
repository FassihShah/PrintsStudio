
namespace PrintsStudio.Domain.Entities
{
    public class ProductDesignTemplate
    {
        public int ProductDesignTemplateId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string TemplateImageUrl { get; set; }        // Preview image
    }
}
