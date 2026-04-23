namespace PrintsStudio.Domain.Entities
{
    public class Designer
    {
        public int DesignerId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string PortfolioUrl { get; set; } = string.Empty;
        public string ProfileImageUrl { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
    }

}
