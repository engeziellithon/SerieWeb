using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SerieWeb.Models.Identity;
using SerieWeb.Models;

namespace SerieWeb.Controllers.Admininstracao
{
    public class EpisodioController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Episodio
        public ActionResult Index()
        {
            ViewBag.listaSeries = db.Series.ToList();
            var episodios = db.Episodios.Include(e => e.Temporada);
            return View(episodios.ToList());
        }
        
        // GET: Episodio/Details/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Episodio episodio = db.Episodios.Find(id);
            if (episodio == null)
            {
                return HttpNotFound();
            }
            return View(episodio);
        }


        public List<Serie> GetSeriesList()
        {
            List<Serie> series = db.Series.ToList();
            return series;
        }
        public ActionResult GetTemporadasList(int SerieID)
        {
            List<Temporada> TemporadasList = db.Temporadas.Where(x => x.SerieID == SerieID).ToList();

            ViewBag.TemporadaOpcao = new SelectList(TemporadasList, "TemporadaID", "NomeTemporada");

            return PartialView("TemporadaOpcaoParcial");
        }

        // GET: Episodio/Create
        public ActionResult Adicionar()
        {
            ViewBag.listaSeries = new SelectList(GetSeriesList(), "SerieID", "NomeSerie");
            //ViewBag.TemporadaID = new SelectList(db.Temporadas, "TemporadaID", "NomeTemporada");
            return View();
        }

        // POST: Episodio/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar([Bind(Include = "EpisodioID,NomeEpisodio,Sinopse,Video,DataExibicao,TemporadaID")] Episodio episodio)
        {
            if (ModelState.IsValid)
            {
                db.Episodios.Add(episodio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.TemporadaID = new SelectList(db.Temporadas, "TemporadaID", "NomeTemporada", episodio.TemporadaID);
            return View(episodio);
        }

        // GET: Episodio/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Episodio episodio = db.Episodios.Find(id);
            if (episodio == null)
            {
                return HttpNotFound();
            }

            var ListaSeriesdoEpisodio = db.Temporadas.Where(x => x.TemporadaID == episodio.TemporadaID).First();
            int SeriesdoEpisodio = ListaSeriesdoEpisodio.SerieID;
            ViewBag.listaSeries = new SelectList(GetSeriesList(), "SerieID", "NomeSerie");
            ViewData["TemporadaID"] = new SelectList(db.Temporadas.Where(x => x.SerieID == SeriesdoEpisodio), "TemporadaID", "NomeTemporada", episodio.TemporadaID);
            //ViewBag.TemporadaID = new SelectList(db.Temporadas, "TemporadaID", "NomeTemporada", episodio.TemporadaID);
            return View(episodio);
        }

        // POST: Episodio/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "EpisodioID,NomeEpisodio,Sinopse,Video,DataExibicao,TemporadaID")] Episodio episodio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(episodio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TemporadaID = new SelectList(db.Temporadas, "TemporadaID", "NomeTemporada", episodio.TemporadaID);
            return View(episodio);
        }

        // GET: Episodio/Delete/5
        public ActionResult Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Episodio episodio = db.Episodios.Find(id);
            if (episodio == null)
            {
                return HttpNotFound();
            }
            return View(episodio);
        }

        // POST: Episodio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Deletar(int id)
        {
            Episodio episodio = db.Episodios.Find(id);
            db.Episodios.Remove(episodio);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
