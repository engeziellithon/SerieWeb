using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SerieWeb.Models
{
    [Table(name:"Episodio")]
    public class Episodio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EpisodioID { get; set; }

        [Required()]
        [MaxLength(30)]
        public String NomeEpisodio { get; set; }

        [MaxLength(255)]
        public string Sinopse { get; set; }


        [MaxLength(255)]
        public string Video { get; set; }

        public DateTime DataExibicao { get; set; }

        public int TemporadaID { get; set; }
        [ForeignKey(name: "TemporadaID")]
        public virtual Temporada Temporada { get; set; }
    }
}
