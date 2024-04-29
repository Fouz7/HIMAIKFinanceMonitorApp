using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIMAIKFinanceMonitorApp.Server.Models
{
    [Table("Transactions", Schema = "FinanceSchema")]
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Debit { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Credit { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Balance { get; set; }

        public string Notes { get; set; } = "";

        [MaxLength(255)]
        public string CreatedBy { get; set; } = "";

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
    }
}
