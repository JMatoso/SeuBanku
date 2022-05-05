#nullable disable

using SeuBanku.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeuBanku.Entities
{
    [Table("Accounts")]
    public class Account
    {
        [Key]
        public Guid Id { get; set; }

        [Required, Range(100000000, int.MaxValue)]
        public int AccountNumber { get; set; }

        [Required]
        public Guid? UserId { get; set; }

        [Required, Column(TypeName = "decimal(18, 2)"), DataType(DataType.Currency)]
        public decimal Balance { get; set; }

        [Required, Column(TypeName = "decimal(18, 2)"), DataType(DataType.Currency)]
        public decimal Limit { get; set; }

        [Required]
        public AccountType AccountType { get; set; }

        [Range(0, int.MaxValue)]
        public int PeriodToTakeMoneyInYears { get; set; }

        public bool IsDisabled { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OpenedDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ExpireDate { get; set; }
    }
}
