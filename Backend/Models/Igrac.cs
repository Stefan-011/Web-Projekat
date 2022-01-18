using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Models
{
 // Gotovo
    [Table("Igrac")]
    public class Igrac
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
        [MaxLength(20)]
        [Column("Nadimak")]
        [DataType("nvarchar(20)")]
        public string Nadimak { get; set; }

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

        [Required]
        [Column("Pozicija")]
        public Pozicija Pozicija { get; set; }

        [Column("TimID")]
        [DataType("integer")]
        [JsonIgnore]
        public virtual ETeam Tim{ get; set; }
    }
}