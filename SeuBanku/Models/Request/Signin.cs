using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace SeuBanku.Models.Request
{
    public class Signin
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public AccountType AccountType { get; set; }

        [Column(TypeName = "decimal(18, 2)"), DataType(DataType.Currency)]
        public decimal IntialBalance { get; set; }

        [Column(TypeName = "decimal(18, 2)"), DataType(DataType.Currency)]
        public decimal DiaryLimit { get; set; }

        [Range(0, int.MaxValue)]
        public int PeriodToTakeMoneyInYears { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
