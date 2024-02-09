using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Task6.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
