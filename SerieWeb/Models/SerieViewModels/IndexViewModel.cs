using SeriesWeb.Models;
using System.Collections.Generic;

namespace SerieWeb.Models.SerieViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Serie> BannerDestaque { get; set; }

        public IEnumerable<Serie> SerieStreming { get; set; }

        public IEnumerable<Genero> Generos { get; set; }
    }
}
