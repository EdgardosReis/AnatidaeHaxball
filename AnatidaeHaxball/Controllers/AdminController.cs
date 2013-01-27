﻿using AnatidaeHaxball.Models;
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
            return View(AppServices.GetAllJogadoresDaEquipa());
        }

        //
        // GET: /Admin/Create

        public ActionResult CreateJogador()
        {
            return View(new JogadorModels());
        }

        //
        // POST: /Admin/Create

        [HttpPost]
        public ActionResult CreateJogador(Jogador jogador)
        {
            try
            {
                jogador.nomeShirt = DataUtils.CreateJogadorShirt(
                    HttpContext.Server.MapPath("../Images/Player_Shirts"),
                    jogador);

                AppServices.AddJogador(jogador);

                return RedirectToAction("Jogadores");
            }
            catch
            {
                return View(new JogadorModels());
            }
        }

        //
        // GET: /Admin/Edit/5

        public ActionResult EditJogador(int id)
        {
            JogadorModels jm = new JogadorModels();
            jm.Jogador = AppServices.GetJogador(id);
            return View(jm);
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
                JogadorModels jm = new JogadorModels();
                jm.Jogador = AppServices.GetJogador(id);
                return View(jm);
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

                return RedirectToAction("Jogadores");
            }
            catch
            {
                return View(AppServices.GetJogador(id));
            }
        }

        public ActionResult RemoveJogadorFromTeam(int id)
        {
            return View(AppServices.GetJogador(id));
        }

        [HttpPost]
        public ActionResult RemoveJogadorFromTeam(int id, FormCollection collection)
        {
            try
            {
                AppServices.RemoveJogadorFromTeam(id, collection["Notas"]);

                return RedirectToAction("Jogadores");
            }
            catch
            {
                return View(AppServices.GetJogador(id));
            }
        }

        //**************************************// EQUIPAS //**************************************//

        //
        // GET: /Admin/Jogadores/

        public ActionResult Equipas()
        {
            return View(AppServices.GetAllEquipas());
        }

        //
        // GET: /Admin/Create

        public ActionResult CreateEquipa()
        {
            return View();
        }

        //
        // POST: /Admin/Create

        [HttpPost]
        public ActionResult CreateEquipa(Equipa equipa)
        {
            try
            {
                AppServices.AddEquipa(equipa);

                return RedirectToAction("Equipas");
            }
            catch
            {
                return View(equipa);
            }
        }

        //
        // GET: /Admin/Edit/5

        public ActionResult EditEquipa(int id)
        {
            return View(AppServices.GetEquipa(id));
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public ActionResult EditEquipa(int id, Equipa equipa)
        {
            try
            {
                AppServices.EditEquipa(equipa);

                return RedirectToAction("Equipas");
            }
            catch
            {
                return View(AppServices.GetEquipa(id));
            }
        }

        //
        // GET: /Admin/Delete/5

        public ActionResult DeleteEquipa(int id)
        {
            return View(AppServices.GetEquipa(id));
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost]
        public ActionResult DeleteEquipa(int id, Equipa equipa)
        {
            try
            {
                AppServices.RemoveEquipa(id);

                return RedirectToAction("Equipas");
            }
            catch
            {
                return View(AppServices.GetEquipa(id));
            }
        }
    }
}