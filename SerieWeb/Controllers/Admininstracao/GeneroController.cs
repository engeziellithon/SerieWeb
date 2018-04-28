using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using SerieWeb.Models.Admininstracao;
using SerieWeb.Models.Identity;

namespace SerieWeb.Controllers.Admininstracao
{
    public class GeneroController : Controller
    {

        #region Banco
        private ApplicationDbContext db = new ApplicationDbContext();
        #endregion

        #region Index
        // GET: Genero
        public async Task<ActionResult> Index()
        {
            return View(await db.Generos.ToListAsync());
        }
        #endregion

        #region Detalhes
        // GET: Genero/Details/5
        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genero genero = await db.Generos.FindAsync(id);
            if (genero == null)
            {
                return HttpNotFound();
            }
            return View(genero);
        }
        #endregion

        #region Adicionar
        // GET: Genero/Create
        public ActionResult Adicionar()
        {
            return View();
        }

        // POST: Genero/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Adicionar([Bind(Include = "GeneroID,NomeGenero")] Genero genero)
        {
            if (ModelState.IsValid)
            {
                db.Generos.Add(genero);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(genero);
        }
        #endregion

        #region Editar
        // GET: Genero/Edit/5
        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genero genero = await db.Generos.FindAsync(id);
            if (genero == null)
            {
                return HttpNotFound();
            }
            return View(genero);
        }

        // POST: Genero/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar([Bind(Include = "GeneroID,NomeGenero")] Genero genero)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genero).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(genero);
        }
        #endregion

        #region Deletar
        // GET: Genero/Delete/5
        public async Task<ActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genero genero = await db.Generos.FindAsync(id);
            if (genero == null)
            {
                return HttpNotFound();
            }
            return View(genero);
        }

        // POST: Genero/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletarConfirmacao(int id)
        {
            Genero genero = await db.Generos.FindAsync(id);
            db.Generos.Remove(genero);
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
