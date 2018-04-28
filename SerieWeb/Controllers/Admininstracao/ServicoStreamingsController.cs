using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using SerieWeb.Models.Admininstracao;
using SerieWeb.Models.Identity;

namespace SerieWeb.Controllers.Admininstracao
{
    public class ServicoStreamingsController : Controller
    {

        #region Banco
        private ApplicationDbContext db = new ApplicationDbContext();
        #endregion

        #region Index
        // GET: ServicoStreamings
        public async Task<ActionResult> Index()
        {
            return View(await db.ServicosStreaming.ToListAsync());
        }
        #endregion

        #region Detalhes
        // GET: ServicoStreamings/Details/5
        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicoStreaming servicoStreaming = await db.ServicosStreaming.FindAsync(id);
            if (servicoStreaming == null)
            {
                return HttpNotFound();
            }
            return View(servicoStreaming);
        }
        #endregion

        #region Adicionar
        // GET: ServicoStreamings/Create
        public ActionResult Adicionar()
        {
            return View();
        }

        // POST: ServicoStreamings/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Adicionar([Bind(Include = "ServicoStreamingID,NomeServicoStreaming")] ServicoStreaming servicoStreaming)
        {
            if (ModelState.IsValid)
            {
                db.ServicosStreaming.Add(servicoStreaming);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(servicoStreaming);
        }
        #endregion

        #region Editar
        // GET: ServicoStreamings/Edit/5
        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicoStreaming servicoStreaming = await db.ServicosStreaming.FindAsync(id);
            if (servicoStreaming == null)
            {
                return HttpNotFound();
            }
            return View(servicoStreaming);
        }

        // POST: ServicoStreamings/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar([Bind(Include = "ServicoStreamingID,NomeServicoStreaming")] ServicoStreaming servicoStreaming)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servicoStreaming).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(servicoStreaming);
        }

        #endregion

        #region Deletar
        // GET: ServicoStreamings/Delete/5
        public async Task<ActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicoStreaming servicoStreaming = await db.ServicosStreaming.FindAsync(id);
            if (servicoStreaming == null)
            {
                return HttpNotFound();
            }
            return View(servicoStreaming);
        }

        // POST: ServicoStreamings/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletarConfirmacao(int id)
        {
            ServicoStreaming servicoStreaming = await db.ServicosStreaming.FindAsync(id);
            db.ServicosStreaming.Remove(servicoStreaming);
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
