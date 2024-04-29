using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIMAIKFinanceMonitorApp.Server.Models
{
    [Table("IncomeData", Schema = "FinanceSchema")]
    public class IncomeData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; } = "";

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Nominal { get; set; }

        [DataType(DataType.Date)]
        [Column("transfer_date")]
        public DateTime TransferDate { get; set; }

        [MaxLength(255)]
        public string CreatedBy { get; set; } = "";

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

    }
}
