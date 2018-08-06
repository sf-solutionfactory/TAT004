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
using TAT001.Models;

namespace TAT001.Controllers
{
    [AllowAnonymous]
    public class CorreosController : Controller
    {
        private TAT001Entities db = new TAT001Entities();

        // GET: Correos
        public ActionResult Index(decimal id, bool? mail) //B20180803 MGC Correos
        {
            var dOCUMENTO = db.DOCUMENTOes.Where(x => x.NUM_DOC == id).FirstOrDefault();
            var flujo = db.FLUJOes.Where(x => x.NUM_DOC == id).OrderByDescending(o => o.POS).Select(s => s.POS).ToList();
            ViewBag.Pos = flujo[0];
            ViewBag.url = "http://localhost:64497";
            ViewBag.url = "http://192.168.1.77";
            ViewBag.url = Request.Url.AbsoluteUri.Replace(Request.Url.AbsolutePath, "");
            //ViewBag.miles = dOCUMENTOes.PAI.MILES;//LEJGG 090718
            //ViewBag.dec = dOCUMENTOes.PAI.DECIMAL;//LEJGG 090718
            FormatosC fc = new FormatosC();
            ViewBag.monto = fc.toShow((decimal)dOCUMENTO.MONTO_DOC_MD, dOCUMENTO.PAI.DECIMAL);

            //B20180803 MGC Correos............
            string mailv = "";
            if(mail != null)
            {
                if(mail == true)
                {
                    mailv = "X";
                }
            }

            ViewBag.mail = mailv;
            //B20180803 MGC Correos............

            //B20180803 MGC Presupuesto............
            Models.PresupuestoModels carga = new Models.PresupuestoModels();
            ViewBag.ultMod = carga.consultarUCarga();

            dOCUMENTO.PAI = db.PAIS.Where(a => a.LAND.Equals(dOCUMENTO.PAIS_ID)).FirstOrDefault();
            if (dOCUMENTO.PAI != null)
            {
                ViewBag.miles = dOCUMENTO.PAI.MILES;//LEJGG 090718
                ViewBag.dec = dOCUMENTO.PAI.DECIMAL;//LEJGG 090718
            }

            CLIENTE_MOD cli = new CLIENTE_MOD();

            cli = SelectCliente(dOCUMENTO.PAYER_ID);

            ViewBag.kunnr = cli.KUNNR;
            ViewBag.vtweg = cli.VTWEG;
            //B20180803 MGC Presupuesto............

            return View(dOCUMENTO);
        }

        // GET: Correos/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dOCUMENTO = db.DOCUMENTOes.Where(x => x.NUM_DOC == id).FirstOrDefault();
            ViewBag.workflow = db.FLUJOes.Where(a => a.NUM_DOC.Equals(id)).OrderBy(a => a.POS).ToList();
            ViewBag.acciones = db.FLUJOes.Where(a => a.NUM_DOC.Equals(id) & a.ESTATUS.Equals("P") & a.USUARIOA_ID.Equals(User.Identity.Name)).FirstOrDefault();
            ViewBag.url = "http://localhost:64497";
            ViewBag.url = "http://192.168.1.77";
            ViewBag.url = Request.Url.AbsoluteUri.Replace(Request.Url.AbsolutePath, "");
            if (dOCUMENTO == null)
            {
                return HttpNotFound();
            }
            //ViewBag.miles = dOCUMENTO.PAI.MILES;//LEJGG 090718
            //ViewBag.dec = dOCUMENTO.PAI.DECIMAL;//LEJGG 090718
            FormatosC fc = new FormatosC();
            ViewBag.monto = fc.toShow((decimal)dOCUMENTO.MONTO_DOC_MD, dOCUMENTO.PAI.DECIMAL);
            return View(dOCUMENTO);
        }

        // GET: Correos
        public ActionResult Recurrente(decimal id)
        {
            var dOCUMENTO = db.DOCUMENTOes.Where(x => x.NUM_DOC == id).FirstOrDefault();
            var flujo = db.FLUJOes.Where(x => x.NUM_DOC == id).OrderByDescending(o => o.POS).Select(s => s.POS).ToList();
            ViewBag.Pos = flujo[0];
            ViewBag.url = "http://localhost:64497";
            ViewBag.url = "http://192.168.1.77";
            ViewBag.url = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, "");

            DOCUMENTOL dl = dOCUMENTO.DOCUMENTOLs.OrderByDescending(x => x.POS).FirstOrDefault();
            FormatosC fc = new FormatosC();
            ViewBag.monto = fc.toShow((decimal)dOCUMENTO.MONTO_DOC_MD, dOCUMENTO.PAI.DECIMAL);
            ViewBag.mes = dl.FECHAF.Value.Month;
            ViewBag.venta = fc.toShow((decimal)dl.MONTO_VENTA, dOCUMENTO.PAI.DECIMAL);
            DOCUMENTOREC dr = db.DOCUMENTORECs.Where(x => x.DOC_REF == dOCUMENTO.NUM_DOC).FirstOrDefault();
            ViewBag.objetivo = fc.toShow((decimal)dr.MONTO_BASE, dOCUMENTO.PAI.DECIMAL);
            ViewBag.porc = fc.toShowPorc((decimal)dr.PORC, dOCUMENTO.PAI.DECIMAL);
            if (dl.MONTO_VENTA < dr.MONTO_BASE)
            {
                ViewBag.tsol = dOCUMENTO.TSOL.TSOLR;
                ViewBag.nota = false;
            }
            else
            {
                ViewBag.tsol = "";
                ViewBag.nota = true;
            }

            return View(dOCUMENTO);
        }

        // GET: Correos
        public ActionResult Backorder(decimal id)
        {
            var dOCUMENTO = db.DOCUMENTOes.Where(x => x.NUM_DOC == id).FirstOrDefault();
            var flujo = db.FLUJOes.Where(x => x.NUM_DOC == id).OrderByDescending(o => o.POS).Select(s => s.POS).ToList();
            ViewBag.Pos = flujo[0];
            ViewBag.url = "http://localhost:64497";
            ViewBag.url = "http://192.168.1.77";
            ViewBag.url = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, "");

            DOCUMENTOL dl = dOCUMENTO.DOCUMENTOLs.OrderByDescending(x => x.POS).FirstOrDefault();
            FormatosC fc = new FormatosC();
            ViewBag.monto = fc.toShow((decimal)dOCUMENTO.MONTO_DOC_MD, dOCUMENTO.PAI.DECIMAL);
            ViewBag.mes = dl.FECHAF.Value.Month;
            ViewBag.venta = fc.toShow((decimal)dl.MONTO_VENTA, dOCUMENTO.PAI.DECIMAL);
            DOCUMENTOREC dr = db.DOCUMENTORECs.Where(x => x.DOC_REF == dOCUMENTO.NUM_DOC).FirstOrDefault();
            ViewBag.objetivo = fc.toShow((decimal)dr.MONTO_BASE, dOCUMENTO.PAI.DECIMAL);
            ViewBag.porc = fc.toShowPorc((decimal)dr.PORC, dOCUMENTO.PAI.DECIMAL);
            if (dl.MONTO_VENTA < dr.MONTO_BASE)
            {
                ViewBag.tsol = dOCUMENTO.TSOL.TSOLR;
                ViewBag.nota = false;
            }
            else
            {
                ViewBag.tsol = "";
                ViewBag.nota = true;
            }

            return View(dOCUMENTO);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public CLIENTE_MOD SelectCliente(string kunnr)
        {

            TAT001Entities db = new TAT001Entities();

            CLIENTE_MOD id_cl = (from c in db.CLIENTEs
                                 join co in db.CONTACTOCs
                                 on new { c.VKORG, c.VTWEG, c.SPART, c.KUNNR } equals new { co.VKORG, co.VTWEG, co.SPART, co.KUNNR } into jjcont
                                 from co in jjcont.DefaultIfEmpty()
                                 where (c.KUNNR == kunnr & co.DEFECTO == true)
                                 select new CLIENTE_MOD
                                 {
                                     VKORG = c.VKORG,
                                     VTWEG = c.VTWEG,
                                     VTWEG2 = c.VTWEG,//RSG 05.07.2018
                                     SPART = c.SPART,//RSG 28.05.2018-------------------
                                     NAME1 = c.NAME1,
                                     KUNNR = c.KUNNR,
                                     STCD1 = c.STCD1,
                                     PARVW = c.PARVW,
                                     BANNER = c.BANNER,
                                     CANAL = c.CANAL,
                                     PAYER_NOMBRE = co == null ? String.Empty : co.NOMBRE,
                                     PAYER_EMAIL = co == null ? String.Empty : co.EMAIL,
                                 }).FirstOrDefault();

            if (id_cl == null)
            {
                id_cl = (from c in db.CLIENTEs
                         where (c.KUNNR == kunnr)
                         select new CLIENTE_MOD
                         {
                             VKORG = c.VKORG,
                             VTWEG = c.VTWEG,
                             VTWEG2 = c.VTWEG,//RSG 05.07.2018
                             SPART = c.SPART,//RSG 28.05.2018-------------------
                             NAME1 = c.NAME1,
                             KUNNR = c.KUNNR,
                             STCD1 = c.STCD1,
                             PARVW = c.PARVW,
                             BANNER = c.BANNER,
                             CANAL = c.CANAL,
                             PAYER_NOMBRE = String.Empty,
                             PAYER_EMAIL = String.Empty,
                         }).FirstOrDefault();
            }

            if (id_cl != null)
            {
                //Obtener el cliente
                //CANAL canal = db.CANALs.Where(ca => ca.BANNER == id_cl.BANNER && ca.KUNNR == kunnr).FirstOrDefault();
                CANAL canal = db.CANALs.Where(ca => ca.CANAL1 == id_cl.CANAL).FirstOrDefault();
                id_cl.VTWEG = "";
                //if (canal == null)
                //{
                //    string kunnrwz = kunnr.TrimStart('0');
                //    string bannerwz = id_cl.BANNER.TrimStart('0');
                //    canal = db.CANALs.Where(ca => ca.BANNER == bannerwz && ca.KUNNR == kunnrwz).FirstOrDefault();
                //}

                if (canal != null)
                {
                    //id_cl.VTWEG = canal.CANAL1 + " - " + canal.CDESCRIPCION;
                    id_cl.VTWEG = canal.CDESCRIPCION;
                }

                //Obtener el tipo de cliente
                var clientei = (from c in db.TCLIENTEs
                                join ct in db.TCLIENTETs
                                on c.ID equals ct.PARVW_ID
                                where c.ID == id_cl.PARVW && c.ACTIVO == true
                                select ct).FirstOrDefault();
                id_cl.PARVW = "";
                if (clientei != null)
                {
                    id_cl.PARVW = clientei.TXT50;
                }

            }

            return id_cl;
        }
    }
}
