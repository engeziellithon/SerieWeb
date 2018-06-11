using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SerieWeb.Models.Admininstracao;
using SerieWeb.Models.Usuario;
using SerieWeb.Models.Identity;
using System.Data;
using System.Data.Entity;

namespace SerieWeb.Controllers.Usuario
{
    [Authorize(Roles = "SuperAdmin,Admin,Usuario")]
    public class PerfilController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            List<Serie> ListaSerie = new List<Serie>();

            string user = User.Identity.GetUserId();

            List<Serie> PerfilSerieFavorito = db.UsuarioPerfil.Where(u => u.UserId == user && u.SerieFavorita == true).Select(s => s.Serie).ToList();
            foreach (var serie in PerfilSerieFavorito)
            {
                ListaSerie.Add(serie);
            }

            return View(ListaSerie);
        }


        public ActionResult Recomendado()
        {
            List<Serie> ListaSerie = new List<Serie>();            

            ListaSerie = db.UsuarioPerfil.OrderBy(s => s.Avaliacao).Select(s=>s.Serie).Take(10).ToList();

            ViewBag.Series = ListaSerie;

            return View(ListaSerie);
        }

      
        public ActionResult Servico()
        {
            List<Serie> ListaSerie = new List<Serie>();

            List<SeriesServicos> ListaServicos = new List<SeriesServicos>();

            string user = User.Identity.GetUserId();

            List<Serie> PerfilSerie = db.UsuarioPerfil.Where(u => u.UserId == user && u.SerieFavorita == true).Select(s => s.Serie).ToList();
            if (PerfilSerie != null)
            {
                foreach (var item in PerfilSerie)
                {
                    List<SeriesServicos> listaFilhos = db.SeriesServicos.Where(c => c.SerieID == item.SerieID).ToList();
                    foreach (SeriesServicos filho in listaFilhos)
                    {
                        ListaServicos.Add(filho);
                    }
                }
            }

            ViewBag.HBO = ListaServicos.Where(S => S.ServicoStreaming.NomeServicoStreaming.ToUpper() == "HBO").ToList();
            ViewBag.NETFLIX = ListaServicos.Where(S => S.ServicoStreaming.NomeServicoStreaming.ToUpper() == "NETFLIX").ToList();
            ViewBag.PRIMEVIDEO = ListaServicos.Where(S => S.ServicoStreaming.NomeServicoStreaming.ToUpper() == "PRIME VIDEO").ToList();
            ViewBag.FOXPLAY = ListaServicos.Where(S => S.ServicoStreaming.NomeServicoStreaming.ToUpper() == "FOX PLAY").ToList();
            ViewBag.SONY = ListaServicos.Where(S => S.ServicoStreaming.NomeServicoStreaming.ToUpper() == "SONY").ToList();
           
            var maior  = ListaServicos.Select(s => s.ServicoStreaming).Max(c => c.NomeServicoStreaming);

            ServicoStreaming MelhorServico = db.ServicosStreaming.Where(s => s.NomeServicoStreaming == maior).FirstOrDefault();
            ViewBag.MelhorServico = MelhorServico;

            return View(ListaServicos);
        }


        #region Avaliacao da serie 
        [HttpPost]
        public ActionResult ExcluirFavorito(int FavoritoExcluido)
        {
            #region verifica se esta logando / Verifica id do usuario logado / verifica se a serie existe.

            string user = User.Identity.GetUserId();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            Serie serie = db.Series.Find(FavoritoExcluido);

            if (serie == null)
            {
                return HttpNotFound();
            }
            #endregion

            UsuarioPerfil perfil = new UsuarioPerfil();
            var SerieFavoritoExcluido = db.UsuarioPerfil.Where(p => p.SerieID == serie.SerieID && p.UserId == user).FirstOrDefault();

            #region Edição do favorito       
            if (SerieFavoritoExcluido != null)
            {
                try
                {
                    perfil = db.UsuarioPerfil.Find(SerieFavoritoExcluido.UsuarioPerfilID);
                    perfil.SerieFavorita = false;
                    db.Entry(perfil).State = EntityState.Modified;
                    db.SaveChanges();

                    return Json(new { perfil = perfil });
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Não foi possível salvar as alterações.Tente novamente se o problema persistir, consulte o administrador do sistema.");
                }
            }
            #endregion

            return RedirectToAction("Index", "Perfil");
        }
        #endregion





    }
}