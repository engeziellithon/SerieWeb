using SerieWeb.Models;
using SerieWeb.Models.Identity;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace SerieWeb.Controllers.Admininstracao
{
    public class TemporadaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region index
        // GET: Temporada
        public ActionResult Index()
        {
            var temporada = db.Temporadas.Include(t => t.Serie);
            return View(temporada);
        }
        #endregion

        #region Detalhes
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temporada temporada = db.Temporadas.Find(id);

            if (temporada == null)
            {
                return HttpNotFound();
            }

            return View(temporada);
        }
        #endregion

        #region Adicionar Temporada 
        // GET: Temporada/Adicionar
        public ActionResult Adicionar()
        {
            ViewData["SerieID"] = new SelectList(db.Series, "SerieID", "NomeSerie");
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar([Bind(Include = "TemporadaID, NomeTemporada, SerieID")] Temporada temporada)
        {
            if (ModelState.IsValid)
            {
                db.Temporadas.Add(temporada);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["SerieID"] = new SelectList(db.Series, "SerieID", "NomeSerie", temporada.SerieID);
            return View(temporada);
        }
        #endregion

        #region Editar temporada 
        [HttpGet]
        // GET: Serie/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temporada temporada = db.Temporadas.Find(id);
            if (temporada == null)
            {
                return HttpNotFound();
            }
            ViewData["SerieID"] = new SelectList(db.Series, "SerieID", "NomeSerie", temporada.SerieID);
            return View(temporada);
        }

        [HttpPost, ActionName("Editar")]
        [ValidateAntiForgeryToken]
        public ActionResult EditarConfirmar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var temporada = db.Temporadas.Find(id);
            try
            {
                if (TryUpdateModel(temporada, "",
                  new string[] { "TemporadaID", "NomeTemporada", "SerieID" }))
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
            return View();
        }
        #endregion

        #region Deletar Temporada 
        // GET: Serie/Deletar/5
        public ActionResult Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temporada temporada = db.Temporadas.Include(s => s.Serie).First(t => t.TemporadaID == id);

            if (temporada == null)
            {
                return HttpNotFound();
            }
            return View(temporada);
        }

        // POST: Serie/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deletar(int id)
        {
            Temporada temporada = db.Temporadas.Find(id);
            db.Temporadas.Remove(temporada);
            db.SaveChanges();
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