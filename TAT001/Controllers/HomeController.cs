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
                int pagina = 101; //ID EN BASE DE DATOS
                string u = User.Identity.Name;
                if (pais != null)
                    Session["pais"] = pais;
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
                int pagina = 102; //ID EN BASE DE DATOS
                string u = User.Identity.Name;
                var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                ViewBag.permisos = db.PAGINAVs.Where(a => a.ID.Equals(user.ID)).ToList();
                ViewBag.carpetas = db.CARPETAVs.Where(a => a.USUARIO_ID.Equals(user.ID)).ToList();
                ViewBag.usuario = user;
                ViewBag.rol = user.MIEMBROS.FirstOrDefault().ROL.NOMBRE;
                ViewBag.flag = true;
                ViewBag.Title = db.PAGINAs.Where(a => a.ID.Equals(pagina)).FirstOrDefault().PAGINATs.Where(b => b.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                ViewBag.warnings = db.WARNINGVs.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();
                ViewBag.textos = db.TEXTOes.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();
            }
            //ViewBag.Title = "Seleccionar país";
            return View();
        }


        [HttpGet]
        public JsonResult Clientes(string Prefix, string User)
        {
            if (Prefix == null)
                Prefix = "";
            //Note : you can bind same list from database  
            List<City> ObjList = new List<City>()
            {

                new City {Id=1,CityName="Latur" },
                new City {Id=2,CityName="Mumbai" },
                new City {Id=3,CityName="Pune" },
                new City {Id=4,CityName="Delhi" },
                new City {Id=5,CityName="Dehradun" },
                new City {Id=6,CityName="Noida" },
                new City {Id=7,CityName="New Delhi" },
                new City {Id=7,CityName="Mexico" }

        };
            //Searching records from list using LINQ query  
            var CityList = (from N in ObjList
                            where N.CityName.Contains(Prefix)
                            select new { N.CityName });
            JsonResult A = Json(CityList, JsonRequestBehavior.AllowGet);
            return A;
        }
    }
}