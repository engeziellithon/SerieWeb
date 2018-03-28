using System.Collections.Generic;

namespace SerieWeb.Models.SerieViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Serie> BannerDestaque { get; set; }

        public IEnumerable<Serie> SerieStreming { get; set; }

        public IEnumerable<Genero> Generos { get; set; }
    }
}