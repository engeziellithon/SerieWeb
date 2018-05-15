using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SerieWeb.Models.Identity;


namespace SerieWeb.Controllers.Usuario
{
    [Authorize(Roles = "SuperAdmin,Admin,Usuario")]
    public class PerfilController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int? id)
        {
            int? ParcialMostrar = id;
            if(ParcialMostrar > 1)
            {
                ViewBag.ParcialMostrar = ParcialMostrar;
            }
            else
            {
                ViewBag.ParcialMostrar = 1;
            }
            return View();
        }

        

        public ActionResult Recomendado()
        {
            var user = User.Identity.GetUserId();
            var PerfilSerieId = db.UsuarioPerfil.Where(u => u.UserId == user).Select(s => s.SerieID).ToList();
            var serie = db.Series.Where(s => PerfilSerieId.Contains(s.SerieID)).ToList();
            return View();
        }
        
        public ActionResult Calendario()
        {
            return View();
        }
        public ActionResult Servico()
        {
            return View();
        }


    }
}