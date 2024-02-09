using ASP.NET_Task6.Data;
using ASP.NET_Task6.Models.DTOs.CartDTOs;
using ASP.NET_Task6.Models.DTOs.ProductDTOs;
using ASP.NET_Task6.Models.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Task6.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CartService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddToCartAsync(int productId, string userId)
        {
            var cart = await _context.Carts
        .Include(c => c.Items) 
        .SingleOrDefaultAsync(c => c.UserId == userId);

            if (cart != null)
            {
                var existingCartItem = cart.Items.SingleOrDefault(ci => ci.ProductId == productId);

                if (existingCartItem != null)
                {
                    existingCartItem.Quantity++;
                }
                else
                {
                    var newCartItem = new CartItem
                    {
                        ProductId = productId,
                        Quantity = 1 
                    };
                    cart.Items.Add(newCartItem);
                }
            }
            else
            {
                cart = new Cart
                {
                    UserId = userId,
                    Items = new List<CartItem>
            {
                new CartItem
                {
                    ProductId = productId,
                    Quantity = 1 
                }
            }
                };
                _context.Carts.Add(cart);
            }

            await _context.SaveChangesAsync();
        }

        public async Task ClearCartAsync(string userId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart != null)
            {
                var cartItems = await _context.CartItems.Where(item => item.CartId == cart.Id).ToListAsync();

                _context.CartItems.RemoveRange(cartItems);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CartItem>> GetCartItemsAsync(string userId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items) 
                .SingleOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                return new List<CartItem>();
            }
            var cartItems = cart.Items.ToList();
            return cartItems;
        }

        public Task RemoveFromCartAsync(int productId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
