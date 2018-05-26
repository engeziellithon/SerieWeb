﻿using Microsoft.AspNet.Identity;
using SerieWeb.Models.Admininstracao;
using SerieWeb.Models.Identity;
using SerieWeb.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;

using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SerieWeb.Controllers.Site
{
    public class ExplorarController : Controller
    {
       
        #region Banco
        private ApplicationDbContext db = new ApplicationDbContext();
        #endregion

        #region Explorar
        // GET: Explorar
        public ActionResult Index(string pesquisa)
        {
            List<Serie> model = new List<Serie>();
            if (pesquisa != null)
            {
                model = db.Series.Where(x => x.NomeSerie.Contains(pesquisa)).Take(20).OrderBy(d => d.Nota).ToList();
            }
            else
            {
                model = db.Series.OrderBy(d =>d.Nota).Take(20).ToList();
            }
           
            return View(model);
        }
        #endregion
        
        #region Detalhes dos serie
        // GET: Serie/DetailsUsuario/5
        [HttpGet]
        public ActionResult DetalhesSerie(int? id, string MensagemSucesso)
        {
            #region Verificar se serie existe
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Serie serie = db.Series.Find(id);
            

            if (serie == null)
            {
                return HttpNotFound();
            }
            #endregion


            if (serie.Trailer != null)
            {
                ViewBag.TrailerCompartilhar = "";
                string trailer = serie.Trailer;
                bool compartilharYoutube = Regex.IsMatch(serie.Trailer, @"^(https:\/\/)youtu(.be)?(\.com)?\/.+");

                if (compartilharYoutube == true)
                {
                    var TrailerCompartilhar = trailer.Substring(trailer.Length - 11);
                    ViewBag.TrailerCompartilhar = TrailerCompartilhar;
                }
            }

            ViewBag.Indicacoes = db.Series.OrderBy(c => c.Nota).Take(3).ToList();
            ViewBag.listatemporada = db.Episodios.Where(s => s.SerieID == serie.SerieID).Select(t => t.Temporada).ToList().Distinct();
            
            
          
            return View(serie);
        }
        #endregion

        [HttpPost]
        public ActionResult ExiberEpisodio(int SerieID,int TemporadaID)
        {

            var listaEpisodios = db.Episodios.Where(s => s.SerieID == SerieID && s.TemporadaID == TemporadaID).Select(s => new
            {
                NomeEpisodio = s.NomeEpisodio,
                DataEpisodio = s.DataExibicao.Day + "/" + s.DataExibicao.Month + "/" + s.DataExibicao.Year
            }).ToList();

           
            return Json(new { listaEpisodio = listaEpisodios });
        }

        [HttpPost]
        public ActionResult SalvarFavorito(int IdSerie)
        {
            #region verifica se esta logando / Verifica id do usuario logado / verifica se a serie existe.

            string user = User.Identity.GetUserId();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            Serie serie = db.Series.Find(IdSerie);

            if (serie == null)
            {
                return HttpNotFound();
            }
            #endregion

            UsuarioPerfil perfil = new UsuarioPerfil();
            var StatusSerieFavorita = db.UsuarioPerfil.Where(p => p.SerieID == serie.SerieID && p.UserId == user).FirstOrDefault();

            #region Adicionar aos favoritos
            if (StatusSerieFavorita == null)
            {
                perfil.UserId = user;
                perfil.SerieID = serie.SerieID;
                perfil.SerieFavorita = true;
                db.UsuarioPerfil.Add(perfil);
                db.SaveChanges();
            }
            #endregion

            #region Edição da Serie favorita 
            else if (StatusSerieFavorita.SerieFavorita == false)
            {
                try
                {
                    perfil = db.UsuarioPerfil.Find(StatusSerieFavorita.UsuarioPerfilID);
                    perfil.SerieFavorita = true;
                    db.Entry(perfil).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("DetalhesSerie", "Explorar", new { id = serie.SerieID });
                }
                catch (Exception ex)
                {
                    var teste = ex;
                    ModelState.AddModelError("", "Não foi possível salvar as alterações.Tente novamente se o problema persistir, consulte o administrador do sistema.");
                }
            }
            else
            {
                try
                {
                    perfil = db.UsuarioPerfil.Find(StatusSerieFavorita.UsuarioPerfilID);
                    perfil.SerieFavorita = false;
                    db.Entry(perfil).State = EntityState.Modified;
                    db.SaveChanges();
                    
                    return RedirectToAction("DetalhesSerie", "Explorar", new { id = serie.SerieID });
                }
                catch (Exception ex)
                {
                    var teste = ex;
                    ModelState.AddModelError("", "Não foi possível salvar as alterações.Tente novamente se o problema persistir, consulte o administrador do sistema.");
                }
            }
            #endregion
            
          
            return RedirectToAction("DetalhesSerie", "Explorar", new { id = serie.SerieID });
        }

       


    }
}