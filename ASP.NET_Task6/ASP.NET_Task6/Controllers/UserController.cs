using ASP.NET_Task6.Data;
using ASP.NET_Task6.Models.Entities;
using ASP.NET_Task6.Services.CartService;
using ASP.NET_Task6.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Task6.Controllers
{
    public class UserController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly AppDbContext _context;

        public UserController(IProductService productService, AppDbContext context, ICartService cartService)
        {
            _productService = productService;
            _context = context;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index(string category)
        {
            IEnumerable<Product> products;

            if (!string.IsNullOrEmpty(category))
            {
                products = await _productService.GetProductsByCategoryAsync(category);
            }
            else
            {
                products = await _productService.GetAllProductsAsync();
            }

            var categories = await _context.Categories.ToListAsync();

            ViewBag.Categories = categories;

            return View(products);
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var userId = User.Identity.Name;
            try
            {
                await _cartService.AddToCartAsync(productId, userId);
                return RedirectToAction("Index", "Cart");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }

        }
    }
}
