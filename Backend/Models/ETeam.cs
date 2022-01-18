using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    // Gotovo
    [Table("ETeam")]
    public class ETeam
    {
        [Key]
        [Column("ID")]
        [DataType("integer")]
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("Naziv")]
        [DataType("nvarchar(20)")]
        public string Naziv { get; set; }
      
        [Required]
        [Range(5,10)]
        [Column("MaxIgraci")]
        [DataType("tinyint")]
        public int MaxIgraci { get; set; }
        [JsonIgnore]
        public List<Igrac> Igraci { get; set; }

        [JsonIgnore]
        public Sponzor Spozor { get; set; }

    }
}