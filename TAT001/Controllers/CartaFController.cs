using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
                TempData["id"] = id;
                var lista = db.CARTAs.Where(a => a.NUM_DOC.Equals(id)).ToList();
                return View(lista);
            }
        }

        // GET: CartaF/Details/5
        public ActionResult Details(string ruta)
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

            ViewBag.url = ruta;
            return View();
        }

        // GET: CartaF/Details/5
        [HttpPost]
        public ActionResult Details(CartaF c)
        {
            CartaF carta = c;
            return RedirectToAction("Index");
        }
        // GET: CartaF/Create
        public ActionResult Detailss(decimal id, int pos)
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
                ViewBag.mon = db.TEXTOCARTAVs.Where(t => t.SPRAS_ID.Equals(user.SPRAS_ID)).Select(t => t.MONTO).FirstOrDefault();

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
            CARTA c = new CARTA();
            using (TAT001Entities db = new TAT001Entities())
            {
                c = db.CARTAs.Where(a => a.NUM_DOC.Equals(id) & a.POS.Equals(pos)).First();
            }
            CartaF cf = new CartaF();
            cf.num_doc = id;
            cf.company = c.COMPANY;
            cf.company_x = (bool)c.COMPANYX;
            cf.taxid = c.TAXID;
            cf.taxid_x = (bool)c.TAXIDX;
            cf.concepto = c.CONCEPTO;
            cf.concepto_x = (bool)c.CONCEPTOX;
            cf.cliente = c.CLIENTE;
            cf.cliente_x = (bool)c.CLIENTEX;
            cf.puesto = c.PUESTO;
            cf.puesto_x = (bool)c.PUESTOX;
            cf.direccion = c.DIRECCION;
            cf.direccion_x = (bool)c.DIRECCIONX;
            cf.folio = c.FOLIO;
            cf.folio_x = (bool)c.FOLIOX;
            cf.lugar = c.LUGAR;
            cf.lugar_x = (bool)c.LUGARX;
            cf.lugarFech = c.LUGARFECH;
            cf.lugarFech_x = (bool)c.LUGARFECHX;
            cf.payerId = c.PAYER;
            cf.payerId_x = (bool)c.PAYERX;
            cf.payerNom = c.NOMBREC;
            cf.payerNom_x = (bool)c.NOMBRECX;
            cf.estimado = c.ESTIMADO;
            cf.estimado_x = (bool)c.ESTIMADOX;
            cf.mecanica = c.MECANICA;
            cf.mecanica_x = (bool)c.MECANICAX;
            cf.monto = c.MONEDA;
            cf.monto = c.MONTO;
            cf.nombreE = c.NOMBREE;
            cf.nombreE_x = (bool)c.NOMBREEX;
            cf.puestoE = c.PUESTOE;
            cf.puestoE_x = (bool)c.PUESTOEX;
            cf.companyC = c.COMPANYC;
            cf.companyC_x = (bool)c.COMPANYCX;
            cf.nombreC = c.NOMBREC;
            cf.nombreC_x = (bool)c.NOMBRECX;
            cf.puestoC = c.PUESTOC;
            cf.puestoC_x = (bool)c.PUESTOCX;
            cf.companyCC = c.COMPANYCC;
            cf.companyCC_x = (bool)c.COMPANYCCX;
            cf.legal = c.LEGAL;
            cf.legal_x = (bool)c.LEGALX;
            cf.mail = c.MAIL;
            cf.mail_x = (bool)c.MAILX;
            return View(cf);
        }
        // GET: CartaF/Create
        public ActionResult Create(decimal id)
        {
            int pagina = 231; //ID EN BASE DE DATOS
            TEXTOCARTAF c = new TEXTOCARTAF();
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
                ViewBag.mon = db.TEXTOCARTAVs.Where(t => t.SPRAS_ID.Equals(user.SPRAS_ID)).Select(t => t.MONTO).FirstOrDefault();

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

                c = db.TEXTOCARTAFs
                        .Where(x => x.SPRAS_ID == user.SPRAS_ID)
                        .First();
            }

            DOCUMENTO d = new DOCUMENTO();
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
                    ////d.CITy.STATE.NAME = db.STATES.Where(a => a.ID.Equals(d.CITy.STATE_ID)).FirstOrDefault().NAME;
                }
                ViewBag.legal = db.LEYENDAs.Where(a => a.PAIS_ID.Equals(d.PAIS_ID) && a.ACTIVO == true).FirstOrDefault();

                HeaderFooter hfc = new HeaderFooter();
                hfc.eliminaArchivos();

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
                //cf.lugar = "Qro, Qro."+ DateTime.Now.ToShortTimeString();
                cf.lugarFech = DateTime.Now.ToShortDateString();
                cf.lugarFech_x = true;
                cf.lugar = d.CIUDAD.Trim() + ", " + d.ESTADO.Trim();
                ////cf.lugar = d.CITy.NAME + ", " + d.CITy.STATE.NAME;
                cf.lugar_x = true;
                cf.payerId = d.CLIENTE.PAYER;
                cf.payerId_x = true;
                cf.payerNom = d.CLIENTE.NAME1;
                cf.payerNom_x = true;
                cf.estimado = d.PAYER_NOMBRE;
                cf.estimado_x = true;
                cf.mecanica = d.NOTAS;
                cf.mecanica_x = true;
                cf.nombreE = d.USUARIO.NOMBRE + " " + d.USUARIO.APELLIDO_P + " " + d.USUARIO.APELLIDO_M;
                cf.nombreE_x = true;
                if (p != null)
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
                if (ViewBag.legal != null)
                    cf.legal = ViewBag.legal.LEYENDA1;
                cf.legal_x = true;
                cf.mail = c.E_MAIL + " " + d.PAYER_EMAIL;
                cf.mail_x = true;
                cf.monto = d.MONTO_DOC_MD.ToString();
                cf.moneda = d.MONEDA_ID.ToString();
                return View(cf);
            }
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
                ca.LUGARFECH = c.lugarFech;
                ca.LUGARFECHX = c.lugarFech_x;
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
                ca.PAYER = c.payerId;
                ca.PAYERX = c.payerId_x;
                ca.PUESTO = c.puesto;
                ca.PUESTOC = c.puestoC;
                ca.PUESTOCX = c.puestoC_x;
                ca.PUESTOE = c.puestoE;
                ca.PUESTOEX = c.puestoE_x;
                ca.PUESTOX = c.puestoE_x;
                ca.TAXID = c.taxid;
                ca.TAXIDX = c.taxid_x;
                ca.MONTO = c.monto;
                ca.MONEDA = c.moneda;
                //ca.TIPO = c.TIPO;
                //ca.USUARIO = c.USUARIO;
                //ca.USUARIO_ID = c.USUARIO_ID;
                ca.USUARIO_ID = User.Identity.Name;
                ca.FECHAC = DateTime.Now;

                CartaFEsqueleto cfe = new CartaFEsqueleto();
                TEXTOCARTAF f = new TEXTOCARTAF();
                string u = User.Identity.Name;
                string recibeRuta = "";
                using (TAT001Entities db = new TAT001Entities())
                {
                    var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                    var cartas = db.CARTAs.Where(a => a.NUM_DOC.Equals(ca.NUM_DOC)).ToList();
                    int pos = 0;
                    if (cartas.Count > 0)
                        pos = cartas.OrderByDescending(a => a.POS).First().POS;
                    ca.POS = pos + 1;
                    db.CARTAs.Add(ca);
                    db.SaveChanges();

                    f = db.TEXTOCARTAFs
                        .Where(x => x.SPRAS_ID == user.SPRAS_ID)
                        .First();
                }

                cfe.crearPDF(c, f);
                recibeRuta = Convert.ToString(Session["rutaCompletaf"]);
                return RedirectToAction("Details", new { ruta = recibeRuta });
            }
            catch (DbEntityValidationException e)
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
                    DOCUMENTO d = db.DOCUMENTOes.Include("SOCIEDAD").Include("USUARIO").Where(a => a.NUM_DOC.Equals(c.num_doc)).First();
                    ViewBag.legal = db.LEYENDAs.Where(a => a.PAIS_ID.Equals(d.PAIS_ID) && a.ACTIVO == true).FirstOrDefault();
                }
                try
                {
                    ViewBag.error = e.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage;
                }
                catch
                {
                    ViewBag.error = e.Message;
                }
                return View(c);
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
