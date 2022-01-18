using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Models
{
 // Gotovo
    [Table("Trener")]
    public class Trener
    {
        [Key]
        [Column("ID")]
        [DataType("integer")]
        public int ID { get; set; }

        [Required] 
        [MaxLength(20)]
        [Column("Ime")]
        [DataType("nvarchar(20)")]
        public string Ime { get; set; }

        [Required]
        [MaxLength(40)]
        [Column("Prezime")]
        [DataType("nvarchar(40)")]
        public string Prezime  { get; set; }

        [Required]
        [Range(1,10)]
        [Column("GodineStaza")]
        [DataType("tinyint")]
        public int GodineStaza { get; set; }

        [Column("TimID")]
        [DataType("integer")]
        public virtual ETeam Tim{ get; set; }
    }
}