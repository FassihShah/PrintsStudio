﻿namespace PrintsStudio.Client.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        public int ProductId { get; set; }

        public string UserId { get; set; }

        public int Rating { get; set; }            // 1 to 5
        public string Comment { get; set; }

        public DateTime ReviewDate { get; set; }
    }

}
