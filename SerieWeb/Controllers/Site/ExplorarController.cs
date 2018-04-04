using SerieWeb.Models;
using SerieWeb.Models.Identity;
using SerieWeb.Models.SerieViewModels;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Web.Mvc;

namespace SerieWeb.Controllers.Site
{
    public class ExplorarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Explorar
        public ActionResult Index()
        {
            var model = db.Series.ToList();
            return View(model);
        }

        #region Detalhes dos serie
        // GET: Serie/DetailsUsuario/5
        public ActionResult DetalhesSerie(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Serie serie = db.Series.Find(id);

            if (serie == null)
            {
                return HttpNotFound();
            }

            return View(serie);
        }
        #endregion

        #region Detalhes dos serie
        // GET: Serie/DetailsUsuario/5
        public ActionResult DetalhesSerieFavorito(int? id)
        {
            DetalhesSerieViewModel model = new DetalhesSerieViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Serie serie = db.Series.Find(id);     
            
            if(serie != null)
            {
                model.serie = db.Series.Where(s => s.SerieID == serie.SerieID);

                model.temporada = db.Temporadas.Where(t => t.SerieID == serie.SerieID);

                var EpisodioTemporadaId = db.Temporadas.Where(t => t.SerieID == serie.SerieID).Select(t => t.TemporadaID).FirstOrDefault();

                model.episodio = db.Episodios.Where(e => e.TemporadaID == EpisodioTemporadaId);
            }
            
            if (serie == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }
        #endregion
    }
}