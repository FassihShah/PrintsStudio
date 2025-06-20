namespace PrintsStudio.Client.Models
{
    public class CustomaizationModel
    {
        public int CustomizationOptionId { get; set; }
        public int ProductId { get; set; }
        public string NameOfOption { get; set; }
        public List<string> OptionValues { get; set; }  
    }
}
