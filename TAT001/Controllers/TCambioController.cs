﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TAT001.Entities;

namespace TAT001.Controllers
{
    public class TCambioController : Controller
    {
        private TAT001Entities db = new TAT001Entities();

        // GET: TCambio
        public ActionResult Index()
        {
            int pagina = 621; //ID EN BASE DE DATOS
            using (TAT001Entities db = new TAT001Entities())
            {
                string u = User.Identity.Name;
                //string u = "admin";
                var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                ViewBag.permisos = db.PAGINAVs.Where(a => a.ID.Equals(user.ID)).ToList();
                ViewBag.carpetas = db.CARPETAVs.Where(a => a.USUARIO_ID.Equals(user.ID)).ToList();
                ViewBag.usuario = user;
                ViewBag.rol = user.PUESTO.PUESTOTs.Where(a => a.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                ViewBag.Title = db.PAGINAs.Where(a => a.ID.Equals(pagina)).FirstOrDefault().PAGINATs.Where(b => b.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                ViewBag.warnings = db.WARNINGVs.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();
                ViewBag.textos = db.TEXTOes.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();

                try
                {
                    string p = Session["pais"].ToString();
                    ViewBag.pais = p + ".svg";
                }
                catch
                {
                    //ViewBag.pais = "mx.svg";
                    //return RedirectToAction("Pais", "Home");
                }
                Session["spras"] = user.SPRAS_ID;
            }
            return View(db.TCAMBIOs.ToList());
        }
        // GET: TCambio/Details/5
        public ActionResult Details(string fcur, string tcur, string gd, string uku)
        {
            int pagina = 642; //ID EN BASE DE DATOS
            using (TAT001Entities db = new TAT001Entities())
            {
                string u = User.Identity.Name;
                var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                ViewBag.permisos = db.PAGINAVs.Where(a => a.ID.Equals(user.ID)).ToList();
                ViewBag.carpetas = db.CARPETAVs.Where(a => a.USUARIO_ID.Equals(user.ID)).ToList();
                ViewBag.usuario = user;
                ViewBag.rol = user.MIEMBROS.FirstOrDefault().ROL.NOMBRE;
                ViewBag.Title = db.PAGINAs.Where(a => a.ID.Equals(pagina)).FirstOrDefault().PAGINATs.Where(b => b.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                ViewBag.warnings = db.WARNINGVs.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();
                ViewBag.textos = db.TEXTOes.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();

                try
                {
                    string p = Session["pais"].ToString();
                    ViewBag.pais = p + ".svg";
                }
                catch
                {
                    //return RedirectToAction("Pais", "Home");
                }
                Session["spras"] = user.SPRAS_ID;
                var de = decimal.Parse(uku);
                var con = db.TCAMBIOs
                          .Where(x => x.FCURR == fcur && x.TCURR == tcur && x.UKURS == de)
                          .First();

                TCAMBIO co = new TCAMBIO();
                co.KURST = con.KURST;
                co.FCURR = con.FCURR;
                co.TCURR = con.TCURR;
                co.GDATU = con.GDATU;
                co.UKURS = con.UKURS;
                return View(co);
            }
        }
        // GET: TCambio/Create
        public ActionResult Create()
        {
            int pagina = 621; //ID EN BASE DE DATOS
            using (TAT001Entities db = new TAT001Entities())
            {
                string u = User.Identity.Name;
                //string u = "admin";
                var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                ViewBag.permisos = db.PAGINAVs.Where(a => a.ID.Equals(user.ID)).ToList();
                ViewBag.carpetas = db.CARPETAVs.Where(a => a.USUARIO_ID.Equals(user.ID)).ToList();
                ViewBag.usuario = user;
                ViewBag.rol = user.PUESTO.PUESTOTs.Where(a => a.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                ViewBag.Title = db.PAGINAs.Where(a => a.ID.Equals(pagina)).FirstOrDefault().PAGINATs.Where(b => b.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                ViewBag.warnings = db.WARNINGVs.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();
                ViewBag.textos = db.TEXTOes.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();

                try
                {
                    string p = Session["pais"].ToString();
                    ViewBag.pais = p + ".svg";
                }
                catch
                {
                    //ViewBag.pais = "mx.svg";
                    //return RedirectToAction("Pais", "Home");
                }
                Session["spras"] = user.SPRAS_ID;
            }
            ViewBag.TCURR = new SelectList(db.MONEDAs, "WAERS", "WAERS");
            ViewBag.FCURR = new SelectList(db.MONEDAs, "WAERS", "WAERS");
            return View();
        }

        // POST: TCambio/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KURST,FCURR,TCURR,GDATU,UKURS")] TCAMBIO tCAMBIO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (tCAMBIO.UKURS != 0 && tCAMBIO.UKURS != null)
                    {
                        tCAMBIO.KURST = "C";
                        db.TCAMBIOs.Add(tCAMBIO);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }

                ViewBag.TCURR = new SelectList(db.MONEDAs, "WAERS", "WAERS", tCAMBIO.TCURR);
                ViewBag.FCURR = new SelectList(db.MONEDAs, "WAERS", "WAERS", tCAMBIO.FCURR);
                //Como se genera la lista se borra y se ocupa regenerar
                int pagina = 621; //ID EN BASE DE DATOS
                using (TAT001Entities db = new TAT001Entities())
                {
                    string u = User.Identity.Name;
                    //string u = "admin";
                    var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                    ViewBag.permisos = db.PAGINAVs.Where(a => a.ID.Equals(user.ID)).ToList();
                    ViewBag.carpetas = db.CARPETAVs.Where(a => a.USUARIO_ID.Equals(user.ID)).ToList();
                    ViewBag.usuario = user;
                    ViewBag.rol = user.PUESTO.PUESTOTs.Where(a => a.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                    ViewBag.Title = db.PAGINAs.Where(a => a.ID.Equals(pagina)).FirstOrDefault().PAGINATs.Where(b => b.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                    ViewBag.warnings = db.WARNINGVs.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();
                    ViewBag.textos = db.TEXTOes.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();
                    ViewBag.error = "Campo Nulo o Valor Incorrecto";
                    try
                    {
                        string p = Session["pais"].ToString();
                        ViewBag.pais = p + ".svg";
                    }
                    catch
                    {
                        //ViewBag.pais = "mx.svg";
                        //return RedirectToAction("Pais", "Home");
                    }
                    Session["spras"] = user.SPRAS_ID;
                }

                return View(tCAMBIO);
            }
            catch (Exception e)
            {
                ViewBag.TCURR = new SelectList(db.MONEDAs, "WAERS", "WAERS", tCAMBIO.TCURR);
                ViewBag.FCURR = new SelectList(db.MONEDAs, "WAERS", "WAERS", tCAMBIO.FCURR);
                //Como se genera la lista se borra y se ocupa regenerar
                int pagina = 621; //ID EN BASE DE DATOS
                using (TAT001Entities db = new TAT001Entities())
                {
                    string u = User.Identity.Name;
                    //string u = "admin";
                    var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                    ViewBag.permisos = db.PAGINAVs.Where(a => a.ID.Equals(user.ID)).ToList();
                    ViewBag.carpetas = db.CARPETAVs.Where(a => a.USUARIO_ID.Equals(user.ID)).ToList();
                    ViewBag.usuario = user;
                    ViewBag.rol = user.PUESTO.PUESTOTs.Where(a => a.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                    ViewBag.Title = db.PAGINAs.Where(a => a.ID.Equals(pagina)).FirstOrDefault().PAGINATs.Where(b => b.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                    ViewBag.warnings = db.WARNINGVs.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();
                    ViewBag.textos = db.TEXTOes.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();
                    ViewBag.error = e.Message.ToString();
                    try
                    {
                        string p = Session["pais"].ToString();
                        ViewBag.pais = p + ".svg";
                    }
                    catch
                    {
                        //ViewBag.pais = "mx.svg";
                        //return RedirectToAction("Pais", "Home");
                    }
                    Session["spras"] = user.SPRAS_ID;
                }
                return View(tCAMBIO);
            }

        }

        // GET: TCambio/Edit/5
        public ActionResult Edit(string fcur, string tcur, string gd, string uku)
        {
            int pagina = 642; //ID EN BASE DE DATOS
            using (TAT001Entities db = new TAT001Entities())
            {
                string u = User.Identity.Name;
                var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                ViewBag.permisos = db.PAGINAVs.Where(a => a.ID.Equals(user.ID)).ToList();
                ViewBag.carpetas = db.CARPETAVs.Where(a => a.USUARIO_ID.Equals(user.ID)).ToList();
                ViewBag.usuario = user;
                ViewBag.rol = user.MIEMBROS.FirstOrDefault().ROL.NOMBRE;
                ViewBag.Title = db.PAGINAs.Where(a => a.ID.Equals(pagina)).FirstOrDefault().PAGINATs.Where(b => b.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                ViewBag.warnings = db.WARNINGVs.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();
                ViewBag.textos = db.TEXTOes.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();

                try
                {
                    string p = Session["pais"].ToString();
                    ViewBag.pais = p + ".svg";
                }
                catch
                {
                    //return RedirectToAction("Pais", "Home");
                }
                Session["spras"] = user.SPRAS_ID;
                decimal myuku = decimal.Parse(uku);
                var con = db.TCAMBIOs
                          .Where(x => x.FCURR == fcur && x.TCURR == tcur && x.UKURS == myuku)
                          .First();

                TCAMBIO co = new TCAMBIO();
                co.KURST = con.KURST;
                co.FCURR = con.FCURR;
                co.TCURR = con.TCURR;
                co.GDATU = con.GDATU;
                co.UKURS = con.UKURS;
                return View(co);
            }
        }

        // POST: TCambio/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KURST,FCURR,TCURR,GDATU,UKURS")] TCAMBIO tCAMBIO)
        {
            //Codigo incrustado
            tCAMBIO.KURST = "C";
            if (ModelState.IsValid)
            {

                db.Entry(tCAMBIO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TCURR = new SelectList(db.MONEDAs, "WAERS", "ISOCD", tCAMBIO.TCURR);
            ViewBag.FCURR = new SelectList(db.MONEDAs, "WAERS", "ISOCD", tCAMBIO.FCURR);
            return View(tCAMBIO);
        }

        // GET: TCambio/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TCAMBIO tCAMBIO = db.TCAMBIOs.Find(id);
            if (tCAMBIO == null)
            {
                return HttpNotFound();
            }
            return View(tCAMBIO);
        }

        // POST: TCambio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TCAMBIO tCAMBIO = db.TCAMBIOs.Find(id);
            db.TCAMBIOs.Remove(tCAMBIO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
