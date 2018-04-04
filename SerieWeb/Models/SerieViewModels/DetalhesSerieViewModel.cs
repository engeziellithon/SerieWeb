using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerieWeb.Models.SerieViewModels
{
    public class DetalhesSerieViewModel
    {
        public IEnumerable<Serie> serie { get; set; }

        public IEnumerable<Temporada> temporada { get; set; }

        public IEnumerable<Episodio> episodio { get; set; }
    }
}