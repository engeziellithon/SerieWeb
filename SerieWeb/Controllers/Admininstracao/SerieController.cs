using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SerieWeb.Models.Admininstracao;
using SerieWeb.Models.Identity;


namespace SerieWeb.Controllers.Admininstracao
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class SerieController : Controller
    {
        #region Banco de dados
        private ApplicationDbContext db = new ApplicationDbContext();
        #endregion

        #region Index

        public ActionResult Index(string valor = "")
        {
            ViewBag.MessagemSucesso = valor;
            return View(db.Series.ToList());
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
            ViewBag.Generos = db.Generos.ToList();
            ViewBag.Servicos = db.ServicosStreaming.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Adicionar([Bind(Include = "SerieID,NomeSerie,Imagem,Trailer,Sinopse,Nota,ListGeneros,ListServicos")] Serie serie)
        {
            if (ModelState.IsValid)
            {
                db.Series.Add(serie);
                await db.SaveChangesAsync();
                foreach (int item in serie.ListGeneros)
                {
                    var serieGeneros = new SeriesGeneros();
                    serieGeneros.SerieID = serie.SerieID;
                    serieGeneros.GeneroID = item;
                    db.SeriesGeneros.Add(serieGeneros);
                }
                foreach (int item in serie.ListServicos)
                {
                    var serieServicos = new SeriesServicos();
                    serieServicos.SerieID = serie.SerieID;
                    serieServicos.ServicoStreamingID = item;
                    db.SeriesServicos.Add(serieServicos);
                }
                await db.SaveChangesAsync();
                String valor = "A Serio " + serie.NomeSerie + " foi salva com sucesso";
                return RedirectToAction("Index","Serie",new { valor });
            }
            ViewBag.Generos = db.Generos.ToList();
            ViewBag.Servicos = db.ServicosStreaming.ToList();
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
            ViewBag.Generos = db.Generos.ToList();
            ViewBag.Servicos = db.ServicosStreaming.ToList();
            ViewBag.GeneroSelecionado = db.SeriesGeneros.Where(s => s.SerieID == serie.SerieID).Select(g => g.GeneroID).ToList();
            ViewBag.ServicoSelecionado = db.SeriesServicos.Where(s => s.SerieID == serie.SerieID).Select(g => g.ServicoStreamingID).ToList();

            return View(serie);
        }


        [HttpPost, ActionName("Editar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditarConfirmar(int? id, int?[] ListGeneros, int?[] ListServicos)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Serie = await db.Series.FindAsync(id);

            try
            {
                if (TryUpdateModel(Serie, "",
                  new string[] { "SerieID", "NomeSerie", "Imagem", "Sinopse", "Nota", "Trailer" }))
                {
                    try
                    {
                        var serieGeneros = db.SeriesGeneros.Where(s => s.SerieID == Serie.SerieID).ToList();
                        foreach (var item in serieGeneros)
                        {
                            db.SeriesGeneros.Remove(item);                            
                        }                        
                        if (ListGeneros != null)
                        {
                            foreach (int item in ListGeneros)
                            {
                                var serieGenero = new SeriesGeneros();
                                serieGenero.SerieID = Serie.SerieID;
                                serieGenero.GeneroID = item;
                                db.SeriesGeneros.Add(serieGenero);
                            }
                        }
                        var serieServicos = db.SeriesServicos.Where(s => s.SerieID == Serie.SerieID).ToList();
                        foreach (var item in serieServicos)
                        {
                            db.SeriesServicos.Remove(item);
                            await db.SaveChangesAsync();
                        }                        
                        if (ListServicos != null)
                        {
                            foreach (int item in ListServicos)
                            {
                                var serieServico = new SeriesGeneros();
                                serieServico.SerieID = Serie.SerieID;
                                serieServico.GeneroID = item;
                                db.SeriesGeneros.Add(serieServico);
                            }
                        }
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    catch (DataException)
                    {
                        ModelState.AddModelError("", "Não foi possível salvar as alterações.Volte para index é tente novamente,se o problema persistir, consulte o administrador do sistema.");
                    }
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Não foi possível salvar as alterações.Tente novamente se o problema persistir, consulte o administrador do sistema.");
            }

            ViewBag.Generos = db.Generos.ToList();
            ViewBag.Servicos = db.ServicosStreaming.ToList();
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
