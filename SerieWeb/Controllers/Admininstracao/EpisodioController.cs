using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using SerieWeb.Models.Admininstracao;
using SerieWeb.Models.Identity;

namespace SerieWeb.Controllers.Admininstracao
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class EpisodioController : Controller
    {

        #region Banco
        private ApplicationDbContext db = new ApplicationDbContext();
        #endregion

        #region Index
        // GET: Episodio
        public async Task<ActionResult> Index()
        {
            var episodios = db.Episodios.Include(e => e.serie).Include(e => e.Temporada);
            
            return View(await episodios.ToListAsync());
        }
        #endregion

        #region Detalhes
        // GET: Episodio/Details/5
        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Episodio episodio = await db.Episodios.FindAsync(id);
            if (episodio == null)
            {
                return HttpNotFound();
            }
            return View(episodio);
        }
        #endregion

        #region Adicionar
        // GET: Episodio/Create
        public ActionResult Adicionar()
        {
            ViewBag.SerieID = new SelectList(db.Series, "SerieID", "NomeSerie");
            ViewBag.TemporadaID = new SelectList(db.Temporadas, "TemporadaID", "NomeTemporada");
            return View();
        }

        // POST: Episodio/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Adicionar([Bind(Include = "EpisodioID,NomeEpisodio,Sinopse,Video,DataExibicao,SerieID,TemporadaID")] Episodio episodio)
        {
            if (ModelState.IsValid)
            {
                db.Episodios.Add(episodio);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SerieID = new SelectList(db.Series, "SerieID", "NomeSerie", episodio.SerieID);
            ViewBag.TemporadaID = new SelectList(db.Temporadas, "TemporadaID", "NomeTemporada", episodio.TemporadaID);
            return View(episodio);
        }
        #endregion

        #region Editar
        // GET: Episodio/Edit/5
        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Episodio episodio = await db.Episodios.FindAsync(id);
            if (episodio == null)
            {
                return HttpNotFound();
            }
            ViewBag.SerieID = new SelectList(db.Series, "SerieID", "NomeSerie", episodio.SerieID);
            ViewBag.TemporadaID = new SelectList(db.Temporadas, "TemporadaID", "NomeTemporada", episodio.TemporadaID);
            return View(episodio);
        }

        // POST: Episodio/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar([Bind(Include = "EpisodioID,NomeEpisodio,Sinopse,Video,DataExibicao,SerieID,TemporadaID")] Episodio episodio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(episodio).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SerieID = new SelectList(db.Series, "SerieID", "NomeSerie", episodio.SerieID);
            ViewBag.TemporadaID = new SelectList(db.Temporadas, "TemporadaID", "NomeTemporada", episodio.TemporadaID);
            return View(episodio);
        }
        #endregion

        #region Deletar   
        // GET: Episodio/Delete/5
        public async Task<ActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Episodio episodio = await db.Episodios.FindAsync(id);
           
            if (episodio == null)
            {
                return HttpNotFound();
            }
            return View(episodio);
        }

        // POST: Episodio/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletarConfirmacao(int id)
        {
            Episodio episodio = await db.Episodios.FindAsync(id);
            db.Episodios.Remove(episodio);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
