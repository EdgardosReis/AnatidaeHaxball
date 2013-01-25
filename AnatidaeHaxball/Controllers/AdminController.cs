using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnatidaeHaxball.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Admin/Jogadores/

        public ActionResult Jogadores()
        {
            return View(AppServices.GetAllJogadores());
        }

        //
        // GET: /Admin/Create

        public ActionResult CreateJogador()
        {
            return View();
        }

        //
        // POST: /Admin/Create

        [HttpPost]
        public ActionResult CreateJogador(Jogador jogador)
        {
            try
            {
                AppServices.AddJogador(jogador);

                return RedirectToAction("Jogadores");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Edit/5

        public ActionResult EditJogador(int id)
        {
            return View(AppServices.GetJogador(id));
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public ActionResult EditJogador(int id, Jogador jogador)
        {
            try
            {
                AppServices.EditJogador(jogador);

                return RedirectToAction("Jogadores");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Delete/5

        public ActionResult DeleteJogador(int id)
        {
            return View(AppServices.GetJogador(id));
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost]
        public ActionResult DeleteJogador(int id, FormCollection collection)
        {
            try
            {
                AppServices.RemoveJogador(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
