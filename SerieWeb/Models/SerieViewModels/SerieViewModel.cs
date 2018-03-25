using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SerieWeb.Models.SerieViewModels
{
    public class SerieViewModel
    {
        [Required()]
        public int SerieID { get; set; }

        [Required()]
        public int TemporadaID { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EpisodioID { get; set; }

        [Required()]
        [MaxLength(30)]
        public String NomeEpisodio { get; set; }

        [MaxLength(255)]
        public string Sinopse { get; set; }

        public DateTime DataExibicao { get; set; }

        [MaxLength(255)]
        public string Video { get; set; }

    }
}
