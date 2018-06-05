using ClosedXML.Excel;
using System;
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
    public class TSOLController : Controller
    {
        private TAT001Entities db = new TAT001Entities();

        // GET: TSOL
        public ActionResult Index()
        {
            int pagina = 791; //ID EN BASE DE DATOS
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
            var tSOLs = db.TSOLs.Include(t => t.RANGO).Include(t => t.TSOL2).Include(x => x.TSOLTs).ToList();
            return View(tSOLs);
        }

        // GET: TSOL/Details/5
        public ActionResult Details(string id)
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TSOL tSOL = db.TSOLs.Find(id);
            if (tSOL == null)
            {
                return HttpNotFound();
            }
            return View(tSOL);
        }

        // GET: TSOL/Create
        public ActionResult Create()
        {
            ViewBag.RANGO_ID = new SelectList(db.RANGOes, "ID", "ID");
            ViewBag.TSOLR = new SelectList(db.TSOLs, "ID", "DESCRIPCION");
            return View();
        }

        // POST: TSOL/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DESCRIPCION,TSOLR,RANGO_ID,ESTATUS")] TSOL tSOL)
        {
            if (ModelState.IsValid)
            {
                db.TSOLs.Add(tSOL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RANGO_ID = new SelectList(db.RANGOes, "ID", "ID", tSOL.RANGO_ID);
            ViewBag.TSOLR = new SelectList(db.TSOLs, "ID", "DESCRIPCION", tSOL.TSOLR);
            return View(tSOL);
        }

        // GET: TSOL/Edit/5
        public ActionResult Edit(string id)
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TSOL tSOL = db.TSOLs.Find(id);
            if (tSOL == null)
            {
                return HttpNotFound();
            }
            ViewBag.RANGO_ID = new SelectList(db.RANGOes, "ID", "ID", tSOL.RANGO_ID);
            ViewBag.TSOLR = new SelectList(db.TSOLs, "ID", "DESCRIPCION", tSOL.TSOLR);
            return View(tSOL);
        }

        // POST: TSOL/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DESCRIPCION,TSOLR,RANGO_ID,ESTATUS")] TSOL tSOL)
        {
            if (ModelState.IsValid)
            {
                List<SPRA> ss = db.SPRAS.ToList();
                foreach (SPRA s in ss)
                {
                    try
                    {
                        TSOLT p = new TSOLT();
                        p.TSOL_ID = tSOL.ID;
                        p.SPRAS_ID = s.ID;
                        p.TXT020 = Request.Form[s.ID].ToString();
                        db.Entry(p).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        var x = e.ToString();
                    }
                }
                return RedirectToAction("Index");
            }
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
            ViewBag.RANGO_ID = new SelectList(db.RANGOes, "ID", "ID", tSOL.RANGO_ID);
            ViewBag.TSOLR = new SelectList(db.TSOLs, "ID", "DESCRIPCION", tSOL.TSOLR);
            return View(tSOL);
        }

        // GET: TSOL/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TSOL tSOL = db.TSOLs.Find(id);
            if (tSOL == null)
            {
                return HttpNotFound();
            }
            return View(tSOL);
        }

        // POST: TSOL/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TSOL tSOL = db.TSOLs.Find(id);
            db.TSOLs.Remove(tSOL);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public FileResult Descargar()
        {
            var tSOLs = db.TSOLs.Include(t => t.RANGO).Include(t => t.TSOL2).Include(x => x.TSOLTs).ToList();
            generarExcelHome(tSOLs, Server.MapPath("~/pdfTemp/"));
            return File(Server.MapPath("~/pdfTemp/DocTS" + DateTime.Now.ToShortDateString() + ".xlsx"), "application /vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DocTS" + DateTime.Now.ToShortDateString() + ".xlsx");
        }

        public void generarExcelHome(List<TSOL> lst, string ruta)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sheet 1");
            try
            {
                worksheet.Cell("A1").Value = new[]
             {
                  new {
                      BANNER = "ID"
                      },
                    };
                worksheet.Cell("B1").Value = new[]
            {
                  new {
                      BANNER = "RANGO"
                      },
                    };
                worksheet.Cell("C1").Value = new[]
            {
                  new {
                      BANNER = "DESCRIPCION"
                      },
                    };
                for (int i = 2; i <= (lst.Count + 1); i++)
                {
                    worksheet.Cell("A" + i).Value = new[]
               {
                  new {
                      BANNER       = lst[i-2].ID
                      },
                    };
                    worksheet.Cell("B" + i).Value = new[]
                {
                  new {
                      BANNER       = lst[i-2].RANGO.ID
                      },
                    };
                    var tslt = "";
                    try
                    {                   
                        string u = User.Identity.Name;
                        USUARIO user = null;
                        user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                        tslt = lst[i - 2].TSOLTs.Where(x => x.SPRAS_ID == user.SPRAS_ID).FirstOrDefault().TXT020;
                    }
                    catch (Exception e)
                    {
                        tslt = "";
                    }
                    worksheet.Cell("C" + i).Value = new[]
                    {
                        new {
                            BANNER       = tslt
                        },
                    };
                }
                var rt = ruta + @"\DocTS" + DateTime.Now.ToShortDateString() + ".xlsx";
                workbook.SaveAs(rt);
            }
            catch (Exception e)
            {
                var ex = e.ToString();
            }
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
