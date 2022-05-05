using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace SeuBanku.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
