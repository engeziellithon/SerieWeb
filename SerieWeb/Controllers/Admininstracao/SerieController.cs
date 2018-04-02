using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SerieWeb.Models.Identity;
using SerieWeb.Models;

namespace SerieWeb.Controllers.Admininstracao
{
    public class SerieController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Index
        // GET: Serie
        public ActionResult Index()
        {
            return View(db.Series.ToList());
        }
        #endregion

        #region Detalhes
        // GET: Serie/Details/5
        public ActionResult Detalhes(int? id)
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

        #region Adicionar serie 
        // GET: Serie/Adicionar
        public ActionResult Adicionar()
        {
            return View();
        }

        // POST: Serie/Adicionar
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar([Bind(Include = "SerieID,NomeSerie,Imagem,Sinopse,Nota")] Serie serie)
        {
            if (ModelState.IsValid)
            {
                db.Series.Add(serie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serie);
        }
        #endregion

        #region Editar serie 
        [HttpGet]
        // GET: Serie/Edit/5
        public ActionResult Editar(int? id)
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

        [HttpPost, ActionName("Editar")]
        [ValidateAntiForgeryToken]
        public ActionResult EditarConfirmar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var Serie = db.Series.Find(id);
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

            //    if (ModelState.IsValid)
            //{
            //    db.Entry(serie).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            return View(Serie);
        }
        #endregion

        #region Deletar Serie 
        // GET: Serie/Delete/5
        public ActionResult Deletar(int? id)
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

        // POST: Serie/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deletar(int id)
        {
            Serie serie = db.Series.Find(id);
            db.Series.Remove(serie);
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
