namespace ASP.NET_Task6.Models.Entities
{
    public class OrderItem : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
    }
}
