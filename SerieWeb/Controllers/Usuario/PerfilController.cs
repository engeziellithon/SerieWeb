using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SerieWeb.Models.Admininstracao;
using SerieWeb.Models.Identity;


namespace SerieWeb.Controllers.Usuario
{
    public class PerfilController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Perfil

        [Authorize]
        public ActionResult Index()
        {
           
            var user = User.Identity.GetUserId();
            
            var PerfilSerieId = db.UsuarioPerfil.Where(u => u.UserId == user && u.SerieFavorita == true).Select(s => s.SerieID).ToList();
           // model.serie = db.Series.Where(s => PerfilSerieId.Contains(s.SerieID)).ToList();
            
            return View();
        }

        public ActionResult Calendario()
        {
           

            return View();
        }
    }
}