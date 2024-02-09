using ASP.NET_Task6.Models.DTOs.ProductDTOs;
using ASP.NET_Task6.Models.Entities;

namespace ASP.NET_Task6.Services.ProductService
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int productId); 
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetProductsByCategoryAsync(string category); 
        Task<Product> AddProductAsync(AddProductDTO product); 
        Task<Product> UpdateProductAsync(EditProductDTO product); 
        Task DeleteProductAsync(int productId); 
    }
}
