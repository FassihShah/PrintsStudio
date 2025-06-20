namespace PrintsStudio.Client.Models
{
    public class CustomizationOption
    {
        public int CustomizationOptionId { get; set; }

        public int ProductId { get; set; }

        public string OptionName { get; set; } // e.g., "Color", "Print Area", "Size"
        public string OptionValue { get; set; } // e.g., "Red", "Front Only", "Large"
    }

}
