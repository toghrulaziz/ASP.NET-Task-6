namespace ASP.NET_Task6.Models.Entities
{
    public class CartItem : BaseEntity
    {
        public int ProductId { get; set; } 
        public int Quantity { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; } = null!;
        
    }
}
