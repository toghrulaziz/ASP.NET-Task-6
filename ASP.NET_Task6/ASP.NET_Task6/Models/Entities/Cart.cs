namespace ASP.NET_Task6.Models.Entities
{
    public class Cart : BaseEntity
    {
        public string UserId { get; set; } = null!;
        public List<CartItem> Items { get; set; } = null!;
    }
}
