
namespace PrintsStudio.Domain.Entities
{
    public class ProductDesignTemplate
    {
        public int id{ get; set; }
        public int ProductId { get; set; }
        public string TemplateImageUrl { get; set; }        // Preview image
    }
}
