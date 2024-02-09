using ASP.NET_Task6.Data;
using ASP.NET_Task6.Helpers;
using ASP.NET_Task6.Models.DTOs.ProductDTOs;
using ASP.NET_Task6.Models.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace ASP.NET_Task6.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Product> AddProductAsync(AddProductDTO dto)
        {
            var product = _mapper.Map<Product>(dto);

            var tags = _context.Tags.Where(t => dto.TagIds.Contains(t.Id)).ToList();
            product.Tags = tags;

            if (dto.ImageUrl != null)
            {
                product.ImageUrl = await UploadFileHelper.UploadFile(dto.ImageUrl);
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _context.Products
                                .Include(p => p.Category)
                                .Include(p => p.Tags)
                                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var products = await _context.Products
                        .Include(p => p.Tags)
                        .Include(p => p.Category)
                        .ToListAsync();

            if (products == null)
            {
                return null;
            }

            return products;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            return product;
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(string category)
        {
            var products = await _context.Products
                                .Include(p => p.Tags)
                                .Where(p => p.Category.Name == category)
                                .ToListAsync();

            return products;
        }

        public async Task<Product> UpdateProductAsync(EditProductDTO product)
        {
            var existingProduct = await _context.Products
                .Include (p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id == product.Id);
       

            if (existingProduct == null)
            {
                return null;
            }

            var newProduct = _mapper.Map<Product>(product);
            newProduct.ImageUrl = await UploadFileHelper.UploadFile(product.ImageUrl);

            existingProduct.Title = newProduct.Title;
            existingProduct.Description = newProduct.Description;
            existingProduct.Price = newProduct.Price;
            existingProduct.ImageUrl = newProduct.ImageUrl;
            existingProduct.CategoryId = newProduct.CategoryId;

            
            if (product.TagIds != null && product.TagIds.Any())
            {
                existingProduct.Tags.Clear();

                var tags = await _context.Tags
                    .Where(tag => product.TagIds.Contains(tag.Id))
                    .ToListAsync();

                existingProduct.Tags.AddRange(tags);
            }

            await _context.SaveChangesAsync();

            return existingProduct;
        }
    }
}
