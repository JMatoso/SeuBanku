using SeuBanku.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace SeuBanku.Entities
{
    [Table("Extracts")]
    public class Extract
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "decimal(18, 2)"), DataType(DataType.Currency)]
        public decimal OutcomingBalance { get; set; }

        [Range(100000000, int.MaxValue)]
        public int OutAccountNumber { get; set; }

        [Range(100000000, int.MaxValue)]
        public int InAccountNumber { get; set; }

        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }

        [Required]
        public BankOperations Operation { get; set; }

        public string Reference { get; set; }

        public bool IsApproved { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OperationDate { get; set; }
    }
}
