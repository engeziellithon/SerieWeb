using System.Web.Mvc;

namespace SerieWeb.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {    
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
       
    }
}