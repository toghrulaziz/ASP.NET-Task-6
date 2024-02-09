using ASP.NET_Task6.Models.DTOs.CartDTOs;
using ASP.NET_Task6.Models.DTOs.ProductDTOs;
using ASP.NET_Task6.Models.Entities;

namespace ASP.NET_Task6.Services.CartService
{
    public interface ICartService
    {
        Task AddToCartAsync(int productId, string userId); 
        Task RemoveFromCartAsync(int productId, string userId); 
        Task<List<CartItem>> GetCartItemsAsync(string userId); 
        Task ClearCartAsync(string userId); 
    }
}
