using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TAT001.Entities;
using TAT001.Services;

namespace TAT001.Controllers
{
    [Authorize]
    public class FlujosController : Controller
    {
        private TAT001Entities db = new TAT001Entities();

        // GET: Flujos
        public ActionResult Index()
        {
            int pagina = 103; //ID EN BASE DE DATOS
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
                    //ViewBag.pais = "mx.svg";
                    return RedirectToAction("Pais", "Home");
                }
                Session["spras"] = user.SPRAS_ID;
            }
            var fLUJOes = db.FLUJOes.Include(f => f.DOCUMENTO).Include(f => f.USUARIO).Include(f => f.USUARIO1).Include(f => f.WORKFP);
            return View(fLUJOes.ToList());
        }

        // GET: Flujos/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FLUJO fLUJO = db.FLUJOes.Find(id);
            if (fLUJO == null)
            {
                return HttpNotFound();
            }
            return View(fLUJO);
        }

        // GET: Flujos/Create
        public ActionResult Create(decimal id)
        {
            int pagina = 103; //ID EN BASE DE DATOS
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
                    //ViewBag.pais = "mx.svg";
                    return RedirectToAction("Pais", "Home");
                }
                Session["spras"] = user.SPRAS_ID;
            }

            DOCUMENTO dOCUMENTO = db.DOCUMENTOes.Find(id);
            //db.DOCUMENTOes.Add(dOCUMENTO);
            //db.SaveChanges();

            WORKFV wf = db.WORKFHs.Where(a => a.TSOL_ID.Equals(dOCUMENTO.TSOL_ID)).FirstOrDefault().WORKFVs.OrderByDescending(a => a.VERSION).FirstOrDefault();
            WORKFP wp = wf.WORKFPs.OrderBy(a => a.POS).FirstOrDefault();
            FLUJO f = new FLUJO();
            f.WORKF_ID = wf.ID;
            f.WF_VERSION = wf.VERSION;
            f.WF_POS = wp.POS;
            f.NUM_DOC = dOCUMENTO.NUM_DOC;
            f.POS = 1;
            f.LOOP = 1;
            f.USUARIOA_ID = dOCUMENTO.USUARIOC_ID;
            f.ESTATUS = "A";
            f.FECHAC = DateTime.Now;
            f.FECHAM = DateTime.Now;

            WORKFP next = wf.WORKFPs.Where(a => a.POS.Equals(wp.NEXT_STEP)).FirstOrDefault();
            FLUJO fn = new FLUJO();
            fn.WORKF_ID = wf.ID;
            fn.WF_VERSION = wf.VERSION;
            fn.WF_POS = next.POS;
            fn.NUM_DOC = dOCUMENTO.NUM_DOC;
            fn.POS = 2;
            fn.LOOP = 1;
            fn.ESTATUS = "P";
            fn.FECHAC = DateTime.Now;
            fn.FECHAM = DateTime.Now;
            fn.USUARIOA_ID = db.USUARIOs.Where(a => a.ID.Equals(dOCUMENTO.USUARIOC_ID)).FirstOrDefault().MANAGER;

            db.FLUJOes.Add(f);
            db.FLUJOes.Add(fn);

            db.SaveChanges();
            if(wp.EMAIL.Equals("X"))
                return RedirectToAction("Enviar", "Mails", new { id = id });

            return RedirectToAction("Details", "Solicitudes", new { id = id });
        }

        // POST: Flujos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WORKF_ID,WF_VERSION,WF_POS,NUM_DOC,POS,LOOP,USUARIOA_ID,USUARIOD_ID,ESTATUS,FECHAC,HORAC,FECHAM,HORAM")] FLUJO fLUJO)
        {
            if (ModelState.IsValid)
            {
                db.FLUJOes.Add(fLUJO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NUM_DOC = new SelectList(db.DOCUMENTOes, "NUM_DOC", "TSOL_ID", fLUJO.NUM_DOC);
            ViewBag.USUARIOA_ID = new SelectList(db.USUARIOs, "ID", "PASS", fLUJO.USUARIOA_ID);
            ViewBag.USUARIOD_ID = new SelectList(db.USUARIOs, "ID", "PASS", fLUJO.USUARIOD_ID);
            ViewBag.WORKF_ID = new SelectList(db.WORKFPs, "ID", "EMAIL", fLUJO.WORKF_ID);
            return View(fLUJO);
        }

        // GET: Flujos/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FLUJO fLUJO = db.FLUJOes.Find(id);
            if (fLUJO == null)
            {
                return HttpNotFound();
            }
            ViewBag.NUM_DOC = new SelectList(db.DOCUMENTOes, "NUM_DOC", "TSOL_ID", fLUJO.NUM_DOC);
            ViewBag.USUARIOA_ID = new SelectList(db.USUARIOs, "ID", "PASS", fLUJO.USUARIOA_ID);
            ViewBag.USUARIOD_ID = new SelectList(db.USUARIOs, "ID", "PASS", fLUJO.USUARIOD_ID);
            ViewBag.WORKF_ID = new SelectList(db.WORKFPs, "ID", "EMAIL", fLUJO.WORKF_ID);
            return View(fLUJO);
        }

        // POST: Flujos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WORKF_ID,WF_VERSION,WF_POS,NUM_DOC,POS,LOOP,USUARIOA_ID,USUARIOD_ID,ESTATUS,FECHAC,HORAC,FECHAM,HORAM")] FLUJO fLUJO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fLUJO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NUM_DOC = new SelectList(db.DOCUMENTOes, "NUM_DOC", "TSOL_ID", fLUJO.NUM_DOC);
            ViewBag.USUARIOA_ID = new SelectList(db.USUARIOs, "ID", "PASS", fLUJO.USUARIOA_ID);
            ViewBag.USUARIOD_ID = new SelectList(db.USUARIOs, "ID", "PASS", fLUJO.USUARIOD_ID);
            ViewBag.WORKF_ID = new SelectList(db.WORKFPs, "ID", "EMAIL", fLUJO.WORKF_ID);
            return View(fLUJO);
        }

        // GET: Flujos/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FLUJO fLUJO = db.FLUJOes.Find(id);
            if (fLUJO == null)
            {
                return HttpNotFound();
            }
            return View(fLUJO);
        }

        // POST: Flujos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            FLUJO fLUJO = db.FLUJOes.Find(id);
            db.FLUJOes.Remove(fLUJO);
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
        [HttpGet]
        public ActionResult Procesa(decimal id, string accion)
        {
            int pagina = 103; //ID EN BASE DE DATOS
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
                    //ViewBag.pais = "mx.svg";
                    return RedirectToAction("Pais", "Home");
                }
                Session["spras"] = user.SPRAS_ID;
            }
            FLUJO f = db.FLUJOes.Where(a => a.NUM_DOC.Equals(id)).OrderByDescending(a => a.POS).FirstOrDefault();
            f.ESTATUS = accion;
            return View(f);
        }
        [HttpPost]
        public ActionResult Procesa(FLUJO f)
        {
            FLUJO actual = db.FLUJOes.Where(a => a.NUM_DOC.Equals(f.NUM_DOC)).OrderByDescending(a => a.POS).FirstOrDefault();
            FLUJO flujo = actual;
            flujo.ESTATUS = f.ESTATUS;
            flujo.FECHAM = DateTime.Now;
            flujo.COMENTARIO = f.COMENTARIO;

            ProcesaFlujo pf = new ProcesaFlujo();
            if (ModelState.IsValid)
            {
                int res = pf.procesa(flujo);
                if (res.Equals(0))//Aprobado
                {
                    return RedirectToAction("Details", "Solicitudes", new { id = flujo.NUM_DOC });
                }
                else if (res.Equals(1))//CORREO
                {
                    return RedirectToAction("Enviar", "Mails", new { id = flujo.NUM_DOC });

                }
                else if (res.Equals(2))//CORREO DE FIN DE WORKFLOW
                {
                    return RedirectToAction("Enviar", "Mails", new { id = flujo.NUM_DOC });
                }
                else if (res.Equals(3))//Rechazado
                {
                    return RedirectToAction("Details", "Solicitudes", new { id = flujo.NUM_DOC });
                }
            }

            int pagina = 103; //ID EN BASE DE DATOS
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
                    //ViewBag.pais = "mx.svg";
                    return RedirectToAction("Pais", "Home");
                }
                Session["spras"] = user.SPRAS_ID;
            }
            return View(f);
        }
    }
}
