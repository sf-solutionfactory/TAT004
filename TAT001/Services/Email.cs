using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TAT001.Entities;

namespace TAT001.Services
{
    public class Email
    {
        private TAT001Entities db = new TAT001Entities();
        public void enviaMailC(decimal id, bool ban, string spras, string UrlDirectory)
        {
            //int pagina = 203; //ID EN BASE DE DATOS
            //ViewBag.Title = "Solicitud";

            if (id != 0)
            {
                DOCUMENTO dOCUMENTO = db.DOCUMENTOes.Find(id);
                if (dOCUMENTO != null)
                {

                    dOCUMENTO.CLIENTE = db.CLIENTEs.Where(a => a.VKORG.Equals(dOCUMENTO.VKORG)
                                                            & a.VTWEG.Equals(dOCUMENTO.VTWEG)
                                                            & a.SPART.Equals(dOCUMENTO.SPART)
                                                            & a.KUNNR.Equals(dOCUMENTO.PAYER_ID)).First();
                    var workflow = db.FLUJOes.Where(a => a.NUM_DOC.Equals(id)).OrderByDescending(a => a.POS).FirstOrDefault();
                    //ViewBag.acciones = db.FLUJOes.Where(a => a.NUM_DOC.Equals(id) & a.ESTATUS.Equals("P") & a.USUARIOA_ID.Equals(User.Identity.Name)).FirstOrDefault();

                    string mailt = ConfigurationManager.AppSettings["mailt"];
                    CONMAIL conmail = db.CONMAILs.Find(mailt);
                    if (conmail != null)
                    {
                        MailMessage mail = new MailMessage(conmail.MAIL, "rogelio.sanchez@sf-solutionfactory.com");
                        SmtpClient client = new SmtpClient();
                        client.Port = (int)conmail.PORT;
                        client.EnableSsl = conmail.SSL;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = conmail.HOST;
                        client.Credentials = new NetworkCredential(conmail.MAIL, conmail.PASS);


                        if (workflow == null)
                            mail.Subject = "N" + dOCUMENTO.NUM_DOC + "-" + DateTime.Now.ToShortTimeString();
                        else
                            mail.Subject = workflow.ESTATUS + dOCUMENTO.NUM_DOC + "-" + DateTime.Now.ToShortTimeString();
                        mail.IsBodyHtml = true;
                        //UrlDirectory = UrlDirectory.Substring(0, UrlDirectory.LastIndexOf("/"));
                        UrlDirectory = UrlDirectory.Replace("Solicitudes/Create", "Correos/Index");
                        UrlDirectory += "/" + dOCUMENTO.NUM_DOC;
                        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(UrlDirectory);
                        myRequest.Method = "GET";
                        WebResponse myResponse = myRequest.GetResponse();
                        StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
                        string result = sr.ReadToEnd();
                        sr.Close();
                        myResponse.Close();

                        mail.Body = result;

                        client.Send(mail);

                    }

                }
            }
            //return View(dOCUMENTO);
        }
    }
}