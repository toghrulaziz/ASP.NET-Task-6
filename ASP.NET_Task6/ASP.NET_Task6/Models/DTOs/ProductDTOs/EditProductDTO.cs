using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Task6.Models.DTOs.ProductDTOs
{
    public class EditProductDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "CategoryId is required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid price")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public IFormFile ImageUrl { get; set; } = null!;

        [Required(ErrorMessage = "TagIds are required")]
        public List<int> TagIds { get; set; } = null!;
    }
}
