﻿namespace PrintsStudio.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } // e.g., Mug, T-shirt, Custom Product
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public List<string> Images { get; set; } 
        public List<CustomizationOption> CustomizationOptions { get; set; }
    }

}
