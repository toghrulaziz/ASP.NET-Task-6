using ASP.NET_Task6.Data;
using ASP.NET_Task6.Helpers;
using ASP.NET_Task6.Models.DTOs.ProductDTOs;
using ASP.NET_Task6.Models.Entities;
using ASP.NET_Task6.Services.ProductService;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Task6.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HomeController(IProductService productService, AppDbContext context, IMapper mapper)
        {
            _productService = productService;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        //add
        public IActionResult Add()
        {
            var categories = _context.Categories.ToList();
            var tags = _context.Tags.ToList();
            ViewData["Categories"] = categories;
            ViewData["Tags"] = tags;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var product = await _productService.AddProductAsync(dto);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(dto);
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "The file type is not accepted.";
                return View(dto);
            }
        }



        // delete
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        // update 
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(); 
            }

            var categories = await _context.Categories.ToListAsync();
            var tags = await _context.Tags.ToListAsync();

            ViewData["Categories"] = categories;
            ViewData["Tags"] = tags;

            var dto = _mapper.Map<EditProductDTO>(product);

            return View(dto);
        }


        [HttpPost]
        public async Task<IActionResult> Update(EditProductDTO updatedProduct)
        {
            try
            {
                var updatedProductDTO = await _productService.UpdateProductAsync(updatedProduct);
                if (updatedProductDTO == null)
                {
                    return NotFound(); 
                }

                return RedirectToAction("Index"); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); 
            }
        }


        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _context.Orders.Include(o => o.OrderItems).ToListAsync();
            return View(orders);
        }


        public async Task<IActionResult> AcceptOrder(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            order.OrderStatus = OrderStatus.Approved;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return RedirectToAction("GetAllOrders"); 
        }


        public async Task<IActionResult> RejectOrder(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            order.OrderStatus = OrderStatus.Cancelled; 
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return RedirectToAction("GetAllOrders"); 
        }



    }
}
