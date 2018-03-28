using SerieWeb.Models.Identity;
using System.Linq;
using System.Web.Mvc;

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
    }
}