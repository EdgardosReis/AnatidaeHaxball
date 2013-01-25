using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnatidaeHaxball.Controllers
{
    public class ApresentacaoController : Controller
    {
        //
        // GET: /Apresentacao/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Apresentacao/Contactos

        public ActionResult Contactos()
        {
            return View();
        }

        //
        // GET: /Apresentacao/Equipamentos

        public ActionResult Equipamentos()
        {
            return View();
        }

       
    }
}
