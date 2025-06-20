using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PrintsStudio.Client.Services;

namespace PrintsStudio.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7032/") // ? Your API URL
            });
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<DesignerService>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<ProductDesignTemplateService>();
            builder.Services.AddScoped<CustomizationOptionService>();
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<OrderItemService>();
            builder.Services.AddScoped<BookingService>();
            builder.Services.AddScoped<ReviewService>();
            builder.Services.AddScoped<ContactFormService>();



            await builder.Build().RunAsync();
        }
    }
}
