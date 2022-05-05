using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable 

namespace SeuBanku.Entities
{
    [Table("Services")]
    public class Service
    {
        [Key]
        public Guid Id { get; set; }

        [Required, Range(100000000, int.MaxValue)]
        public int AccountNumber { get; set; }

        [Required, StringLength(100)]
        public string ServiceName { get; set; }

        [Required, Column(TypeName = "decimal(18, 2)"), DataType(DataType.Currency)]
        public decimal Cost { get; set; }

        public bool IsDisabled { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
    }
}
