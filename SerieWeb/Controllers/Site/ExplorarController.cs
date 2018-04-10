using Microsoft.AspNet.Identity;
using SerieWeb.Models;
using SerieWeb.Models.Identity;
using SerieWeb.Models.SerieViewModels;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Mvc.Razor;

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
        [HttpGet]
        public ActionResult DetalhesSerieFavorito(int? id)
        {
            DetalhesSerieViewModel model = new DetalhesSerieViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Serie serie = db.Series.Find(id);

            if (serie == null)
            {
                return HttpNotFound();
            }
            //Passar dados para view

            model.serie = db.Series.Where(s => s.SerieID == serie.SerieID);

            model.temporada = db.Temporadas.Where(t => t.SerieID == serie.SerieID);

            model.episodio = db.Episodios.Where(e => e.Temporada.Serie.SerieID == serie.SerieID);

            return View(model);
        }
        #endregion

        [HttpPost]
        public ActionResult SalvarFavorito(int IdSerie)
        {
            #region verifica se esta logando / Verifica id do usuario logado / verifica se a serie existe.

            string user = User.Identity.GetUserId();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            Serie serie = db.Series.Find(IdSerie);

            if (serie == null)
            {
                return HttpNotFound();
            }
            #endregion

            UsuarioPerfil perfil = new UsuarioPerfil();
            var StatusSerieFavorita = db.UsuarioPerfil.Where(p => p.SerieID == serie.SerieID && p.UserId == user).FirstOrDefault();

            #region Adicionar aos favoritos
            if (StatusSerieFavorita == null)
            {
                perfil.UserId = user;
                perfil.SerieID = serie.SerieID;
                perfil.SerieFavorita = true;
                db.UsuarioPerfil.Add(perfil);
                db.SaveChanges();
            }
            #endregion

            #region Edição da Serie favorita 
            else if (StatusSerieFavorita.SerieFavorita == false)
            {
                try
                {
                    perfil = db.UsuarioPerfil.Find(StatusSerieFavorita.UsuarioPerfilID);
                    perfil.SerieFavorita = true;
                    db.Entry(perfil).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("DetalhesSerieFavorito", "Explorar", new { id = serie.SerieID });
                }
                catch (Exception ex)
                {
                    var teste = ex;
                    ModelState.AddModelError("", "Não foi possível salvar as alterações.Tente novamente se o problema persistir, consulte o administrador do sistema.");
                }
            }
            else
            {
                try
                {
                    perfil = db.UsuarioPerfil.Find(StatusSerieFavorita.UsuarioPerfilID);
                    perfil.SerieFavorita = false;
                    db.Entry(perfil).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("DetalhesSerieFavorito", "Explorar", new { id = serie.SerieID });
                }
                catch (Exception ex)
                {
                    var teste = ex;
                    ModelState.AddModelError("", "Não foi possível salvar as alterações.Tente novamente se o problema persistir, consulte o administrador do sistema.");
                }
            }
            #endregion

            return RedirectToAction("DetalhesSerieFavorito", "Explorar", new { id = serie.SerieID });
        }

        [HttpPost]
        public ActionResult InteresseSerie (int IdSerie)
        {
            #region verifica se esta logando / Pega id do usuario logado / verifica se a serie existe.

            string user = User.Identity.GetUserId();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            Serie serie = db.Series.Find(IdSerie);

            if (serie == null)
            {
                return HttpNotFound();
            }
            #endregion

            UsuarioPerfil perfil = new UsuarioPerfil();
            var StatusSerie = db.UsuarioPerfil.Where(p => p.SerieID == serie.SerieID && p.UserId == user).FirstOrDefault();

            #region Adicionar aos favoritos
            if (StatusSerie == null)
            {
                perfil.UserId = user;
                perfil.SerieID = serie.SerieID;
                perfil.InteresseSerie = true;
                db.UsuarioPerfil.Add(perfil);
                db.SaveChanges();
            }
            #endregion

            #region Edição da Serie favorita 
            else if (StatusSerie.SerieFavorita == false)
            {
                try
                {
                    perfil = db.UsuarioPerfil.Find(StatusSerie.UsuarioPerfilID);
                    perfil.InteresseSerie = true;
                    db.Entry(perfil).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("DetalhesSerieFavorito", "Explorar", new { id = serie.SerieID });
                }
                catch (Exception ex)
                {
                    var teste = ex;
                    ModelState.AddModelError("", "Não foi possível salvar as alterações.Tente novamente se o problema persistir, consulte o administrador do sistema.");
                }
            }
            else
            {
                try
                {
                    perfil = db.UsuarioPerfil.Find(StatusSerie.UsuarioPerfilID);
                    perfil.InteresseSerie = false;
                    db.Entry(perfil).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("DetalhesSerieFavorito", "Explorar", new { id = serie.SerieID });
                }
                catch (Exception ex)
                {
                    var teste = ex;
                    ModelState.AddModelError("", "Não foi possível salvar as alterações.Tente novamente se o problema persistir, consulte o administrador do sistema.");
                }
            }
            #endregion

            return RedirectToAction("DetalhesSerieFavorito", "Explorar", new { id = serie.SerieID });
        }
    }
}