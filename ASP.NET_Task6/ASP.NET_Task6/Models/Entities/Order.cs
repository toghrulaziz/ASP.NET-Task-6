namespace ASP.NET_Task6.Models.Entities
{
    public class Order : BaseEntity
    {
        public int OrderNumber { get; set; }
        public string UserId { get; set; } = null!;
        public AppUser User { get; set; } = null!;
        public List<OrderItem> OrderItems { get; set; } = null!;
        public OrderStatus OrderStatus { get; set; }
    }
}
