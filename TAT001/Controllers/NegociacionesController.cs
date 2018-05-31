using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TAT001.Entities;


namespace TAT001.Controllers
{
    public class NegociacionesController : Controller
    {
        private TAT001Entities db = new TAT001Entities();

        // GET: Negociaciones
        public ActionResult Index(string pay, string vkorg, string vtweg, string spart,string correo)
        {
            var dOCUMENTOes = db.DOCUMENTOes.Where(x => x.PAYER_ID == pay && x.VKORG == vkorg && x.VTWEG == vtweg && x.SPART == spart && x.FECHAC.Value.Month == DateTime.Now.Month && x.FECHAC.Value.Year == DateTime.Now.Year).Include(d => d.CLIENTE).Include(d => d.PAI).Include(d => d.SOCIEDAD).Include(d => d.TALL).Include(d => d.TSOL).Include(d => d.USUARIO);
            var dx = dOCUMENTOes.ToList();
            List<DOCUMENTO> dd = dx;
            //List<CONTACTOC> send = new List<CONTACTOC>();
            //foreach (var d in dd)
            //{
            //    bool ban = true;
            //    foreach (var d1 in send)
            //    {
            //        if (d1.EMAIL == d.PAYER_EMAIL)
            //            ban = false;
            //    }
            //    if (ban)
            //    {
            //        CONTACTOC c = new CONTACTOC();
            //        c.EMAIL = d.PAYER_EMAIL;
            //        send.Add(c);
            //    }
            //}

            //foreach (CONTACTOC c in send)
            //{
            //    var view = dd.Where(a => a.PAYER_EMAIL == c.EMAIL).ToList();
            //    return View(view.ToList());
            //}
            ViewBag.cliente = db.CLIENTEs.Where(a => a.KUNNR == pay & a.VKORG == vkorg).FirstOrDefault();
            DateTime hoy = DateTime.Now;
            ViewBag.first = new DateTime(hoy.Year, hoy.Month, 1).ToShortDateString();
            ViewBag.last = new DateTime(hoy.Year, hoy.Month, 1).AddMonths(1).AddDays(-1).ToShortDateString();

            return View(dd);
        }

        // GET: Negociaciones/Details/5
        public ActionResult Details(decimal id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            DOCUMENTO dOCUMENTO = db.DOCUMENTOes.Find(id);
            if (dOCUMENTO == null)
            {
                return HttpNotFound();
            }
            return View(dOCUMENTO);
        }

        // GET: Negociaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Negociaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NUM_DOC,TSOL_ID,TALL_ID,SOCIEDAD_ID,PAIS_ID,ESTADO,CIUDAD,PERIODO,EJERCICIO,TIPO_TECNICO,TIPO_RECURRENTE,CANTIDAD_EV,USUARIOC_ID,FECHAD,FECHAC,HORAC,FECHAC_PLAN,FECHAC_USER,HORAC_USER,ESTATUS,ESTATUS_C,ESTATUS_SAP,ESTATUS_WF,DOCUMENTO_REF,CONCEPTO,NOTAS,MONTO_DOC_MD,MONTO_FIJO_MD,MONTO_BASE_GS_PCT_MD,MONTO_BASE_NS_PCT_MD,MONTO_DOC_ML,MONTO_FIJO_ML,MONTO_BASE_GS_PCT_ML,MONTO_BASE_NS_PCT_ML,MONTO_DOC_ML2,MONTO_FIJO_ML2,MONTO_BASE_GS_PCT_ML2,MONTO_BASE_NS_PCT_ML2,PORC_ADICIONAL,IMPUESTO,FECHAI_VIG,FECHAF_VIG,ESTATUS_EXT,SOLD_TO_ID,PAYER_ID,PAYER_NOMBRE,PAYER_EMAIL,GRUPO_CTE_ID,CANAL_ID,MONEDA_ID,MONEDAL_ID,MONEDAL2_ID,TIPO_CAMBIO,TIPO_CAMBIOL,TIPO_CAMBIOL2,NO_FACTURA,FECHAD_SOPORTE,METODO_PAGO,NO_PROVEEDOR,PASO_ACTUAL,AGENTE_ACTUAL,FECHA_PASO_ACTUAL,VKORG,VTWEG,SPART,PUESTO_ID,GALL_ID,CONCEPTO_ID")] DOCUMENTO dOCUMENTO)
        {
            ViewData.Model = dOCUMENTO;
            MailMessage mail = new System.Net.Mail.MailMessage();

            mail.From = new MailAddress("lejgg017@gmail.com");

            mail.To.Add("luisengonzalez25@hotmail.com");
            mail.Subject = "Asunto";

            SmtpClient smtp = new SmtpClient();

            smtp.Host = "smtp.gmail.com";
            smtp.Port = 25; //465; //587
            smtp.Credentials = new NetworkCredential("lejgg017@gmail.com", "24abril14");
            smtp.EnableSsl = true;
            try
            {
                string UrlDirectory = Request.Url.GetLeftPart(UriPartial.Path);
                UrlDirectory = UrlDirectory.Replace("create", "Index");
                UrlDirectory += "?pay=" + dOCUMENTO.PAYER_ID + "&vkorg=" + dOCUMENTO.VKORG + "&vtweg=" + dOCUMENTO.VTWEG + "&spart=" + dOCUMENTO.SPART;
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(UrlDirectory);
                myRequest.Method = "GET";
                WebResponse myResponse = myRequest.GetResponse();
                StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
                string result = sr.ReadToEnd();
                sr.Close();
                myResponse.Close();
                mail.IsBodyHtml = true;
                mail.Body = result;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception("No se ha podido enviar el email", ex.InnerException);
            }
            return View();
        }

        public void mandarCorreo(string pay, string vkorg, string vtweg, string spart, string correo)
        {
            MailMessage mail = new System.Net.Mail.MailMessage();

            mail.From = new MailAddress("lejgg017@gmail.com");

            //mail.To.Add("rogelio.sanchez@sf-solutionfactory.com");
            mail.To.Add(correo);
            mail.Subject = "Asunto";

            SmtpClient smtp = new SmtpClient();

            smtp.Host = "smtp.gmail.com";
            smtp.Port = 25; //465; //587
            smtp.Credentials = new NetworkCredential("lejgg017@gmail.com", "24abril14");
            smtp.EnableSsl = true;
            try
            {
                string UrlDirectory = Request.Url.GetLeftPart(UriPartial.Path);
                UrlDirectory = UrlDirectory.Replace("Edit", "Index");
                UrlDirectory += "?pay=" + pay + "&vkorg=" + vkorg + "&vtweg=" + vtweg + "&spart=" + spart + "&correo=" + correo;
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(UrlDirectory);
                myRequest.Method = "GET";
                WebResponse myResponse = myRequest.GetResponse();
                StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
                string result = sr.ReadToEnd();
                sr.Close();
                myResponse.Close();
                mail.IsBodyHtml = true;
                mail.Body = result;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception("No se ha podido enviar el email", ex.InnerException);
            }
        }
        // GET: Negociaciones/Edit/5
        public ActionResult Edit()
        {
            var fs = db.DOCUMENTOes.Where(f => f.FECHAC.Value.Month == DateTime.Now.Month && f.FECHAC.Value.Year == DateTime.Now.Year).ToList();
            var fs2 = fs.Select(p => new { p.PAYER_ID, p.PAYER_EMAIL, p.VKORG, p.VTWEG, p.SPART }).Distinct().ToList();
            var fs3 = fs.DistinctBy(q => q.PAYER_ID).ToList();
            //for (int i = 0; i < fs2.Count; i++)
            //{
            //    if (fs2[i].PAYER_ID != null && fs2[i].PAYER_EMAIL != null)
            //    {
            //        mandarCorreo(fs2[i].PAYER_ID, fs2[i].VKORG, fs2[i].VTWEG, fs2[i].SPART, fs2[i].PAYER_EMAIL);
            //    }
            //}
            for (int i = 0; i < fs3.Count; i++)
            {
                if (fs3[i].PAYER_ID != null && fs3[i].PAYER_EMAIL != null)
                {
                    mandarCorreo(fs3[i].PAYER_ID, fs3[i].VKORG, fs3[i].VTWEG, fs3[i].SPART, fs3[i].PAYER_EMAIL);
                }
            }
            return RedirectToAction("Index","Home");
        }


        // POST: Negociaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NUM_DOC,TSOL_ID,TALL_ID,SOCIEDAD_ID,PAIS_ID,ESTADO,CIUDAD,PERIODO,EJERCICIO,TIPO_TECNICO,TIPO_RECURRENTE,CANTIDAD_EV,USUARIOC_ID,FECHAD,FECHAC,HORAC,FECHAC_PLAN,FECHAC_USER,HORAC_USER,ESTATUS,ESTATUS_C,ESTATUS_SAP,ESTATUS_WF,DOCUMENTO_REF,CONCEPTO,NOTAS,MONTO_DOC_MD,MONTO_FIJO_MD,MONTO_BASE_GS_PCT_MD,MONTO_BASE_NS_PCT_MD,MONTO_DOC_ML,MONTO_FIJO_ML,MONTO_BASE_GS_PCT_ML,MONTO_BASE_NS_PCT_ML,MONTO_DOC_ML2,MONTO_FIJO_ML2,MONTO_BASE_GS_PCT_ML2,MONTO_BASE_NS_PCT_ML2,PORC_ADICIONAL,IMPUESTO,FECHAI_VIG,FECHAF_VIG,ESTATUS_EXT,SOLD_TO_ID,PAYER_ID,PAYER_NOMBRE,PAYER_EMAIL,GRUPO_CTE_ID,CANAL_ID,MONEDA_ID,MONEDAL_ID,MONEDAL2_ID,TIPO_CAMBIO,TIPO_CAMBIOL,TIPO_CAMBIOL2,NO_FACTURA,FECHAD_SOPORTE,METODO_PAGO,NO_PROVEEDOR,PASO_ACTUAL,AGENTE_ACTUAL,FECHA_PASO_ACTUAL,VKORG,VTWEG,SPART,PUESTO_ID,GALL_ID,CONCEPTO_ID")] DOCUMENTO dOCUMENTO)
        {
            return View();
        }

        // GET: Negociaciones/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOCUMENTO dOCUMENTO = db.DOCUMENTOes.Find(id);
            if (dOCUMENTO == null)
            {
                return HttpNotFound();
            }
            return View(dOCUMENTO);
        }

        // POST: Negociaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            DOCUMENTO dOCUMENTO = db.DOCUMENTOes.Find(id);
            db.DOCUMENTOes.Remove(dOCUMENTO);
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
    }
}
