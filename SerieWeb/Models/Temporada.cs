using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SeriesWeb.Models
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

        public int SerieID { get; set; }
        [ForeignKey(name: "SerieID")]
        public virtual Serie Serie { get; set; }

        [ForeignKey("TemporadaID")]
        public ICollection<Episodio> Episodio { get; set; }
    }
}
