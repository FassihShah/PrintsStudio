using Microsoft.AspNetCore.Identity;
using PrintsStudio.Domain.Entities;

namespace PrintsStudio.Infrastructure.Identity
{
    
        public class ApplicationUser:IdentityUser
        {
            public string FullName { get; set; }
            public string PhoneNumber { get; set; }
            //public string Password { get; set; }  
            public string Role { get; set; } // e.g., "Customer", "Admin", "Designer"
            public string? ProfileImageUrl { get; set; }
            public ICollection<Order> Orders { get; set; }
            public ICollection<Review> Reviews { get; set; }
            public Designer? DesignerProfile { get; set; }  // If Designer
        }

}
