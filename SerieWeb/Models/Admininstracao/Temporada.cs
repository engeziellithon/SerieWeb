using System;
using System.Collections.Generic;
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
        [MaxLength(30)]
        public String NomeTemporada { get; set; }
        
    }
}
