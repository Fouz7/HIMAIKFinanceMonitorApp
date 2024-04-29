using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIMAIKFinanceMonitorApp.Server.Models
{
    [Table("Users", Schema = "FinanceSchema")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [MaxLength(255)]
        public string Username { get; set; } ="";

        [MaxLength(255)]
        public string Fullname { get; set; } ="";

        [MaxLength(255)]
        public string NIM { get; set; } ="";

        [MaxLength(255)]
        public string Pic { get; set; } ="";

        [MaxLength(255)]
        public string Password { get; set; } = "";

        [MaxLength(255)]
        public string Role { get; set; } = "";

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

    }
}
