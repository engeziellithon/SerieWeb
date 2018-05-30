using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SerieWeb.Models.Admininstracao;
using SerieWeb.Models.Identity;


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
            //List<Serie> ListaSerie = new List<Serie>();

            //string user = User.Identity.GetUserId();

            //dd.

            //List<Serie> PerfilSerieFavorito = db.UsuarioPerfil.Where(u => u.UserId == user && u.SerieFavorita == true).Select(s => s.Serie).ToList();
            //foreach (var serie in PerfilSerieFavorito)
            //{
            //    ListaSerie.Add(serie);
            //}

            return View();
        }
        
        public ActionResult Calendario()
        {
            /// tirar isso não vai ficar bom para ficar vai ser dificil 
            return View();
        }
        public ActionResult Servico()
        {
            List<Serie> ListaSerie = new List<Serie>();

            List<SeriesServicos> ListaServicos = new List<SeriesServicos>();

            string user = User.Identity.GetUserId();

            List<Serie> PerfilSerieFavorito = db.UsuarioPerfil.Where(u => u.UserId == user && u.SerieFavorita == true).Select(s => s.Serie).ToList();
            foreach (var serie in PerfilSerieFavorito)
            {
                ListaSerie.Add(serie);
            }
            foreach (var item in ListaSerie)
            {
                List<SeriesServicos> listaFilhos = db.SeriesServicos.Where(c => c.SerieID == item.SerieID).ToList();
                foreach (SeriesServicos filho in listaFilhos)
                {
                    ListaServicos.Add(filho);                   
                }
            }
            


            return View(ListaServicos);
        }

        public List<SeriesServicos> ChildrenOf(int SerieID)
        {
            List<SeriesServicos> ListaServicos = new List<SeriesServicos>();
            AddChildren(SerieID, ListaServicos);

            return ListaServicos;
        }

        private void AddChildren(int SerieID, List<SeriesServicos> ListaServicos)
        {
            
        }


    }
}