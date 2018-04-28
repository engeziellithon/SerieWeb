using System;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SerieWeb.Models.Admininstracao;
using SerieWeb.Models.Identity;

namespace SerieWeb.Controllers.Admininstracao
{
    public class SerieController : Controller
    {
        #region Banco de dados
        private ApplicationDbContext db = new ApplicationDbContext();
        #endregion

        #region Index

        public async Task<ActionResult> Index()
        {
            return View(await db.Series.ToListAsync());
        }
        #endregion

        #region Detalhes
        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Serie serie = await db.Series.FindAsync(id);
            if (serie == null)
            {
                return HttpNotFound();
            }
            return View(serie);
        }
        #endregion

        #region Adicionar Série
        [HttpGet]
        public ActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Adicionar([Bind(Include = "SerieID,NomeSerie,Imagem,Trailer,Sinopse,Nota")] Serie serie)
        {
            if (ModelState.IsValid)
            {
                db.Series.Add(serie);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(serie);
        }

        #endregion

        #region Editar Série
        
        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Serie serie = await db.Series.FindAsync(id);
            if (serie == null)
            {
                return HttpNotFound();
            }
            return View(serie);
        }

        
        [HttpPost, ActionName("Editar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditarConfirmar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var Serie = await db.Series.FindAsync(id);
            try
            {
                if (TryUpdateModel(Serie, "",
                  new string[] { "SerieID", "NomeSerie", "Imagem", "Sinopse", "Nota" }))
                {
                    try
                    {
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    catch (DataException)
                    {
                        //Log the error (uncomment dex variable name and add a line here to write a log.
                        ModelState.AddModelError("", "Não foi possível salvar as alterações.Volte para index é tente novamente,se o problema persistir, consulte o administrador do sistema.");
                    }
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Não foi possível salvar as alterações.Tente novamente se o problema persistir, consulte o administrador do sistema.");
            }


            return View(Serie);
        }
        #endregion

        #region Deletar Serie
        public async Task<ActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Serie serie = await db.Series.FindAsync(id);
            if (serie == null)
            {
                return HttpNotFound();
            }
            return View(serie);
        }

        // POST: Serie/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletarConfirmacao(int id)
        {
            Serie serie = await db.Series.FindAsync(id);
            db.Series.Remove(serie);
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
