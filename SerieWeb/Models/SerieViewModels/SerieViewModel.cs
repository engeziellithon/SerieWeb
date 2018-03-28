using System;

namespace SerieWeb.Models.SerieViewModels
{
    public class SerieViewModel
    {
        
        public int SerieID { get; set; }
        
        public int TemporadaID { get; set; }

        
        public int EpisodioID { get; set; }

        
        public String NomeEpisodio { get; set; }

      
        public string Sinopse { get; set; }

        public DateTime DataExibicao { get; set; }

       
        public string Video { get; set; }
    }
}