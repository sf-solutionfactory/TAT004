using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAT001.Entities;
using TAT001.Models;

namespace TAT001.Controllers
{
    public class ListasController : Controller
    {
        // GET: Listas
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Clientes(string Prefix)
        {
            if (Prefix == null)
                Prefix = "";

            TAT001Entities db = new TAT001Entities();

            var c = (from N in db.CLIENTEs
                     where N.KUNNR.Contains(Prefix)
                     select new { N.KUNNR, N.NAME1 }).ToList();
            if (c.Count == 0)
            {
                var c2 = (from N in db.CLIENTEs
                          where N.NAME1.Contains(Prefix)
                          select new { N.KUNNR, N.NAME1 }).ToList();
                c.AddRange(c2);
            }
            JsonResult cc = Json(c, JsonRequestBehavior.AllowGet);
            return cc;
        }

        [HttpGet]
        public JsonResult Estados(string pais, string Prefix)
        {
            if (Prefix == null)
                Prefix = "";

            TAT001Entities db = new TAT001Entities();

            string p = pais.Split('.')[0].ToUpper();
            var c = (from N in db.STATES
                     where N.NAME.Contains(Prefix) & N.COUNTRy.SORTNAME.Equals(p)
                     select new { N.NAME });
            JsonResult cc = Json(c, JsonRequestBehavior.AllowGet);
            return cc;
        }

        [HttpGet]
        public JsonResult Ciudades(string estado, string Prefix)
        {
            if (Prefix == null)
                Prefix = "";

            TAT001Entities db = new TAT001Entities();

            var c = (from N in db.CITIES
                     join St in db.STATES
                     on N.STATE_ID equals St.ID
                     where N.NAME.Contains(Prefix) & St.NAME.Equals(estado)
                     select new { N.NAME });
            JsonResult cc = Json(c, JsonRequestBehavior.AllowGet);
            return cc;
        }

        [HttpGet]
        public JsonResult Det_Aprob(string bukrs, string puesto, string spras)
        {
            TAT001Entities db = new TAT001Entities();
            int p = Int16.Parse(puesto);
            var c = (from N in db.DET_APROB
                     join St in db.PUESTOTs
                     on N.PUESTOA_ID equals St.PUESTO_ID
                     where N.BUKRS.Equals(bukrs) & N.PUESTOC_ID.Equals(p) & St.SPRAS_ID.Equals(spras)
                     //where N.BUKRS.Equals(bukrs) 
                     select new { N.PUESTOA_ID, St.TXT50 });
            JsonResult cc = Json(c, JsonRequestBehavior.AllowGet);
            return cc;
        }

        [HttpGet]
        public JsonResult UsuariosPuesto(string puesto, string Prefix)
        {
            TAT001Entities db = new TAT001Entities();
            int p = Int16.Parse(puesto);
            var c = (from N in db.USUARIOs
                     where N.PUESTO_ID == p & N.ID.Contains(Prefix)
                     //where N.BUKRS.Equals(bukrs) 
                     select new { N.ID });
            JsonResult cc = Json(c, JsonRequestBehavior.AllowGet);
            return cc;
        }

        [HttpGet]
        public JsonResult Grupos(string bukrs, string pais, string user)
        {
            TAT001Entities db = new TAT001Entities();
            var c = (from N in db.CREADORs
                     where N.BUKRS == bukrs & N.LAND == pais & N.ID.Equals(user)
                     //where N.BUKRS.Equals(bukrs) 
                     select new { N.AGROUP_ID });
            JsonResult cc = Json(c, JsonRequestBehavior.AllowGet);
            return cc;
        }
        [HttpPost]
        public JsonResult getPresupuesto(string kunnr)
        {
            TAT001Entities db = new TAT001Entities();
            PRESUPUESTO_MOD pm = new PRESUPUESTO_MOD();
            try
            {
                if (kunnr == null)
                    kunnr = "";

                //Obtener presupuesto
                var presupuesto = db.CSP_PRESU_CLIENT(cLIENTE: kunnr).Select(p => new { DESC = p.DESCRIPCION.ToString(), VAL = p.VALOR.ToString() }).ToList();


                if (presupuesto != null)
                {
                    pm.P_CANAL = presupuesto[0].VAL;
                    pm.P_BANNER = presupuesto[1].VAL;
                    pm.PC_C = presupuesto[4].VAL;
                    pm.PC_A = presupuesto[5].VAL;
                    pm.PC_P = presupuesto[6].VAL;
                    pm.PC_T = presupuesto[7].VAL;
                }
            }
            catch
            {

            }
            db.Dispose();

            JsonResult cc = Json(pm, JsonRequestBehavior.AllowGet);
            return cc;
        }

    }
}