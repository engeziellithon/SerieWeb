using SerieWeb.Models.Identity;
using System.Linq;
using System.Web.Mvc;

namespace SerieWeb.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Pesquisa()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Pesquisa(string pesquisa)
        {
            return View(db.Series.Where(x => x.NomeSerie.Contains(pesquisa)).OrderBy(x => x.NomeSerie));
        }

    }
}