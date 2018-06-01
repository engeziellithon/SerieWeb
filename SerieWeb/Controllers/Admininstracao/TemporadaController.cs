using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using SerieWeb.Models.Admininstracao;
using SerieWeb.Models.Identity;

namespace SerieWeb.Controllers.Admininstracao
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class TemporadaController : Controller
    {
        #region Banco
        private ApplicationDbContext db = new ApplicationDbContext();
        #endregion

        #region Index
        public async Task<ActionResult> Index()
        {
            return View(await db.Temporadas.ToListAsync());
        }
        #endregion

        #region Detalhes
        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temporada temporada = await db.Temporadas.FindAsync(id);
            if (temporada == null)
            {
                return HttpNotFound();
            }
            return View(temporada);
        }
        #endregion

        #region Adicionar
        public ActionResult Adicionar()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Adicionar([Bind(Include = "TemporadaID,NomeTemporada")] Temporada temporada)
        {
            if (ModelState.IsValid)
            {
                db.Temporadas.Add(temporada);
                await db.SaveChangesAsync();
                Session["MensagemSucesso"] = "A temporada " + temporada.NomeTemporada + " foi salva com sucesso.";
                return RedirectToAction("Index");
            }

            return View(temporada);
        }
        #endregion

        #region Editar
        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temporada temporada = await db.Temporadas.FindAsync(id);
            if (temporada == null)
            {
                return HttpNotFound();
            }
            return View(temporada);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar([Bind(Include = "TemporadaID,NomeTemporada")] Temporada temporada)
        {
            if (ModelState.IsValid)
            {
                db.Entry(temporada).State = EntityState.Modified;
                await db.SaveChangesAsync();
                Session["MensagemSucesso"] = "A temporada " + temporada.NomeTemporada + " foi alterada com sucesso.";
                return RedirectToAction("Index");
            }
            return View(temporada);
        }
        #endregion

        #region Deletar
        // GET: Temporada/Delete/5
        public async Task<ActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temporada temporada = await db.Temporadas.FindAsync(id);
            if (temporada == null)
            {
                return HttpNotFound();
            }
            return View(temporada);
        }

        // POST: Temporada/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletarConfirmacao(int id)
        {
            Temporada temporada = await db.Temporadas.FindAsync(id);
            db.Temporadas.Remove(temporada);
            await db.SaveChangesAsync();
            Session["MensagemSucesso"] = "A temporada " + temporada.NomeTemporada + " foi deletada com sucesso.";
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
