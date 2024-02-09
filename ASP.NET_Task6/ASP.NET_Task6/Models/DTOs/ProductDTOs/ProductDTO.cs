namespace ASP.NET_Task6.Models.DTOs.ProductDTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public string ImageUrl { get; set; } = null!;
        public int CategoryId { get; set; }
        public List<int> TagIds { get; set; } = null!;
    }
}
