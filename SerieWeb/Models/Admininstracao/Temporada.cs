using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SerieWeb.Models.Admininstracao
{
    [Table(name: "Temporada")]
    public class Temporada 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TemporadaID { get; set; }

        [Required()]
        [StringLength(15, MinimumLength = 9)]
        public String NomeTemporada { get; set; }        
    }
}
