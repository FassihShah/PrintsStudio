namespace PrintsStudio.Client.Models
{
    public class Booking
    {
        public int id { get; set; }
        public string UserId { get; set; }
        public int DesignerId { get; set; }
        public string details { get; set; }
        public string budget { get; set; }
        public string contactDetails { get; set; }
        public string status { get; set; }
        public DateTime deadline { get; set; }
    }
}

