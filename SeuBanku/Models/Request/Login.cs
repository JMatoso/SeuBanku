#nullable disable

using System.ComponentModel.DataAnnotations;

namespace SeuBanku.Models.Request
{
    public class Login
    {
        [Required, DataType(DataType.EmailAddress), EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
