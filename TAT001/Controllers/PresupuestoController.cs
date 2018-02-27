using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAT001.Entities;

namespace TAT001.Controllers
{
    public class PresupuestoController : Controller
    {
        public ActionResult Index()
        {
            using (TAT001Entities db = new TAT001Entities())
            {
                string u = Session["UserID"].ToString();
                var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                var obj = db.PAGINAVs.Where(a => a.ID.Equals(user.ID)).ToList();
                if (obj != null)
                    ViewBag.permisos = obj;
                var obj2 = db.CARPETAVs.Where(a => a.USUARIO_ID.Equals(user.ID)).ToList();
                if (obj2 != null)
                    ViewBag.carpetas = obj2;
                ViewBag.nombre = user.NOMBRE + " " + user.APELLIDO_P + " " + user.APELLIDO_M;
                ViewBag.email = user.EMAIL;
                ViewBag.rol = user.MIEMBROS;
            }
            return View();
        }
        public ActionResult Carga()
        {
            using (TAT001Entities db = new TAT001Entities())
            {
                string u = Session["UserID"].ToString();
                var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                var obj = db.PAGINAVs.Where(a => a.ID.Equals(user.ID)).ToList();
                if (obj != null)
                    ViewBag.permisos = obj;
                var obj2 = db.CARPETAVs.Where(a => a.USUARIO_ID.Equals(user.ID)).ToList();
                if (obj2 != null)
                    ViewBag.carpetas = obj2;
                ViewBag.nombre = user.NOMBRE + " " + user.APELLIDO_P + " " + user.APELLIDO_M;
                ViewBag.email = user.EMAIL;
                ViewBag.rol = user.MIEMBROS;
            }
            return View();
        }
    }
}