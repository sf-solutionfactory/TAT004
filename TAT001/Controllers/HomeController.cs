using System;
using System.Collections.Generic;
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
        [Authorize]
        public ActionResult Index(string pais)
        {
            using (TAT001Entities db = new TAT001Entities())
            {
                int pagina = 1; //ID EN BASE DE DATOS
                string u = User.Identity.Name;
                if (pais != null)
                    Session["pais"] = pais;
                var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                ViewBag.permisos = db.PAGINAVs.Where(a => a.ID.Equals(user.ID)).ToList();
                ViewBag.carpetas = db.CARPETAVs.Where(a => a.USUARIO_ID.Equals(user.ID)).ToList();
                ViewBag.nombre = user.NOMBRE + " " + user.APELLIDO_P + " " + user.APELLIDO_M;
                ViewBag.email = user.EMAIL;
                ViewBag.rol = user.MIEMBROS.FirstOrDefault().ROL.NOMBRE;
                ViewBag.Title = db.PAGINAs.Where(a => a.ID.Equals(pagina)).First().PAGINATs.Where(b => b.SPRAS_ID.Equals(user.SPRAS_ID)).First().TXT50;
                ViewBag.warnings = db.WARNINGVs.Where(a => a.PAGINA_ID.Equals(pagina) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();
                try
                {
                    string p = Session["pais"].ToString();
                    ViewBag.pais = p + ".svg";
                }
                catch
                {
                    return RedirectToAction("Pais", "Home");
                }
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelPais(string pais)
        {
            Session["pais"] = pais;
            return View();
        }

        [Authorize]
        public ActionResult Pais()
        {
            using (TAT001Entities db = new TAT001Entities())
            {
                int pagina = 1009; //ID EN BASE DE DATOS
                string u = User.Identity.Name;
                var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                ViewBag.permisos = db.PAGINAVs.Where(a => a.ID.Equals(user.ID)).ToList();
                ViewBag.carpetas = db.CARPETAVs.Where(a => a.USUARIO_ID.Equals(user.ID)).ToList();
                ViewBag.nombre = user.NOMBRE + " " + user.APELLIDO_P + " " + user.APELLIDO_M;
                ViewBag.email = user.EMAIL;
                ViewBag.rol = user.MIEMBROS.FirstOrDefault().ROL.NOMBRE;
                ViewBag.flag = true;
                ViewBag.Title = db.PAGINAs.Where(a => a.ID.Equals(pagina)).First().PAGINATs.Where(b => b.SPRAS_ID.Equals(user.SPRAS_ID)).First().TXT50;
                ViewBag.warnings = db.WARNINGVs.Where(a => a.PAGINA_ID.Equals(pagina) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();
            }
            //ViewBag.Title = "Seleccionar país";
            return View();
        }
    }
}