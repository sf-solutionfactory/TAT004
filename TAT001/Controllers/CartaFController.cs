using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAT001.Entities;
using TAT001.Models;

namespace TAT001.Controllers
{
    public class CartaFController : Controller
    {
        // GET: CartaF
        public ActionResult Index()
        {
            int pagina = 502; //ID EN BASE DE DATOS
            using (TAT001Entities db = new TAT001Entities())
            {
                //string u = User.Identity.Name;
                string u = "admin";
                var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                ViewBag.permisos = db.PAGINAVs.Where(a => a.ID.Equals(user.ID)).ToList();
                ViewBag.carpetas = db.CARPETAVs.Where(a => a.USUARIO_ID.Equals(user.ID)).ToList();
                ViewBag.usuario = user;
                ViewBag.rol = user.MIEMBROS.FirstOrDefault().ROL.NOMBRE;
                ViewBag.Title = db.PAGINAs.Where(a => a.ID.Equals(pagina)).FirstOrDefault().PAGINATs.Where(b => b.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                ViewBag.Title = "Pruebas";
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
            return View();
        }

        // GET: CartaF/Details/5
        public ActionResult Details()
        {
            int pagina = 502; //ID EN BASE DE DATOS
            using (TAT001Entities db = new TAT001Entities())
            {
                //string u = User.Identity.Name;
                string u = "admin";
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
            public ActionResult Create()
        {
            return View();
        }

        // POST: CartaF/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

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
