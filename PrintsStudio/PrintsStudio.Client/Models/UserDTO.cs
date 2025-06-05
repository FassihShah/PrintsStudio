namespace scd_project.Models
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // e.g., "Customer", "Admin", "Designer"
        public string ProfileImageUrl { get; set; }
        public ICollection<Review> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public Designer DesignerProfile { get; set; }
    }
}
