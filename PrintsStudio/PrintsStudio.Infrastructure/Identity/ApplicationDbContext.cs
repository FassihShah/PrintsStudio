using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PrintsStudio.Domain.Entities;

namespace PrintsStudio.Infrastructure.Identity
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public ApplicationDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=PrintsStudio;Integrated Security=True;");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ContactForm> ContactForms { get; set; }
        public DbSet<ProductDesignTemplate> ProductDesignTemplates { get; set; }
        public DbSet<CustomizationOption> CustomizationOptions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Designer> Designers { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
