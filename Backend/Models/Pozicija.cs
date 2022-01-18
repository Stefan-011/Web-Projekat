using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
     [Table("Pozicija")]
    public class Pozicija
    {
        [Key]
        [Column("ID")]
        [DataType("integer")]
        public int ID {get;set;}

        [Required]
        [MaxLength(25)]
        [Column("Ime")]
        public string Naziv { get; set; }

    }

}