using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace SerieWeb.Controllers.Usuario
{
    public class PerfilController : Controller
    {
        // GET: Perfil
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            return View();
        }
    }
}