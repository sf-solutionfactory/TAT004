﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TAT001.Entities;

namespace TAT001.Controllers.Catalogos
{
    public class trvController : Controller
    {
        private TAT001Entities db = new TAT001Entities();

        // GET: trv
        public ActionResult Index()
        {
            int pagina = 621; //ID EN BASE DE DATOS
            USUARIO user = null;
            using (TAT001Entities db = new TAT001Entities())
            {
                string u = User.Identity.Name;
                //string u = "admin";
                user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
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
                ViewBag.lan = user.SPRAS_ID;
            }
            return View(db.TREVERSAs.ToList());
        }

        // GET: trv/Details/5
        public ActionResult Details(int? id)
        {
            int pagina = 621; //ID EN BASE DE DATOS
            USUARIO user = null;
            using (TAT001Entities db = new TAT001Entities())
            {
                string u = User.Identity.Name;
                //string u = "admin";
                user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
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
                ViewBag.lan = user.SPRAS_ID;
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TREVERSA tREVERSA = db.TREVERSAs.Find(id);
            if (tREVERSA == null)
            {
                return HttpNotFound();
            }
            return View(tREVERSA);
        }

        // GET: trv/Create
        public ActionResult Create()
        {
            int pagina = 621; //ID EN BASE DE DATOS
            USUARIO user = null;
            using (TAT001Entities db = new TAT001Entities())
            {
                string u = User.Identity.Name;
                //string u = "admin";
                user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                ViewBag.permisos = db.PAGINAVs.Where(a => a.ID.Equals(user.ID)).ToList();
                ViewBag.carpetas = db.CARPETAVs.Where(a => a.USUARIO_ID.Equals(user.ID)).ToList();
                ViewBag.usuario = user;
                ViewBag.rol = user.PUESTO.PUESTOTs.Where(a => a.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                ViewBag.Title = db.PAGINAs.Where(a => a.ID.Equals(pagina)).FirstOrDefault().PAGINATs.Where(b => b.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                ViewBag.warnings = db.WARNINGVs.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();
                ViewBag.textos = db.TEXTOes.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();
                ViewBag.usi = user.ID;
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
                ViewBag.lan = user.SPRAS_ID;
            }
            return View();
        }

        // POST: trv/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DESCRIPCION,USUARIOC_ID,FECHAC,ACTIVO")] TREVERSA tREVERSA, string us)
        {
            if (ModelState.IsValid)
            {
                tREVERSA.USUARIOC_ID = us;
                tREVERSA.FECHAC = DateTime.Now;
                tREVERSA.ACTIVO = true;
                db.TREVERSAs.Add(tREVERSA);
                db.SaveChanges();
                //Posterior a la insercion del registro, insertar en treversat
                TREVERSA trvi = db.TREVERSAs.Where(x => x.DESCRIPCION == tREVERSA.DESCRIPCION).FirstOrDefault();
                //si trae registros entra
                if (trvi != null)
                {                   
                    List<SPRA> ss = db.SPRAS.ToList();
                    foreach (SPRA s in ss)
                    {
                        TREVERSAT trvt = new TREVERSAT();
                        trvt.SPRAS_ID = s.ID;
                        trvt.TREVERSA_ID = trvi.ID;
                        trvt.TXT100 = tREVERSA.DESCRIPCION;
                        db.TREVERSATs.Add(trvt);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }

            return View(tREVERSA);
        }

        // GET: trv/Edit/5
        public ActionResult Edit(int? id)
        {
            int pagina = 621; //ID EN BASE DE DATOS
            USUARIO user = null;
            using (TAT001Entities db = new TAT001Entities())
            {
                string u = User.Identity.Name;
                //string u = "admin";
                user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
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
                ViewBag.lan = user.SPRAS_ID;
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TREVERSA tREVERSA = db.TREVERSAs.Find(id);
            if (tREVERSA == null)
            {
                return HttpNotFound();
            }
            return View(tREVERSA);
        }

        // POST: trv/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DESCRIPCION,USUARIOC_ID,FECHAC,ACTIVO")] TREVERSA tREVERSA)
        {

            if (ModelState.IsValid)
            {
                //Recuperamos todas las descripciones en sus lenguajes
                List<SPRA> ss = db.SPRAS.ToList();
                foreach (SPRA s in ss)
                {
                    try
                    {
                        TREVERSAT tr = new TREVERSAT();
                        tr.TREVERSA_ID = tREVERSA.ID;
                        tr.SPRAS_ID = s.ID;
                        tr.TXT100 = Request.Form[s.ID].ToString();
                        db.Entry(tr).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        var x = e.ToString();
                    }
                }

                return RedirectToAction("Index");
            }
            return View(tREVERSA);
        }

        // GET: trv/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TREVERSA tREVERSA = db.TREVERSAs.Find(id);
            if (tREVERSA == null)
            {
                return HttpNotFound();
            }
            return View(tREVERSA);
        }

        // POST: trv/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TREVERSA tREVERSA = db.TREVERSAs.Find(id);
            db.TREVERSAs.Remove(tREVERSA);
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