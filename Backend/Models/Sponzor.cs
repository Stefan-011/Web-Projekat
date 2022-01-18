using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
     [Table("Sponzor")]
    public class Sponzor
    {
            [Key]
            [Column("ID")]
            [DataType("integer")]
            public int ID { get; set; }

            [Required]
            [MaxLength(25)]
            [Column("Naziv")]
            public string Naziv { get; set; }

            [Required]
            [Column("Iznos")]
            [Range(1000,1000000)]
            [DataType("integer")]
            public int Iznos { get; set; }        
    }

}