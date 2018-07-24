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
    [AllowAnonymous]
    public class CorreosController : Controller
    {
        private TAT001Entities db = new TAT001Entities();

        // GET: Correos
        public ActionResult Index(decimal id)
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
            ViewBag.url = Request.Url.AbsoluteUri.Replace(Request.Url.AbsolutePath, "");

            DOCUMENTOL dl = dOCUMENTO.DOCUMENTOLs.OrderByDescending(x => x.POS).FirstOrDefault();
            FormatosC fc = new FormatosC();
            ViewBag.monto = fc.toShow((decimal)dOCUMENTO.MONTO_DOC_MD, dOCUMENTO.PAI.DECIMAL);
            ViewBag.mes = dl.FECHAF.Value.Month;
            ViewBag.venta = fc.toShow((decimal)dl.MONTO_VENTA, dOCUMENTO.PAI.DECIMAL);
            DOCUMENTOREC dr =  db.DOCUMENTORECs.Where(x => x.DOC_REF==dOCUMENTO.NUM_DOC).FirstOrDefault();
            ViewBag.objetivo = fc.toShow((decimal)dr.MONTO_BASE, dOCUMENTO.PAI.DECIMAL);
            ViewBag.porc = fc.toShowPorc((decimal)dr.PORC, dOCUMENTO.PAI.DECIMAL);
            if(dl.MONTO_VENTA < dr.MONTO_BASE)
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
    }
}
