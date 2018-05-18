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
        public JsonResult Det_Aprob2(string bukrs, string puesto, string spras)
        {
            TAT001Entities db = new TAT001Entities();
            int p = Int16.Parse(puesto);
            DET_APROBH dh = db.DET_APROBH.Where(a => a.SOCIEDAD_ID.Equals(bukrs) & a.PUESTOC_ID == p).OrderByDescending(a => a.VERSION).FirstOrDefault();
            var c = (from N in db.DET_APROBP
                     join St in db.PUESTOTs
                     on N.PUESTOA_ID equals St.PUESTO_ID
                     where N.SOCIEDAD_ID.Equals(bukrs) & N.PUESTOC_ID.Equals(p) & St.SPRAS_ID.Equals(spras) & N.VERSION.Equals(dh.VERSION)
                     //where N.BUKRS.Equals(bukrs) 
                     select new { N.PUESTOA_ID.Value, St.TXT50 }).ToList();

            TAX_LAND tl = db.TAX_LAND.Where(a => a.SOCIEDAD_ID.Equals(bukrs)).FirstOrDefault();
            if (tl != null)
            {
                var col = (from  St in db.PUESTOTs
                           where St.PUESTO_ID== 9 &  St.SPRAS_ID.Equals(spras)
                           //where N.BUKRS.Equals(bukrs) 
                           select new { Value = St.PUESTO_ID, St.TXT50 });
                c.AddRange(col);
            }
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
            var c = (from N in db.CREADOR2
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
                //var presupuesto = db.CSP_PRESU_CLIENT(cLIENTE: kunnr).Select(p => new { DESC = p.DESCRIPCION.ToString(), VAL = p.VALOR.ToString() }).ToList();


                //if (presupuesto != null)
                //{
                //    pm.P_CANAL = presupuesto[0].VAL;
                //    pm.P_BANNER = presupuesto[1].VAL;
                //    pm.PC_C = presupuesto[4].VAL;
                //    pm.PC_A = presupuesto[5].VAL;
                //    pm.PC_P = presupuesto[6].VAL;
                //    pm.PC_T = presupuesto[7].VAL;
                //}
            }
            catch
            {

            }
            db.Dispose();

            JsonResult cc = Json(pm, JsonRequestBehavior.AllowGet);
            return cc;
        }

        [HttpGet]
        public JsonResult Relacionados(string num_doc, string spras)
        {
            TAT001Entities db = new TAT001Entities();
            //var c = db.DOCUMENTOes.Where(a => a.DOCUMENTO_REF.Equals(num_doc));
            decimal num = (decimal.Parse(num_doc));
            var c = (from D in db.DOCUMENTOes
                     join T in db.TSOLTs
                     on D.TSOL_ID equals T.TSOL_ID
                     join TA in db.TALLs
                     on D.TALL_ID equals TA.ID
                     join G in db.GALLTs
                     on TA.GALL_ID equals G.GALL_ID
                     where D.DOCUMENTO_REF == num
                     & T.SPRAS_ID == spras
                     & G.SPRAS_ID == spras
                     select new { D.NUM_DOC, T.TXT020, TXT500 = G.TXT50, FECHAD = D.FECHAD.Value.Year + "/" + D.FECHAD.Value.Month + "/" + D.FECHAD.Value.Day, HORAC = D.HORAC.Value.ToString(), D.ESTATUS_WF, D.ESTATUS, D.CONCEPTO });
            JsonResult cc = Json(c, JsonRequestBehavior.AllowGet);
            return cc;
        }

        [HttpGet]
        public JsonResult Paises(string bukrs)
        {
            TAT001Entities db = new TAT001Entities();

            var c = (from D in db.PAIS
                     where D.SOCIEDAD_ID == bukrs
                     select new { D.LAND, D.LANDX });
            JsonResult cc = Json(c, JsonRequestBehavior.AllowGet);
            return cc;
        }

        [HttpGet]
        public JsonResult selectTaxeo(string bukrs, string pais, string vkorg, string vtweg, string spart, string kunnr, string spras)
        {
            TAT001Entities db = new TAT001Entities();

            var c = (from T in db.TAXEOHs
                     join TX in db.TX_CONCEPTOT
                     on T.CONCEPTO_ID equals TX.CONCEPTO_ID
                     where T.SOCIEDAD_ID == bukrs
                     & T.PAIS_ID == pais
                     & T.VKORG == vkorg
                     & T.VTWEG == vtweg
                     & T.SPART == spart
                     & T.KUNNR == kunnr
                     & TX.SPRAS_ID == spras
                     select new { T.CONCEPTO_ID, TX.TXT50 });
            JsonResult cc = Json(c, JsonRequestBehavior.AllowGet);
            return cc;
        }
        [HttpPost]
        public JsonResult selectConcepto(string bukrs, string pais, string vkorg, string vtweg, string spart, string kunnr, string concepto, string spras)
        {
            TAT001Entities db = new TAT001Entities();
            int co = int.Parse(concepto);
            var c = (from T in db.TAXEOHs
                     join TX in db.TX_NOTAT
                     on T.TNOTA_ID equals TX.TNOTA_ID
                     where T.SOCIEDAD_ID == bukrs
                     & T.PAIS_ID == pais
                     & T.VKORG == vkorg
                     & T.VTWEG == vtweg
                     & T.SPART == spart
                     & T.KUNNR == kunnr
                     & T.CONCEPTO_ID == co
                     & TX.SPRAS_ID == spras
                     select new { T.TNOTA_ID, TX.TXT50 });
            JsonResult cc = Json(c, JsonRequestBehavior.AllowGet);
            return cc;
        }
        [HttpPost]
        public JsonResult selectImpuesto(string bukrs, string pais, string vkorg, string vtweg, string spart, string kunnr, string concepto, string spras)
        {
            TAT001Entities db = new TAT001Entities();
            int co = int.Parse(concepto);
            var c = (from T in db.TAXEOHs
                     where T.SOCIEDAD_ID == bukrs
                     & T.PAIS_ID == pais
                     & T.VKORG == vkorg
                     & T.VTWEG == vtweg
                     & T.SPART == spart
                     & T.KUNNR == kunnr
                     & T.CONCEPTO_ID == co
                     select new { T.IMPUESTO_ID, T.PORC, TXT50 = "IVA" }).ToList();

            var c2 = (from T in db.TAXEOPs
                      join R in db.TRETENCIONTs
                      on T.TRETENCION_ID equals R.TRETENCION_ID
                      where T.SOCIEDAD_ID == bukrs
                      & T.PAIS_ID == pais
                      & T.VKORG == vkorg
                      & T.VTWEG == vtweg
                      & T.SPART == spart
                      & T.KUNNR == kunnr
                      & T.CONCEPTO_ID == co
                      & R.SPRAS_ID == spras
                      select new { IMPUESTO_ID = T.RETENCION_ID.ToString(), T.PORC, R.TXT50 }).ToList();

            c.AddRange(c2);

            JsonResult cc = Json(c, JsonRequestBehavior.AllowGet);
            return cc;
        }
        [HttpPost]
        public JsonResult soportes(string bukrs, string land, string tsol, string spras)
        {
            TAT001Entities db = new TAT001Entities();

            var c = (from C in db.CONSOPORTEs
                     join T in db.TSOPORTETs
                     on C.TSOPORTE_ID equals T.TSOPORTE_ID
                     where C.SOCIEDAD_ID == bukrs
                     & C.PAIS_ID == land
                     & C.TSOL_ID == tsol
                     & T.SPRAS_ID == spras
                     select new { C.TSOPORTE_ID, C.OBLIGATORIO, T.TXT50 });

            JsonResult cc = Json(c, JsonRequestBehavior.AllowGet);
            return cc;
        }
    }
}