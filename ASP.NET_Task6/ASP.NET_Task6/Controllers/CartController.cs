using ASP.NET_Task6.Data;
using ASP.NET_Task6.Models.Entities;
using ASP.NET_Task6.Services.CartService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Task6.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly AppDbContext _context;

        public CartController(ICartService cartService, AppDbContext context)
        {
            _cartService = cartService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.Identity.Name; 
            var cartItems = await _cartService.GetCartItemsAsync(userId);

            
            return View(cartItems);
        }


        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var userId = User.Identity.Name;
            var cartItems = await _cartService.GetCartItemsAsync(userId);

            var order = new Order
            {
                UserId = userId,
                OrderStatus = OrderStatus.Pending,
                OrderItems = new List<OrderItem>()
            };

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userId);
            if (user != null)
            {
                order.User = user;
            }

            foreach (var cartItem in cartItems)
            {
                
                var product = await _context.Products.FindAsync(cartItem.ProductId);
                var price = product.Price;

                
                var orderItem = new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    Price = price
                };

                order.OrderItems.Add(orderItem);
            }


            _context.Orders.Add(order);
            await _context.SaveChangesAsync();


            await _cartService.ClearCartAsync(userId);

            return RedirectToAction("Index", "User"); 
        }
    }
}
