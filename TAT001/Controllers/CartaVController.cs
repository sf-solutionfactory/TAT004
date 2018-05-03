using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAT001.Entities;
using TAT001.Models;

namespace TAT001.Controllers
{
    [Authorize]
    public class CartaVController : Controller
    {
        // GET: CartaV
        public ActionResult Index(string ruta)
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
            }
            ViewBag.url = ruta;
            return View();
        }

        // GET: CartaV/Details/5
        public ActionResult Details(string ruta)
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
            }
            ViewBag.url = ruta;
            return View();
        }

        // GET: CartaV/Details/5
        public ActionResult Create(decimal id)
        {
            int pagina = 232; //ID EN BASE DE DATOS
            TEXTOCARTAV c = new TEXTOCARTAV();
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
                ViewBag.de = db.TEXTOCARTAVs.Where(t => t.SPRAS_ID.Equals(user.SPRAS_ID)).Select(t => t.DE).FirstOrDefault();
                ViewBag.al = db.TEXTOCARTAVs.Where(t => t.SPRAS_ID.Equals(user.SPRAS_ID)).Select(t => t.A).FirstOrDefault();
                ViewBag.mon = db.TEXTOCARTAVs.Where(t => t.SPRAS_ID.Equals(user.SPRAS_ID)).Select(t => t.MONTO).FirstOrDefault();

                try
                {
                    string p = Session["pais"].ToString();
                    ViewBag.pais = p + ".svg";
                }
                catch
                {
                    //ViewBag.pais = "mx.svg";
                    return RedirectToAction("Pais", "Home");
                }

                List<string> lista = new List<string>();
                List<string> armadoCuerpoTab = new List<string>();
                List<int> numfilasTabla = new List<int>();

                int contadorTabla = 0;

                var con = db.DOCUMENTOPs.Select(x => new { x.NUM_DOC, x.VIGENCIA_DE, x.VIGENCIA_AL }).Where(a => a.NUM_DOC.Equals(id)).GroupBy(f => new { f.VIGENCIA_DE, f.VIGENCIA_AL }).ToList();

                foreach (var item in con)
                {
                    lista.Add(item.Key.VIGENCIA_DE.ToString() + item.Key.VIGENCIA_AL.ToString());
                }

                for (int i = 0; i < lista.Count; i++)
                {
                    contadorTabla = 0;

                    //DateTime a1 = DateTime.Parse(lista[i].Remove(24));
                    DateTime a1 = DateTime.Parse(lista[i].Remove(lista[i].Length / 2));
                    //DateTime a2 = DateTime.Parse(lista[i].Remove(0, 24));
                    DateTime a2 = DateTime.Parse(lista[i].Remove(0, lista[i].Length / 2));

                    var con2 = db.DOCUMENTOPs
                                          .Where(x => x.NUM_DOC.Equals(id) & x.VIGENCIA_DE == a1 && x.VIGENCIA_AL == a2)
                                          .Join(db.MATERIALs, x => x.MATNR, y => y.ID, (x, y) => new { x.NUM_DOC, x.MATNR, x.MATKL, y.MAKTX, x.MONTO, y.PUNIT, x.PORC_APOYO, x.MONTO_APOYO, resta = (x.MONTO - x.MONTO_APOYO), x.PRECIO_SUG })
                                          .ToList();

                    foreach (var item2 in con2)
                    {
                        armadoCuerpoTab.Add(item2.MATNR.TrimStart('0'));
                        armadoCuerpoTab.Add(item2.MATKL);
                        armadoCuerpoTab.Add(item2.MAKTX);
                        armadoCuerpoTab.Add(Math.Round(item2.MONTO, 2).ToString());
                        armadoCuerpoTab.Add(Math.Round(item2.PORC_APOYO, 2).ToString());
                        armadoCuerpoTab.Add(Math.Round(item2.MONTO_APOYO, 2).ToString());
                        armadoCuerpoTab.Add(Math.Round(item2.resta, 2).ToString());
                        armadoCuerpoTab.Add(Math.Round(item2.PRECIO_SUG, 2).ToString());
                        contadorTabla++;
                    }
                    numfilasTabla.Add(contadorTabla);
                }


                HeaderFooter hfc = new HeaderFooter();
                hfc.eliminaArchivos();

                EncabezadoMateriales em = new EncabezadoMateriales();
                CartaV cv = new CartaV();

                c = db.TEXTOCARTAVs
                        .Where(x => x.SPRAS_ID == user.SPRAS_ID)
                        .First();
                //ENCABEZADO TABLA
                var encabezado = new List<string>();
                encabezado.Add(em.material = c.MATERIAL);
                encabezado.Add(em.categoria = c.CATEGORIA);
                encabezado.Add(em.descripcion = c.DESCRIPCION);
                encabezado.Add(em.costoun = c.COSTOU);
                encabezado.Add(em.apoyo = c.APOYOP);
                encabezado.Add(em.apoyop = c.APOYOPP);
                encabezado.Add(em.costoap = c.COSTOA);
                encabezado.Add(em.precio = c.PRECIOSU);

                DOCUMENTO d = new DOCUMENTO();
                PUESTOT pp = new PUESTOT();
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
                    pp = db.PUESTOTs.Where(a => a.SPRAS_ID.Equals(sp) && a.PUESTO_ID == d.USUARIO.PUESTO_ID).FirstOrDefault();
                    ////d.CITy.STATE.NAME = db.STATES.Where(a => a.ID.Equals(d.CITy.STATE_ID)).FirstOrDefault().NAME;
                }
                ViewBag.legal = db.LEYENDAs.Where(a => a.PAIS_ID.Equals(d.PAIS_ID) && a.ACTIVO == true).FirstOrDefault();



                //CUERPO DE LA CARTA
                cv.listaFechas = lista;
                cv.numfilasTabla = numfilasTabla;
                cv.listaEncabezado = encabezado;
                cv.listaCuerpo = armadoCuerpoTab;
                cv.num_doc = id;
                cv.company = d.SOCIEDAD.BUTXT;
                cv.company_x = true;
                cv.taxid = d.SOCIEDAD.LAND;
                cv.taxid_x = true;
                cv.concepto = d.CONCEPTO;
                cv.concepto_x = true;
                cv.cliente = d.PAYER_NOMBRE;
                cv.cliente_x = true;
                cv.puesto = " ";
                cv.puesto_x = false;
                cv.direccion = d.CLIENTE.STRAS_GP;
                cv.direccion_x = true;
                cv.folio = d.NUM_DOC.ToString();
                cv.folio_x = true;
                //cv.lugar = "Qro, Qro."+DateTime.Now.ToShortTimeString();
                cv.lugar = d.CIUDAD.Trim() + ", " + d.ESTADO.Trim();
                ////cv.lugar = d.CITy.NAME + ", " + d.CITy.STATE.NAME;
                cv.lugar_x = true;
                cv.lugarFech = DateTime.Now.ToShortDateString();
                cv.lugarFech_x = true;
                cv.payerId = d.CLIENTE.PAYER;
                cv.payerId_x = true;
                cv.payerNom = d.CLIENTE.NAME1;
                cv.payerNom_x = true;
                cv.estimado = d.PAYER_NOMBRE;
                cv.estimado_x = true;
                cv.mecanica = d.NOTAS;
                cv.mecanica_x = true;
                cv.nombreE = d.USUARIO.NOMBRE + " " + d.USUARIO.APELLIDO_P + " " + d.USUARIO.APELLIDO_M;
                cv.nombreE_x = true;
                if (pp != null)
                    cv.puestoE = pp.TXT50;
                cv.puestoE_x = true;
                cv.companyC = cv.company;
                cv.companyC_x = true;
                cv.nombreC = d.PAYER_NOMBRE;
                cv.nombreC_x = true;
                cv.puestoC = " ";
                cv.puestoC_x = false;
                cv.companyCC = d.CLIENTE.NAME1;
                cv.companyCC_x = true;
                if (ViewBag.legal != null)
                    cv.legal = ViewBag.legal.LEYENDA1;
                cv.legal_x = true;
                cv.mail = c.E_MAIL + " " + d.PAYER_EMAIL;
                cv.mail_x = true;
                cv.comentarios = "";
                cv.comentarios_x = true;
                cv.compromisoK = "";
                cv.compromisoK_x = true;
                cv.compromisoC = "";
                cv.compromisoC_x = true;
                cv.monto = d.MONTO_DOC_MD.ToString();
                cv.moneda = d.MONEDA_ID;
                return View(cv);
            }
        }

        // POST: CartaV/Details/5
        [HttpPost]
        public ActionResult Create(CartaV v)
        {
            using (TAT001Entities db = new TAT001Entities())
            {
                TEXTOCARTAV c = new TEXTOCARTAV();
                string u = User.Identity.Name;
                var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                c = db.TEXTOCARTAVs
                        .Where(x => x.SPRAS_ID == user.SPRAS_ID)
                        .First();
                EncabezadoMateriales em = new EncabezadoMateriales();
                var encabezadoTab = new List<string>();
                encabezadoTab.Add(em.material = c.MATERIAL);
                encabezadoTab.Add(em.categoria = c.CATEGORIA);
                encabezadoTab.Add(em.descripcion = c.DESCRIPCION);
                encabezadoTab.Add(em.costoun = c.COSTOU);
                encabezadoTab.Add(em.apoyo = c.APOYOP);
                encabezadoTab.Add(em.apoyop = c.APOYOPP);
                encabezadoTab.Add(em.costoap = c.COSTOA);
                encabezadoTab.Add(em.precio = c.PRECIOSU);

                List<string> encabezadoFech = new List<string>();
                List<string> armadoCuerpoTab = new List<string>();
                List<int> numfilasTab = new List<int>();

                int contadorTabla = 0;

                //var con = db.DOCUMENTOPs.Select(x => new { x.VIGENCIA_DE, x.VIGENCIA_AL }).GroupBy(f => new { f.VIGENCIA_DE, f.VIGENCIA_AL }).ToList();
                var con = db.DOCUMENTOPs.Select(x => new { x.NUM_DOC, x.VIGENCIA_DE, x.VIGENCIA_AL }).Where(a => a.NUM_DOC.Equals(v.num_doc)).GroupBy(f => new { f.VIGENCIA_DE, f.VIGENCIA_AL }).ToList();

                foreach (var item in con)
                {
                    encabezadoFech.Add(item.Key.VIGENCIA_DE.ToString() + item.Key.VIGENCIA_AL.ToString());
                }

                for (int i = 0; i < encabezadoFech.Count; i++)
                {
                    contadorTabla = 0;
                    //DateTime a1 = DateTime.Parse(lista[i].Remove(24));
                    DateTime a1 = DateTime.Parse(encabezadoFech[i].Remove(encabezadoFech[i].Length / 2));
                    //DateTime a2 = DateTime.Parse(lista[i].Remove(0, 24));
                    DateTime a2 = DateTime.Parse(encabezadoFech[i].Remove(0, encabezadoFech[i].Length / 2));

                    var con2 = db.DOCUMENTOPs
                                      .Where(x => x.NUM_DOC.Equals(v.num_doc) & x.VIGENCIA_DE == a1 && x.VIGENCIA_AL == a2)
                                      .Join(db.MATERIALs, x => x.MATNR, y => y.ID, (x, y) => new { x.MATNR, x.MATKL, y.MAKTX, x.MONTO, y.PUNIT, x.PORC_APOYO, x.MONTO_APOYO, resta = (x.MONTO - x.MONTO_APOYO), x.PRECIO_SUG })
                                      .ToList();


                    foreach (var item in con2)
                    {
                        armadoCuerpoTab.Add(item.MATNR.TrimStart('0'));
                        armadoCuerpoTab.Add(item.MATKL);
                        armadoCuerpoTab.Add(item.MAKTX);
                        armadoCuerpoTab.Add(Math.Round((decimal)item.MONTO, 2).ToString());
                        armadoCuerpoTab.Add(Math.Round(item.PORC_APOYO, 2).ToString());
                        armadoCuerpoTab.Add(Math.Round(item.MONTO_APOYO, 2).ToString());
                        armadoCuerpoTab.Add(Math.Round((decimal)item.resta, 2).ToString());
                        armadoCuerpoTab.Add(Math.Round(item.PRECIO_SUG, 2).ToString());
                        contadorTabla++;
                    }
                    numfilasTab.Add(contadorTabla);
                }
                bool aprob = false;
                DOCUMENTO d = db.DOCUMENTOes.Find(v.num_doc);
                aprob = (d.ESTATUS_WF.Equals("A"));

                CartaV carta = v;
                CartaVEsqueleto cve = new CartaVEsqueleto();
                cve.crearPDF(carta, c, encabezadoFech, encabezadoTab, numfilasTab, armadoCuerpoTab, aprob);
                string recibeRuta = Convert.ToString(Session["rutaCompletaV"]);
                return RedirectToAction("Index", new { ruta = recibeRuta });
            }
        }

        // GET: CartaV/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartaV/Edit/5
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

        // GET: CartaV/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartaV/Delete/5
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
