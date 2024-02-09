using ASP.NET_Task6.Services.CartService;
using ASP.NET_Task6.Services.ProductService;
using Microsoft.AspNetCore.Hosting;

namespace ASP.NET_Task6
{
    public static class DI
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICartService, CartService>();
            return services;
        }
    }
}
