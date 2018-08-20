﻿using System;
using System.Collections.Generic;
using System.Configuration;
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
        public ActionResult Index(string pay, string vkorg, string vtweg, string spart, string correo, string fi, string ff)
        {
            DOCUMENTOA dz = null;
            List<DOCUMENTO> dx = new List<DOCUMENTO>();
            try
            {
                var _fi = DateTime.Parse(fi);
                var _ff = DateTime.Parse(ff);
                var _idN = db.NEGOCIACIONs.Where(x => x.FECHAI == _fi && x.FECHAF == _ff).FirstOrDefault().ID;
                //var dOCUMENTOes = db.DOCUMENTOes.Where(x => x.PAYER_ID == pay && x.VKORG == vkorg && x.VTWEG == vtweg && x.SPART == spart && x.PAYER_EMAIL == correo && ((x.FECHAC.Value.Day >= _fi.Day && x.FECHAC.Value.Day <= _ff.Day) && x.FECHAC.Value.Month == _ff.Month && x.FECHAC.Value.Year == _ff.Year)).Include(d => d.CLIENTE).Include(d => d.PAI).Include(d => d.SOCIEDAD).Include(d => d.TALL).Include(d => d.TSOL).Include(d => d.USUARIO).ToList();
                var dOCUMENTOes = db.DOCUMENTOes.Where(x => x.PAYER_ID == pay && x.VKORG == vkorg && x.VTWEG == vtweg && x.SPART == spart && x.PAYER_EMAIL == correo && ((x.FECHAC >= _fi && x.FECHAC <= _ff))).Include(d => d.CLIENTE).Include(d => d.PAI).Include(d => d.SOCIEDAD).Include(d => d.TALL).Include(d => d.TSOL).Include(d => d.USUARIO).ToList();
                for (int i = 0; i < dOCUMENTOes.Count; i++)
                {
                    //si el documentoref es nullo, significa que no depende de alguno otro
                    if (dOCUMENTOes[i].DOCUMENTO_REF == null)
                    {
                        //recupero el numdoc
                        var de = dOCUMENTOes[i].NUM_DOC;
                        //sino ecuentra una coincidencia con el criterio discriminatorio se agregan o no a la lista
                        dz = db.DOCUMENTOAs.Where(x => x.NUM_DOC == de && x.CLASE != "OTR").FirstOrDefault();
                        if (dz == null || dz != null)
                        {
                            if (dOCUMENTOes[i].TSOL.NEGO == true)//para el ultimo filtro
                            {
                                string estatus = "";
                                if (dOCUMENTOes[i].ESTATUS != null) { estatus += dOCUMENTOes[i].ESTATUS; } else { estatus += " "; }
                                if (dOCUMENTOes[i].ESTATUS_C != null) { estatus += dOCUMENTOes[i].ESTATUS_C; } else { estatus += " "; }
                                if (dOCUMENTOes[i].ESTATUS_SAP != null) { estatus += dOCUMENTOes[i].ESTATUS_SAP; } else { estatus += " "; }
                                if (dOCUMENTOes[i].ESTATUS_WF != null) { estatus += dOCUMENTOes[i].ESTATUS_WF; } else { estatus += " "; }
                                if (dOCUMENTOes[i].FLUJOes.Count > 0)
                                {
                                    estatus += dOCUMENTOes[i].FLUJOes.OrderByDescending(a => a.POS).FirstOrDefault().WORKFP.ACCION.TIPO;
                                }
                                else
                                {
                                    estatus += " ";
                                }
                                if (dOCUMENTOes[i].TSOL.PADRE) { estatus += "P"; } else { estatus += " "; }
                                if (dOCUMENTOes[i].FLUJOes.Where(x => x.ESTATUS == "R").ToList().Count > 0)
                                {
                                    estatus += dOCUMENTOes[i].FLUJOes.Where(x => x.ESTATUS == "R").OrderByDescending(a => a.POS).FirstOrDefault().USUARIO.PUESTO_ID;
                                }
                                else
                                {
                                    estatus += " ";
                                }

                                if (System.Text.RegularExpressions.Regex.IsMatch(estatus, "...[P][R].."))
                                    dx.Add(dOCUMENTOes[i]);
                                else if (System.Text.RegularExpressions.Regex.IsMatch(estatus, "...[R]..[8]"))
                                    dx.Add(dOCUMENTOes[i]);
                                else if (System.Text.RegularExpressions.Regex.IsMatch(estatus, "[P]..[A]..."))
                                    dx.Add(dOCUMENTOes[i]);
                                else if (System.Text.RegularExpressions.Regex.IsMatch(estatus, "..[P][A]..."))
                                    dx.Add(dOCUMENTOes[i]);
                                else if (System.Text.RegularExpressions.Regex.IsMatch(estatus, "..[E][A]..."))
                                    dx.Add(dOCUMENTOes[i]);
                                else if (System.Text.RegularExpressions.Regex.IsMatch(estatus, "...[A].[P]."))
                                    dx.Add(dOCUMENTOes[i]);
                                else if (System.Text.RegularExpressions.Regex.IsMatch(estatus, "...[A]..."))
                                    dx.Add(dOCUMENTOes[i]);
                                else if (System.Text.RegularExpressions.Regex.IsMatch(estatus, "...[T]..."))
                                    dx.Add(dOCUMENTOes[i]);
                                //if (dOCUMENTOes[i].ESTATUS_WF == "P")//LEJ 19.07.2018---------------------------I
                                //{
                                //    if (dOCUMENTOes[i].FLUJOes.Count > 0)
                                //    {
                                //        if (dOCUMENTOes[i].FLUJOes.OrderByDescending(a => a.POS).FirstOrDefault().USUARIO != null)
                                //        {
                                //            //(Pendiente Validación TS)
                                //            if (dOCUMENTOes[i].FLUJOes.OrderByDescending(a => a.POS).FirstOrDefault().USUARIO.PUESTO_ID == 8)
                                //            {
                                //                dx.Add(dOCUMENTOes[i]);
                                //            }
                                //        }
                                //    }
                                //}
                                //else if (dOCUMENTOes[i].ESTATUS_WF == "R")//(Pendiente Corrección)
                                //{
                                //    if (dOCUMENTOes[i].FLUJOes.Count > 0)
                                //    {
                                //        dx.Add(dOCUMENTOes[i]);
                                //    }
                                //}
                                //else if (dOCUMENTOes[i].ESTATUS_WF == "T")//(Pendiente Taxeo)
                                //{
                                //    if (dOCUMENTOes[i].TSOL_ID == "NCIA")
                                //    {
                                //        if (dOCUMENTOes[i].PAIS_ID == "CO")//(sólo Colombia)
                                //        {
                                //            dx.Add(dOCUMENTOes[i]);
                                //        }
                                //    }
                                //}
                                //else if (dOCUMENTOes[i].ESTATUS_WF == "A")//(Por Contabilizar)
                                //{
                                //    if (dOCUMENTOes[i].ESTATUS == "P")
                                //    {
                                //        dx.Add(dOCUMENTOes[i]);
                                //    }
                                //}
                                //else if (dOCUMENTOes[i].ESTATUS_SAP == "E")//Error en SAP
                                //{
                                //    // dx.Add(dOCUMENTOes[i]);
                                //}
                                //else if (dOCUMENTOes[i].ESTATUS_SAP == "X")//Succes en SAP
                                //{
                                //    dx.Add(dOCUMENTOes[i]);
                                //}
                            }
                            //LEJ 19.07.2018----------------------------------------------------------------T
                            // dx.Add(dOCUMENTOes[i]);
                        }
                    }
                }
                if (dx.Count > 0)
                {
                    var uId = dx[0].USUARIOC_ID;
                    var clUsu = db.USUARIOs.Where(x => x.ID == uId).FirstOrDefault();
                    var clSoc = dx[0].SOCIEDAD_ID;
                    int n = 0;
                    var isNumeric = int.TryParse(dx[0].CIUDAD, out n);
                    var clCd = "";
                    var clEdo = "";
                    if (isNumeric)
                    {
                        int c = Convert.ToInt32(dx[0].CIUDAD);
                        var cd = db.CITIES.Where(i => i.ID == c).FirstOrDefault();
                        var edo = db.STATES.Where(i => i.ID == cd.STATE_ID).FirstOrDefault();
                        clCd = cd.NAME;
                        clEdo = edo.NAME;
                    }
                    else
                    {
                        clCd = dx[0].CIUDAD;
                        clEdo = dx[0].ESTADO;
                    }
                    ViewBag.clCorreo = clUsu.EMAIL;
                    var cl = db.CLIENTEs.Where(a => a.KUNNR == pay & a.VKORG == vkorg).FirstOrDefault();
                    ViewBag.clCon = cl.CONTAC;
                    ViewBag.clName = cl.NAME1;
                    ViewBag.clDir = cl.STRAS_GP;
                    DateTime hoy = DateTime.Now;
                    ViewBag.fi = fi;
                    ViewBag.ff = ff;
                    ViewBag.clPayId = pay;
                    ViewBag.clFunci = clUsu.NOMBRE + " " + clUsu.APELLIDO_P + " " + clUsu.APELLIDO_M;
                    ViewBag.clPos = db.PUESTOTs.Where(x => x.PUESTO_ID == clUsu.PUESTO_ID && x.SPRAS_ID == "ES").Select(s => s.TXT50).FirstOrDefault();
                    ViewBag.FechaH = DateTime.Now.ToShortDateString();
                    ViewBag.KellCom = db.SOCIEDADs.Where(s => s.BUKRS == clSoc).Select(r => r.NAME1).FirstOrDefault();
                    ViewBag.cd = clCd;
                    ViewBag.edo = clEdo;
                    ViewBag.idf = _idN;
                    ViewBag.vk = vkorg;
                    ViewBag.vtw = vtweg;
                    ViewBag.clCorreo2 = correo.Replace('@', '/').Replace('.', '*').Replace('-', '#');
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return View(dx);
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
                //smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception("No se ha podido enviar el email", ex.InnerException);
            }
            return View();
        }

        public void mandarCorreo(string pay, string vkorg, string vtweg, string spart, string correo)
        {
            ////MailMessage mail = new System.Net.Mail.MailMessage();

            ////mail.From = new MailAddress("lejgg017@gmail.com");

            //////mail.To.Add("rogelio.sanchez@sf-solutionfactory.com");
            ////mail.To.Add(correo);
            ////mail.Subject = "Asunto";

            ////SmtpClient smtp = new SmtpClient();

            ////smtp.Host = "smtp.gmail.com";
            ////smtp.Port = 25; //465; //587
            ////smtp.Credentials = new NetworkCredential("lejgg017@gmail.com", "24abril14");
            ////smtp.EnableSsl = true;

            string mailt = ConfigurationManager.AppSettings["mailt"];
            string mtest = ConfigurationManager.AppSettings["mailtest"];
            string mailTo = "";
            if (mtest == "X")
                mailTo = "rogelio.sanchez@sf-solutionfactory.com";
            else
                mailTo = correo;
            CONMAIL conmail = db.CONMAILs.Find(mailt);
            if (conmail != null)
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(conmail.MAIL, mailTo);
                SmtpClient client = new SmtpClient();
                if (conmail.SSL)
                {
                    client.Port = (int)conmail.PORT;
                    client.EnableSsl = conmail.SSL;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(conmail.MAIL, conmail.PASS);
                }
                else
                {
                    client.UseDefaultCredentials = true;
                    client.Credentials = new NetworkCredential(conmail.MAIL, conmail.PASS);
                }
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Host = conmail.HOST;


                //mail.To.Add("rogelio.sanchez@sf-solutionfactory.com");
                //mail.To.Add(correo);
                mail.Subject = "Negociaciones cliente: " + pay;

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
                    // client.Send(mail);
                }
                catch (Exception ex)
                {
                    throw new Exception("No se ha podido enviar el email", ex.InnerException);
                }
            }
        }
        // GET: Negociaciones/Edit/5
        public ActionResult Edit()
        {
            var fs = db.DOCUMENTOes.Where(f => f.FECHAC.Value.Month == DateTime.Now.Month && f.FECHAC.Value.Year == DateTime.Now.Year && f.DOCUMENTO_REF == null).ToList();
            var fs3 = fs.DistinctBy(q => q.PAYER_ID).ToList();
            for (int i = 0; i < fs3.Count; i++)
            {
                if (fs3[i].PAYER_ID != null && fs3[i].PAYER_EMAIL != null)
                {
                    var de = fs3[i].NUM_DOC;
                    var dsa = db.DOCUMENTOAs.Where(x => x.NUM_DOC == de && x.CLASE != "OTR").FirstOrDefault();
                    if (dsa == null || dsa != null)
                    {
                        mandarCorreo(fs3[i].PAYER_ID, fs3[i].VKORG, fs3[i].VTWEG, fs3[i].SPART, fs3[i].PAYER_EMAIL);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
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
