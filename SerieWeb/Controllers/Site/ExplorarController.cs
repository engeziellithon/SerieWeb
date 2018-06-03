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
        
        #region Detalhes dos serie
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

            ViewBag.Indicacoes = db.Series.Take(3).ToList();
            ViewBag.listatemporada = db.Episodios.Where(s => s.SerieID == serie.SerieID).Select(t => t.Temporada).ToList().Distinct();
            ViewBag.listagenero = db.SeriesGeneros.Where(g => g.SerieID == serie.SerieID).Select(s=>s.Genero).ToList();
            ViewBag.listaservico = db.SeriesServicos.Where(g => g.SerieID == serie.SerieID).Select(s => s.ServicoStreaming).ToList();
            ViewBag.Avaliacoes = db.UsuarioPerfil.Where(s => s.SerieID == serie.SerieID && s.Avaliacao >= 0).ToList();

            if (user != null)
            {
                int nota = db.UsuarioPerfil.Where(s => s.UserId == user && s.SerieID == serie.SerieID && s.Avaliacao >= 0).Select(a=>a.Avaliacao).FirstOrDefault();
                ViewBag.UserNota = nota;
            }           

            return View(serie);
        }
        #endregion

        #region listar episodio da serue
        [HttpPost]
        public ActionResult ExiberEpisodio(int SerieID,int TemporadaID)
        {

            var listaEpisodios = db.Episodios.Where(s => s.SerieID == SerieID && s.TemporadaID == TemporadaID).Select(s => new
            {
                NomeEpisodio = s.NomeEpisodio,
                DataEpisodio = s.DataExibicao.Day + "/" + s.DataExibicao.Month + "/" + s.DataExibicao.Year
            }).ToList();

           
            return Json(new { listaEpisodio = listaEpisodios });
        }
        #endregion

        #region Salvar seie nos favoritos 
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
                    return RedirectToAction("DetalhesSerie", "Explorar", new { id = serie.SerieID });
                }
                catch (DataException)
                { 
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
                    
                    return RedirectToAction("DetalhesSerie", "Explorar", new { id = serie.SerieID });
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

        #region Avaliacao da serie 
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

                    return RedirectToAction("DetalhesSerie", "Explorar", new { id = serie.SerieID });
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