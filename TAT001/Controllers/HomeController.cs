using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TAT001.Entities;
using TAT001.Models;

namespace TAT001.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private TAT001Entities db = new TAT001Entities();

        [Authorize]
        public ActionResult Index(string pais)
        {
            using (TAT001Entities db = new TAT001Entities())
            {
                int pagina = 201; //ID EN BASE DE DATOS
                string u = User.Identity.Name;
                if (pais != null)
                    Session["pais"] = pais;
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
                    ////return RedirectToAction("Pais", "Home");
                }
                Session["spras"] = user.SPRAS_ID;

                try//Mensaje de documento creado
                {
                    string p = Session["NUM_DOC"].ToString();
                    ViewBag.NUM_DOC = p;
                    Session["NUM_DOC"] = null;
                }
                catch
                {
                    ViewBag.NUM_DOC = "";
                }

                try//Mensaje de documento creado
                {
                    string error_files = Session["ERROR_FILES"].ToString();
                    ViewBag.ERROR_FILES = error_files;
                    Session["ERROR_FILES"] = null;
                }
                catch
                {
                    ViewBag.ERROR_FILES = "";
                }
            }


            var dOCUMENTOes = db.DOCUMENTOes.Where(a => a.USUARIOC_ID.Equals(User.Identity.Name)).Include(d => d.TALL).Include(d => d.TSOL).Include(d => d.USUARIO).Include(d => d.CLIENTE).Include(d => d.PAI).Include(d => d.SOCIEDAD).ToList();
            var dOCUMENTOVs = db.DOCUMENTOVs.Where(a => a.USUARIOA_ID.Equals(User.Identity.Name)).ToList();
            var tsol = db.TSOLs.ToList();
            var tall = db.TALLs.ToList();
            foreach (DOCUMENTOV v in dOCUMENTOVs)
            {
                DOCUMENTO d = new DOCUMENTO();
                var ppd = d.GetType().GetProperties();
                var ppv = v.GetType().GetProperties();
                foreach (var pv in ppv)
                {
                    foreach (var pd in ppd)
                    {
                        if (pd.Name == pv.Name)
                        {
                            pd.SetValue(d, pv.GetValue(v));
                            break;
                        }
                    }
                }
                d.TSOL = tsol.Where(a => a.ID.Equals(d.TSOL_ID)).FirstOrDefault();
                d.TALL = tall.Where(a => a.ID.Equals(d.TALL_ID)).FirstOrDefault();
                //d.ESTADO = db.STATES.Where(a => a.ID.Equals(v.ESTADO)).FirstOrDefault().NAME;
                //d.CIUDAD = db.CITIES.Where(a => a.ID.Equals(v.CIUDAD)).FirstOrDefault().NAME;
                dOCUMENTOes.Add(d);
            }
            dOCUMENTOes = dOCUMENTOes.Distinct(new DocumentoComparer()).ToList();
            dOCUMENTOes = dOCUMENTOes.OrderByDescending(a => a.FECHAC).OrderByDescending(a => a.NUM_DOC).ToList();
            
            return View(dOCUMENTOes);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelPais(string pais)
        {
            Session["pais"] = pais.ToUpper() ;
            return View();
        }

        [Authorize]
        public ActionResult Pais(string pais)
        {
            using (TAT001Entities db = new TAT001Entities())
            {
                int pagina = 102; //ID EN BASE DE DATOS
                string u = User.Identity.Name;
                var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                ViewBag.permisos = db.PAGINAVs.Where(a => a.ID.Equals(user.ID)).ToList();
                ViewBag.carpetas = db.CARPETAVs.Where(a => a.USUARIO_ID.Equals(user.ID)).ToList();
                ViewBag.usuario = user;
                //ViewBag.rol = user.PUESTO.PUESTOTs.Where(a => a.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                ViewBag.rol = user.PUESTO.PUESTOTs.Where(a => a.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                ViewBag.flag = true;
                ViewBag.Title = db.PAGINAs.Where(a => a.ID.Equals(pagina)).FirstOrDefault().PAGINATs.Where(b => b.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                ViewBag.warnings = db.WARNINGVs.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();
                ViewBag.textos = db.TEXTOes.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();

                //var p = db.PAIS.Where(a => a.ACTIVO.Equals(true));

                //ViewBag.creador = db.CREADORs.Where(a => a.ID.Equals(u) & a.ACTIVO == true).ToList();

                var p = from P in db.PAIS
                    join C in db.CREADOR2 on P.LAND equals C.LAND
                    where P.ACTIVO == true
                    & C.ID == u & C.ACTIVO == true
                    select P;

                return View(p.ToList());
            }
            //return View();

        }


        [HttpGet]
        public JsonResult Clientes(string Prefix)
        {
            if (Prefix == null)
                Prefix = "";

            TAT001Entities db = new TAT001Entities();

            var c = (from N in db.CLIENTEs
                     where N.KUNNR.Contains(Prefix)
                     select new { N.KUNNR, N.NAME1 });
            JsonResult cc = Json(c, JsonRequestBehavior.AllowGet);
            return cc;
        }
    }
}