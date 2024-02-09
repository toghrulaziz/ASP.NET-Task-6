using Microsoft.AspNetCore.Identity;

namespace ASP.NET_Task6.Models.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public ICollection<Order> Orders { get; set; } = null!;
    }
}
