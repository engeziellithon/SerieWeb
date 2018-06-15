using Microsoft.AspNet.Identity;
using SerieWeb.Models.Admininstracao;
using SerieWeb.Models.Identity;
using SerieWeb.Models.Usuario;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SerieWeb.Controllers.Site
{
    public class ExplorarController : Controller
    {

        #region Banco
        private ApplicationDbContext db = new ApplicationDbContext();
        #endregion

        #region Explorar
        // GET: Explorar
        public ActionResult Index(string pesquisa)
        {
            List<Serie> model = new List<Serie>();
            if (pesquisa != null)
            {
                model = db.Series.Where(x => x.NomeSerie.Contains(pesquisa)).Take(20).ToList();
            }
            else
            {
                model = db.Series.ToList();
            }

            return View(model);
        }
        #endregion

        #region Detalhes das séries
        // GET: Serie/DetailsUsuario/5
        [HttpGet]
        public ActionResult DetalhesSerie(int? id, string MensagemSucesso)
        {
            #region Verificar se serie existe
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Serie serie = db.Series.Find(id);


            if (serie == null)
            {
                return HttpNotFound();
            }
            #endregion

            if (serie.Trailer != null)
            {
                ViewBag.TrailerCompartilhar = "";
                string trailer = serie.Trailer;
                bool compartilharYoutube = Regex.IsMatch(serie.Trailer, @"^(https:\/\/)youtu(.be)?(\.com)?\/.+");

                if (compartilharYoutube == true)
                {
                    var TrailerCompartilhar = trailer.Substring(trailer.Length - 11);
                    ViewBag.TrailerCompartilhar = TrailerCompartilhar;
                }
            }
            // user logado
            string user = User.Identity.GetUserId();


            ViewBag.listatemporada = db.Episodios.Where(s => s.SerieID == serie.SerieID).Select(t => t.Temporada).ToList().Distinct();
            ViewBag.listaservico = db.SeriesServicos.Where(g => g.SerieID == serie.SerieID).Select(s => s.ServicoStreaming).ToList();
            ViewBag.Avaliacoes = db.UsuarioPerfil.Where(s => s.SerieID == serie.SerieID && s.Avaliacao > 0).ToList();

            var ListaGenero = db.SeriesGeneros.Where(g => g.SerieID == serie.SerieID).Select(s => s.Genero).ToList();
            ViewBag.listagenero = ListaGenero;

            

            var ListaGeneroSerie = ListaGenero.Select(f => f.GeneroID).ToList();
            var lista = db.SeriesGeneros.Where(d => ListaGeneroSerie.Contains(d.GeneroID) && d.Serie.SerieID != serie.SerieID).Select(d => d.Serie).ToList();

            if (lista.Count() == 0)
            {
                ViewBag.Indicacoes = db.Series.Where(d => d.SerieID != serie.SerieID).Take(3).ToList();
            }
            else if (lista.Count() <= 3)
            {
                ViewBag.Indicacoes = lista.Where(s => s.SerieID != serie.SerieID).Distinct().ToList();
            }
            else
            {
                ViewBag.Indicacoes = lista.Where(s => s.SerieID != serie.SerieID).Take(3).Distinct().ToList();
            }


            if (user != null)
            {
                int nota = db.UsuarioPerfil.Where(s => s.UserId == user && s.SerieID == serie.SerieID && s.Avaliacao > 0).Select(a => a.Avaliacao).FirstOrDefault();
                int SerieFavorita = db.UsuarioPerfil.Where(s => s.SerieID == serie.SerieID && s.SerieFavorita == true && s.UserId == user).Select(s => s.SerieFavorita).FirstOrDefault() == true ? 1 : 0;
                ViewBag.SerieFavorita = SerieFavorita;
                ViewBag.UserNota = nota;
            }


            return View(serie);
        }
        #endregion

        #region listar episódio da série
        [HttpPost]
        public ActionResult ExiberEpisodio(int SerieID, int TemporadaID)
        {

            var listaEpisodios = db.Episodios.Where(s => s.SerieID == SerieID && s.TemporadaID == TemporadaID).Select(s => new
            {
                NomeEpisodio = s.NomeEpisodio,
                DataEpisodio = s.DataExibicao.Day + "/" + s.DataExibicao.Month + "/" + s.DataExibicao.Year
            }).ToList();


            return Json(new { listaEpisodio = listaEpisodios });
        }
        #endregion

        #region Salvar série nos favoritos 
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

            var Seriefavorito = serie.NomeSerie;

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

                    return Json(new { Seriefavorito = Seriefavorito });
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Não foi possível salvar as alterações.Tente novamente se o problema persistir, consulte o administrador do sistema.");
                }
            }
           
            #endregion
           

            return Json(new { Seriefavorito = Seriefavorito });
        }
        #endregion

        #region Avaliação da série 
        [HttpPost]
        public ActionResult AvaliarSerie(int avaliacao, int SerieID)
        {
            #region verifica se esta logando / Verifica id do usuario logado / verifica se a serie existe.

            string user = User.Identity.GetUserId();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            Serie serie = db.Series.Find(SerieID);

            if (serie == null)
            {
                return HttpNotFound();
            }
            #endregion

            UsuarioPerfil perfil = new UsuarioPerfil();
            var SerieAvaliadaUsuario = db.UsuarioPerfil.Where(p => p.SerieID == serie.SerieID && p.UserId == user).FirstOrDefault();

            #region Adicionar Avaliacao da Serie
            if (SerieAvaliadaUsuario == null)
            {
                perfil.UserId = user;
                perfil.SerieID = serie.SerieID;
                perfil.Avaliacao = avaliacao;
                db.UsuarioPerfil.Add(perfil);
                db.SaveChanges();

                var serieAvaliacao = perfil.Avaliacao;

                return Json(new { serieAvaliacao = serieAvaliacao });
            }
            #endregion

            #region Edição da Avaliacao Serie         
            else
            {
                try
                {
                    perfil = db.UsuarioPerfil.Find(SerieAvaliadaUsuario.UsuarioPerfilID);
                    perfil.Avaliacao = avaliacao;
                    db.Entry(perfil).State = EntityState.Modified;
                    db.SaveChanges();

                    var serieAvaliacao = perfil.Avaliacao;

                    return Json(new { serieAvaliacao = serieAvaliacao });
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Não foi possível salvar as alterações.Tente novamente se o problema persistir, consulte o administrador do sistema.");
                }
            }
            #endregion


            return RedirectToAction("DetalhesSerie", "Explorar", new { id = serie.SerieID });
        }
        #endregion

    }
}