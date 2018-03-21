﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAT001.Entities;
using TAT001.Models;
namespace TAT001.Controllers
{
    [Authorize]
    public class CartaFController : Controller
    {
        // GET: CartaF
        public ActionResult Index(decimal id)
        {
            int pagina = 230; //ID EN BASE DE DATOS
            using (TAT001Entities db = new TAT001Entities())
            {
                string u = User.Identity.Name;
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
                Session["spras"] = user.SPRAS_ID;
            }
            return View();
        }

        // GET: CartaF/Details/5
        public ActionResult Details()
        {
            int pagina = 232; //ID EN BASE DE DATOS
            using (TAT001Entities db = new TAT001Entities())
            {
                string u = User.Identity.Name;
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
                    ViewBag.pais = "mx.svg";
                    //return RedirectToAction("Pais", "Home");
                }
                Session["spras"] = user.SPRAS_ID;
            }
            CartaF cf = new CartaF();
            cf.company = "Kellogs";
            cf.company_x = true;
            cf.taxid = "ASD7806B67";
            cf.taxid_x = true;
            cf.concepto = "Julio Regalado";
            cf.concepto_x = true;
            cf.cliente = "Oscar Ramírez";
            cf.cliente_x = true;
            cf.puesto = "Gerente de compras";
            cf.puesto_x = true;
            cf.direccion = "Km 1.1 San Francisco, MX";
            cf.direccion_x = true;
            cf.folio = "100011";
            cf.folio_x = true;
            cf.lugar = "Qro, Qro. 14/02/2018";
            cf.lugar_x = true;
            cf.payer = "Walmart México";
            cf.payer_x = true;
            cf.estimado = "Julio Rosales";
            cf.estimado_x = true;
            cf.mecanica = "Detalle de la mecánica con cálculo, objetivo, %. \r\nVarias líneas";
            cf.mecanica_x = true;
            cf.nombreE = "Jorge Escamilla M";
            cf.nombreE_x = true;
            cf.puestoE = "Gerente de ventas comercial";
            cf.puestoE_x = true;
            cf.companyC = "Kelloggs MX SA de CV";
            cf.companyC_x = true;
            cf.nombreC = "Brenda Arrieta";
            cf.nombreC_x = true;
            cf.puestoC = "Gerente de compras";
            cf.puestoC_x = true;
            cf.companyCC = "Walmart MX";
            cf.companyCC_x = true;
            cf.legal = "“Toda la información comercial y/o publicitaria contenida en el presente comunicado, es de aplicación y/o referencia exclusiva para la República Mexicana y para surtir efectos en las cadenas de autoservicio del destinatario del mismo y/o establecidas expresamente por Kellogg Company México, S. de R. L. de C.V.”";
            cf.legal_x = true;
            cf.mail = "El contenido del presente Acuerdo será enviado electrónicamente al correo:";
            cf.mail_x = true;
            return View(cf);
        }

        // GET: CartaF/Details/5
        [HttpPost]
        public ActionResult Details(CartaF c)
        {
            CartaF carta = c;
            return RedirectToAction("Index");
        }
        // GET: CartaF/Create
        public ActionResult Create(decimal id)
        {
            int pagina = 231; //ID EN BASE DE DATOS
            using (TAT001Entities db = new TAT001Entities())
            {
                string u = User.Identity.Name;
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
                    string pa = Session["pais"].ToString();
                    ViewBag.pais = pa + ".svg";
                }
                catch
                {
                    ViewBag.pais = "mx.svg";
                    //return RedirectToAction("Pais", "Home");
                }
                Session["spras"] = user.SPRAS_ID;
            }
            DOCUMENTO d = new DOCUMENTO();
            LEYENDA l = new LEYENDA();
            PUESTOT p = new PUESTOT();
            using (TAT001Entities db = new TAT001Entities())
            {
                d = db.DOCUMENTOes.Include("SOCIEDAD").Include("USUARIO").Where(a => a.NUM_DOC.Equals(id)).First();
                //var dOCUMENTOes = db.DOCUMENTOes.Where(a => a.USUARIOC_ID.Equals(User.Identity.Name)).Include(doa => doa.TALL).Include(d => d.TSOL).Include(d => d.USUARIO).Include(d => d.CLIENTE).Include(d => d.PAI).Include(d => d.SOCIEDAD);
                if (d != null)
                {
                    //var aa = db.CLIENTEs.Where(a => a.VKORG.Equals(d.VKORG)
                    d.CLIENTE = db.CLIENTEs.Where(a => a.VKORG.Equals(d.VKORG)
                                                              & a.VTWEG.Equals(d.VTWEG)
                                                            & a.SPART.Equals(d.SPART)
                                                            & a.KUNNR.Equals(d.PAYER_ID)).First();
                    string sp = Session["spras"].ToString();
                    p = db.PUESTOTs.Where(a => a.SPRAS_ID.Equals(sp) && a.PUESTO_ID == d.USUARIO.PUESTO_ID).FirstOrDefault();
                }
                l = db.LEYENDAs.Where(a => a.PAIS_ID.Equals(d.PAIS_ID) && a.ACTIVO == true).First();
            }
            CartaF cf = new CartaF();
            cf.num_doc = id;
            cf.company = d.SOCIEDAD.BUTXT;
            cf.company_x = true;
            cf.taxid = d.SOCIEDAD.LAND;
            cf.taxid_x = true;
            cf.concepto = d.CONCEPTO;
            cf.concepto_x = true;
            cf.cliente = d.PAYER_NOMBRE;
            cf.cliente_x = true;
            cf.puesto = " ";
            cf.puesto_x = false;
            cf.direccion = d.CLIENTE.STRAS_GP;
            cf.direccion_x = true;
            cf.folio = d.NUM_DOC.ToString();
            cf.folio_x = true;
            //cf.lugar = "Qro, Qro."+DateTime.Now.ToShortTimeString();
            cf.lugar = d.CIUDAD.Trim() + ", " + d.ESTADO.Trim() + ". " + DateTime.Now.ToShortDateString();
            cf.lugar_x = true;
            cf.payer = d.CLIENTE.NAME1;
            cf.payer_x = true;
            cf.estimado = d.PAYER_NOMBRE;
            cf.estimado_x = true;
            cf.mecanica = "";
            cf.mecanica_x = true;
            cf.nombreE = d.USUARIO.NOMBRE + " " + d.USUARIO.APELLIDO_P + " " + d.USUARIO.APELLIDO_M;
            cf.nombreE_x = true;
            cf.puestoE = p.TXT50;
            cf.puestoE_x = true;
            cf.companyC = cf.company;
            cf.companyC_x = true;
            cf.nombreC = d.PAYER_NOMBRE;
            cf.nombreC_x = true;
            cf.puestoC = " ";
            cf.puestoC_x = false;
            cf.companyCC = d.CLIENTE.NAME1;
            cf.companyCC_x = true;
            cf.legal = l.LEYENDA1;
            cf.legal_x = true;
            cf.mail = "El contenido del presente Acuerdo será enviado electrónicamente al correo: " + d.PAYER_EMAIL;
            cf.mail_x = true;
            return View(cf);
        }

        // POST: CartaF/Create
        [HttpPost]
        public ActionResult Create(CartaF c)
        {
            try
            {
                CARTA ca = new CARTA();
                ca.NUM_DOC = c.num_doc;
                ca.CLIENTE = c.cliente;
                ca.CLIENTEX = c.cliente_x;
                ca.COMPANY = c.company;
                ca.COMPANYC = c.companyC;
                ca.COMPANYCC = c.companyCC;
                ca.COMPANYCCX = c.companyCC_x;
                ca.COMPANYCX = c.companyC_x;
                ca.COMPANYX = c.company_x;
                ca.CONCEPTO = c.concepto;
                ca.CONCEPTOX = c.concepto_x;
                ca.DIRECCION = c.direccion;
                ca.DIRECCIONX = c.direccion_x;
                //ca.DOCUMENTO = c.DOCUMENTO;
                ca.ESTIMADO = c.estimado;
                ca.ESTIMADOX = c.estimado_x;
                //ca.FECHAC = c.FECHAC;
                ca.FOLIO = c.folio;
                ca.FOLIOX = c.folio_x;
                ca.LEGAL = c.legal;
                ca.LEGALX = c.legal_x;
                ca.LUGAR = c.lugar;
                ca.LUGARX = c.lugar_x;
                ca.MAIL = c.mail;
                ca.MAILX = c.mail_x;
                ca.MECANICA = c.mecanica;
                ca.MECANICAX = c.mecanica_x;
                ca.NOMBREC = c.nombreC;
                ca.NOMBRECX = c.nombreC_x;
                ca.NOMBREE = c.nombreE;
                ca.NOMBREEX = c.nombreE_x;
                ca.NUM_DOC = c.num_doc;
                ca.PAYER = c.payer;
                ca.PAYERX = c.payer_x;
                ca.PUESTO = c.puesto;
                ca.PUESTOC = c.puestoC;
                ca.PUESTOCX = c.puestoC_x;
                ca.PUESTOE = c.puestoE;
                ca.PUESTOEX = c.puestoE_x;
                ca.PUESTOX = c.puestoE_x;
                ca.TAXID = c.taxid;
                ca.TAXIDX = c.taxid_x;
                //ca.TIPO = c.TIPO;
                //ca.USUARIO = c.USUARIO;
                //ca.USUARIO_ID = c.USUARIO_ID;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CartaF/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartaF/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CartaF/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartaF/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
