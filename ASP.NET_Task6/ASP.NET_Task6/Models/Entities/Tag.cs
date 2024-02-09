namespace ASP.NET_Task6.Models.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; } = null!;
        public List<Product>? Products { get; set; }
    }
}
