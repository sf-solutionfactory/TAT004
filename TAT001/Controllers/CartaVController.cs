using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using TAT001.Entities;
using TAT001.Models;

namespace TAT001.Controllers
{
    [Authorize]
    public class CartaVController : Controller
    {
        // GET: CartaV
        public ActionResult Index(string ruta, decimal ids)
        {
            int pagina = 230; //ID EN BASE DE DATOS
            using (TAT001Entities db = new TAT001Entities())
            {
                string u = User.Identity.Name;
                var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                ViewBag.permisos = db.PAGINAVs.Where(a => a.ID.Equals(user.ID)).ToList();
                ViewBag.carpetas = db.CARPETAVs.Where(a => a.USUARIO_ID.Equals(user.ID)).ToList();
                ViewBag.usuario = user;
                ViewBag.rol = user.PUESTO.PUESTOTs.Where(a => a.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                ViewBag.Title = db.PAGINAs.Where(a => a.ID.Equals(pagina)).FirstOrDefault().PAGINATs.Where(b => b.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                ViewBag.warnings = db.WARNINGVs.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();
                ViewBag.textos = db.TEXTOes.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();

                try
                {
                    string p = Session["pais"].ToString();
                    ViewBag.pais = p + ".png";
                }
                catch
                {
                    //return RedirectToAction("Pais", "Home");
                }
            }
            ViewBag.url = Request.Url.OriginalString.Replace(Request.Url.PathAndQuery, "") + HostingEnvironment.ApplicationVirtualPath + "/" + ruta;
            ViewBag.miNum = ids;

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
                ViewBag.rol = user.PUESTO.PUESTOTs.Where(a => a.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                ViewBag.Title = db.PAGINAs.Where(a => a.ID.Equals(pagina)).FirstOrDefault().PAGINATs.Where(b => b.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                ViewBag.warnings = db.WARNINGVs.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();
                ViewBag.textos = db.TEXTOes.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();

                try
                {
                    string p = Session["pais"].ToString();
                    ViewBag.pais = p + ".png";
                }
                catch
                {
                    //return RedirectToAction("Pais", "Home");
                }
            }
            ViewBag.url = ruta;
            return View();
        }

        // GET: CartaV/Details/5
        public ActionResult Create(decimal id)
        {
            int pagina = 232; //ID EN BASE DE DATOS
            using (TAT001Entities db = new TAT001Entities())
            {
                string u = User.Identity.Name;
                var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                ViewBag.permisos = db.PAGINAVs.Where(a => a.ID.Equals(user.ID)).ToList();
                ViewBag.carpetas = db.CARPETAVs.Where(a => a.USUARIO_ID.Equals(user.ID)).ToList();
                ViewBag.usuario = user;
                ViewBag.rol = user.PUESTO.PUESTOTs.Where(a => a.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                ViewBag.Title = db.PAGINAs.Where(a => a.ID.Equals(pagina)).FirstOrDefault().PAGINATs.Where(b => b.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                ViewBag.warnings = db.WARNINGVs.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();
                ViewBag.textos = db.TEXTOes.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();
                ViewBag.de = db.TEXTOCVs.Where(t => t.SPRAS_ID.Equals(user.SPRAS_ID) & t.CAMPO == "de").Select(t => t.TEXTO).FirstOrDefault();
                ViewBag.al = db.TEXTOCVs.Where(t => t.SPRAS_ID.Equals(user.SPRAS_ID) & t.CAMPO == "a").Select(t => t.TEXTO).FirstOrDefault();
                ViewBag.mon = db.TEXTOCVs.Where(t => t.SPRAS_ID.Equals(user.SPRAS_ID) & t.CAMPO == "monto").Select(t => t.TEXTO).FirstOrDefault();

                try
                {
                    string p = Session["pais"].ToString();
                    ViewBag.pais = p + ".png";
                }
                catch
                {
                    //ViewBag.pais = "mx.png";
                    //return RedirectToAction("Pais", "Home");
                }

                DOCUMENTO d = new DOCUMENTO();
                PUESTOT pp = new PUESTOT();
                d = db.DOCUMENTOes.Include("SOCIEDAD").Include("USUARIO").Where(a => a.NUM_DOC.Equals(id)).First();

                ViewBag.miles = d.PAI.MILES;//LEJGG 090718
                ViewBag.dec = d.PAI.DECIMAL;//LEJGG 090718

                List<string> lista = new List<string>();
                List<listacuerpoc> armadoCuerpoTab = new List<listacuerpoc>(); //B20180710 MGC 2018.07.10 Modificaciones para editar los campos de distribución se agrego los objetos
                List<string> armadoCuerpoTab2 = new List<string>();
                List<int> numfilasTabla = new List<int>();
                int contadorTabla = 0;
                HeaderFooter hfc = new HeaderFooter();
                hfc.eliminaArchivos();
                CartaV cv = new CartaV();

                if (d != null)
                {
                    d.CLIENTE = db.CLIENTEs.Where(a => a.VKORG.Equals(d.VKORG)
                                                              & a.VTWEG.Equals(d.VTWEG)
                                                            & a.SPART.Equals(d.SPART)
                                                            & a.KUNNR.Equals(d.PAYER_ID)).First();
                    string sp = Session["spras"].ToString();
                    pp = db.PUESTOTs.Where(a => a.SPRAS_ID.Equals(sp) && a.PUESTO_ID == d.USUARIO.PUESTO_ID).FirstOrDefault();
                }
                ViewBag.legal = db.LEYENDAs.Where(a => a.PAIS_ID.Equals(d.PAIS_ID) && a.ACTIVO == true).FirstOrDefault();


                /////////////////////////////////////////////DATOS PARA LA TABLA 1 MATERIALES EN LA VISTA///////////////////////////////////////
                var con = db.DOCUMENTOPs.Select(x => new { x.NUM_DOC, x.VIGENCIA_DE, x.VIGENCIA_AL }).Where(a => a.NUM_DOC.Equals(id)).GroupBy(f => new { f.VIGENCIA_DE, f.VIGENCIA_AL }).ToList();

                //B20180710 MGC 2018.07.12 Modificación 9 y 10 dependiendo del campo de factura en tsol............
                bool fact = false;
                try
                {
                    fact = db.TSOLs.Where(ts => ts.ID == d.TSOL_ID).FirstOrDefault().FACTURA;
                }
                catch (Exception)
                {

                }
                //B20180710 MGC 2018.07.12 Modificación 9 y 10 dependiendo del campo de factura en tsol..............

                //B20180710 MGC 2018.07.18 total es input o text
                string trclass = "";
                bool editmonto = false; //B20180710 MGC 2018.07.18 editar el monto en porcentaje categoría

                foreach (var item in con)
                {
                    lista.Add(item.Key.VIGENCIA_DE.ToString() + item.Key.VIGENCIA_AL.ToString());
                }

                for (int i = 0; i < lista.Count; i++)
                {
                    contadorTabla = 0;

                    DateTime a1 = DateTime.Parse(lista[i].Remove(lista[i].Length / 2));
                    DateTime a2 = DateTime.Parse(lista[i].Remove(0, lista[i].Length / 2));

                    var con2 = db.DOCUMENTOPs
                                          .Where(x => x.NUM_DOC.Equals(id) & x.VIGENCIA_DE == a1 && x.VIGENCIA_AL == a2)
                                          .Join(db.MATERIALs, x => x.MATNR, y => y.ID, (x, y) => new {
                                              x.NUM_DOC,
                                              x.MATNR,
                                              x.MATKL,
                                              y.MAKTX,
                                              x.MONTO,
                                              y.PUNIT,
                                              x.PORC_APOYO,
                                              x.MONTO_APOYO,
                                              resta = (x.MONTO - x.MONTO_APOYO),
                                              x.PRECIO_SUG,
                                              x.APOYO_EST,
                                              x.APOYO_REAL
                                          ,
                                              x.VOLUMEN_EST,
                                              x.VOLUMEN_REAL
                                          }) //B20180710 MGC 2018.07.10 Se agregó x.VOLUMEN_EST, x.VOLUMEN_REAL
                                          .ToList();

                    //Definición si la distribución es monto o porcentaje
                    string porclass = "";//B20180710 MGC 2018.07.18 total es input o text
                    string totalm = "";//B20180710 MGC 2018.07.18 total es input o text
                    if (d.TIPO_TECNICO == "M")
                    {
                        porclass = " tipom";
                        totalm = " total";
                        trclass = " total";
                    }
                    else if (d.TIPO_TECNICO == "P")
                    {
                        porclass = " tipop";
                        totalm = " ni";
                    }

                    if (con2.Count > 0)
                    {
                        foreach (var item2 in con2)
                        {
                            //B20180710 MGC 2018.07.10 Modificaciones para editar los campos de distribución se agrego los objetos
                            listacuerpoc lc1 = new listacuerpoc();
                            lc1.val = item2.MATNR.TrimStart('0');
                            lc1.clase = "ni";
                            armadoCuerpoTab.Add(lc1);

                            listacuerpoc lc2 = new listacuerpoc();
                            lc2.val = item2.MATKL;
                            lc2.clase = "ni";
                            armadoCuerpoTab.Add(lc2);

                            listacuerpoc lc3 = new listacuerpoc();
                            lc3.val = item2.MAKTX;
                            lc3.clase = "ni";
                            armadoCuerpoTab.Add(lc3);

                            //Costo unitario
                            listacuerpoc lc4 = new listacuerpoc();
                            lc4.val = "$" + Math.Round(item2.MONTO, 2).ToString();
                            lc4.clase = "input_oper numberd input_dc mon" + porclass;
                            armadoCuerpoTab.Add(lc4);

                            //Porcentaje de apoyo
                            listacuerpoc lc5 = new listacuerpoc();
                            lc5.val = Math.Round(item2.PORC_APOYO, 2).ToString() + "%";
                            lc5.clase = "input_oper numberd porc input_dc" + porclass;
                            armadoCuerpoTab.Add(lc5);

                            //Apoyo por pieza
                            listacuerpoc lc6 = new listacuerpoc();
                            lc6.val = "$" + Math.Round(item2.MONTO_APOYO, 2).ToString();
                            lc6.clase = "input_oper numberd costoa input_dc mon" + porclass;
                            armadoCuerpoTab.Add(lc6);

                            //Costo con apoyo
                            listacuerpoc lc7 = new listacuerpoc();
                            lc7.val = "$" + Math.Round(item2.resta, 2).ToString();
                            lc7.clase = "input_oper numberd costoa input_dc mon" + porclass;//Importante costoa para validación en vista
                            armadoCuerpoTab.Add(lc7);

                            //Precio Sugerido
                            listacuerpoc lc8 = new listacuerpoc();
                            lc8.val = "$" + Math.Round(item2.PRECIO_SUG, 2).ToString();
                            lc8.clase = "input_oper numberd input_dc mon" + porclass;
                            armadoCuerpoTab.Add(lc8);

                            //Modificación 9 y 10 dependiendo del campo de factura en tsol
                            //fact = true es real
                            //Volumen
                            listacuerpoc lc9 = new listacuerpoc();
                            if (fact)
                            {
                                lc9.val = Math.Round(Convert.ToDecimal(item2.VOLUMEN_REAL), 2).ToString();
                            }
                            else
                            {
                                lc9.val = Math.Round(Convert.ToDecimal(item2.VOLUMEN_EST), 2).ToString();
                            }
                            lc9.clase = "input_oper numberd input_dc num" + porclass;
                            armadoCuerpoTab.Add(lc9);

                            //Apoyo estimado
                            listacuerpoc lc10 = new listacuerpoc();
                            if (fact)
                            {
                                lc10.val = "$" + Math.Round(Convert.ToDecimal(item2.APOYO_REAL), 2).ToString();
                            }
                            else
                            {
                                lc10.val = "$" + Math.Round(Convert.ToDecimal(item2.APOYO_EST), 2).ToString();
                            }
                            lc10.clase = "input_oper numberd input_dc mon" + totalm + "" + porclass;
                            armadoCuerpoTab.Add(lc10);

                            contadorTabla++;
                        }
                    }
                    else
                    {
                        var con3 = db.DOCUMENTOPs
                                            .Where(x => x.NUM_DOC.Equals(id) & x.VIGENCIA_DE == a1 && x.VIGENCIA_AL == a2)
                                            .Join(db.MATERIALGPs, x => x.MATKL, y => y.ID, (x, y) => new {
                                                x.NUM_DOC,
                                                x.MATNR,
                                                x.MATKL,
                                                y.ID,
                                                x.MONTO,
                                                x.PORC_APOYO,
                                                y.MATERIALGPTs.Where(a => a.SPRAS_ID.Equals(d.CLIENTE.SPRAS)).FirstOrDefault().TXT50,
                                                x.MONTO_APOYO,
                                                resta = (x.MONTO - x.MONTO_APOYO),
                                                x.PRECIO_SUG,
                                                x.APOYO_EST,
                                                x.APOYO_REAL
                                            ,
                                                x.VOLUMEN_EST,
                                                x.VOLUMEN_REAL
                                            }) //B20180710 MGC 2018.07.10 Se agregó x.VOLUMEN_EST, x.VOLUMEN_REAL})
                                            .ToList();
                        if (d.TIPO_TECNICO == "M")
                        {
                            trclass = "total";
                        }
                        else if (d.TIPO_TECNICO == "P")
                        {
                            editmonto = true;
                        }


                        foreach (var item2 in con3)
                        {
                            //B20180710 MGC 2018.07.10 Modificaciones para editar los campos de distribución se agrego los objetos
                            listacuerpoc lc1 = new listacuerpoc();
                            lc1.val = "";
                            lc1.clase = "ni";
                            armadoCuerpoTab.Add(lc1);

                            listacuerpoc lc2 = new listacuerpoc();
                            lc2.val = item2.MATKL;
                            lc2.clase = "ni";
                            armadoCuerpoTab.Add(lc2);

                            listacuerpoc lc3 = new listacuerpoc();
                            lc3.val = item2.TXT50;
                            lc3.clase = "ni";
                            armadoCuerpoTab.Add(lc3);

                            //Costo unitario
                            listacuerpoc lc4 = new listacuerpoc();
                            //lc4.val = Math.Round(item2.MONTO, 2).ToString();
                            lc4.val = "";
                            lc4.clase = "ni";
                            armadoCuerpoTab.Add(lc4);

                            //Porcentaje de apoyo
                            listacuerpoc lc5 = new listacuerpoc();
                            //lc5.val = Math.Round(item2.PORC_APOYO, 2).ToString();
                            //Definición si la distribución es monto o porcentaje
                            if (d.TIPO_TECNICO == "M")
                            {
                                lc5.val = "";
                            }
                            else if (d.TIPO_TECNICO == "P")
                            {
                                lc5.val = Math.Round(item2.PORC_APOYO, 2).ToString() + "%";
                            }

                            //lc5.clase = "input_oper numberd input_dc";
                            lc5.clase = "ni";
                            armadoCuerpoTab.Add(lc5);

                            //Apoyo por pieza
                            listacuerpoc lc6 = new listacuerpoc();
                            //lc6.val = Math.Round(item2.MONTO_APOYO, 2).ToString();
                            lc6.val = "";
                            lc6.clase = "ni";
                            armadoCuerpoTab.Add(lc6);

                            //Costo con apoyo
                            listacuerpoc lc7 = new listacuerpoc();
                            //lc7.val = Math.Round(item2.resta, 2).ToString();
                            lc7.val = "";
                            lc7.clase = "ni";
                            armadoCuerpoTab.Add(lc7);

                            //Precio Sugerido
                            listacuerpoc lc8 = new listacuerpoc();
                            //lc8.val = Math.Round(item2.PRECIO_SUG, 2).ToString();
                            lc8.val = "";
                            lc8.clase = "ni";
                            armadoCuerpoTab.Add(lc8);
                            //Modificación 9 y 10 dependiendo del campo de factura en tsol
                            //fact = true es real

                            //Volumen
                            listacuerpoc lc9 = new listacuerpoc();
                            if (fact)
                            {
                                //lc9.val = Math.Round(Convert.ToDouble(item2.VOLUMEN_REAL), 2).ToString();
                                lc9.val = "";
                            }
                            else
                            {
                                //lc9.val = Math.Round(Convert.ToDouble(item2.VOLUMEN_EST), 2).ToString();
                                lc9.val = "";
                            }
                            lc9.clase = "ni";
                            armadoCuerpoTab.Add(lc9);

                            //Apoyo
                            listacuerpoc lc10 = new listacuerpoc();
                            if (fact)
                            {
                                lc10.val = "$" + Math.Round(Convert.ToDecimal(item2.APOYO_REAL), 2).ToString();
                            }
                            else
                            {
                                lc10.val = "$" + Math.Round(Convert.ToDecimal(item2.APOYO_EST), 2).ToString();
                            }
                            //Definición si la distribución es monto o porcentaje
                            if (d.TIPO_TECNICO == "M")
                            {
                                lc10.clase = "input_oper numberd input_dc total cat mon";
                            }
                            else if (d.TIPO_TECNICO == "P")
                            {
                                lc10.clase = "ni";
                            }

                            armadoCuerpoTab.Add(lc10);

                            contadorTabla++;
                        }
                    }
                    numfilasTabla.Add(contadorTabla);
                }

                var cabeza = new List<string>();
                cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "materialC").Select(x => x.TEXTO).FirstOrDefault());
                cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "categoriaC").Select(x => x.TEXTO).FirstOrDefault());
                cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "descripcionC").Select(x => x.TEXTO).FirstOrDefault());
                cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "costouC").Select(x => x.TEXTO).FirstOrDefault());
                cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "apoyopoC").Select(x => x.TEXTO).FirstOrDefault());
                cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "apoyopiC").Select(x => x.TEXTO).FirstOrDefault());
                cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "costoaC").Select(x => x.TEXTO).FirstOrDefault());
                cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "preciosC").Select(x => x.TEXTO).FirstOrDefault());
                //B20180710 MGC 2018.07.12 Apoyo es real o es estimado
                //fact = true es real
                //Volumen
                if (fact)
                {
                    cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "volumenrC").Select(x => x.TEXTO).FirstOrDefault());
                }
                else
                {
                    cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "volumeneC").Select(x => x.TEXTO).FirstOrDefault());
                }
                //Apoyo
                if (fact)
                {
                    cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "apoyorC").Select(x => x.TEXTO).FirstOrDefault());
                }
                else
                {
                    cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "apoyoeC").Select(x => x.TEXTO).FirstOrDefault());
                }

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                /////////////////////////////////////////////DATOS PARA LA TABLA 2 RECURRENCIAS EN LA VISTA///////////////////////////////////////
                var cabeza2 = new List<string>();
                cabeza2.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "posC2").Select(x => x.TEXTO).FirstOrDefault());
                cabeza2.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "tipoC2").Select(x => x.TEXTO).FirstOrDefault());
                cabeza2.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "fechaC2").Select(x => x.TEXTO).FirstOrDefault());
                cabeza2.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "montoC2").Select(x => x.TEXTO).FirstOrDefault());
                cabeza2.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "porcentajeC2").Select(x => x.TEXTO).FirstOrDefault());

                var con4 = db.DOCUMENTORECs
                                            .Where(x => x.NUM_DOC.Equals(id))
                                            .Join(db.DOCUMENTOes, x => x.NUM_DOC, y => y.NUM_DOC, (x, y) => new { x.POS, y.TSOL_ID, x.FECHAF, x.MONTO_BASE, x.PORC })
                                            .ToList();

                foreach (var item in con4)
                {
                    DateTime a = Convert.ToDateTime(item.FECHAF);

                    armadoCuerpoTab2.Add(item.POS.ToString());
                    armadoCuerpoTab2.Add(db.TSOLs.Where(x => x.ID == item.TSOL_ID).Select(x => x.DESCRIPCION).First());
                    armadoCuerpoTab2.Add(a.ToShortDateString());
                    armadoCuerpoTab2.Add(item.MONTO_BASE.ToString());
                    armadoCuerpoTab2.Add(item.PORC.ToString());
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //TABLA 1 MATERIALES
                cv.listaFechas = lista;//////////////RANGO DE FECHAS QUE DETERMINAN EL NUMERO DE TABLAS
                cv.numfilasTabla = numfilasTabla;////NUMERO FILAS POR TABLA CALCULADA
                cv.listaCuerpom = armadoCuerpoTab;////NUMERO TOTAL DE FILAS CON LA INFO CORRESPONDIENTE QUE POSTERIORMENTE ES DISTRIBUIDA EN LAS TABLAS //B20180710 MGC 2018.07.10 Modificaciones para editar los campos de distribución se agrego los objetos
                cv.numColEncabezado = cabeza;////////NUMERO DE COLUMNAS PARA LAS TABLAS
                cv.secondTab_x = true;
                cv.costoun_x = true;
                cv.apoyo_x = true;
                cv.apoyop_x = true;
                cv.costoap_x = true;
                cv.precio_x = true;
                cv.apoyoEst_x = true; //Volumen
                cv.apoyoRea_x = true; //Apoyo
                                      /////////////////////////////////

                //TABLA 2 RECURRENCIAS
                cv.numColEncabezado2 = cabeza2;////////NUMERO DE COLUMNAS PARA LAS TABLAS
                cv.numfilasTabla2 = con4.Count();//////NUMERO FILAS TOTAL PARA LA TABLA
                cv.listaCuerpoRec = armadoCuerpoTab2;//NUMERO TOTAL DE FILAS CON LA INFO CORRESPONDIENTE
                ///////////////////////////////

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
                cv.lugar = d.CIUDAD.Trim() + ", " + d.ESTADO.Trim();
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
                cv.mail = db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "correo").Select(x => x.TEXTO).FirstOrDefault() + " " + d.PAYER_EMAIL;
                cv.mail_x = true;
                cv.comentarios = "";
                cv.comentarios_x = true;
                cv.compromisoK = "";
                cv.compromisoK_x = true;
                cv.compromisoC = "";
                cv.compromisoC_x = true;
                cv.monto_x = true;
                cv.monto = d.MONTO_DOC_MD.ToString();
                cv.moneda = d.MONEDA_ID;

                ViewBag.factura = fact;//B20180710 MGC 2018.07.12 Apoyo es real o es estimado
                ViewBag.trclass = trclass;//B20180710 MGC 2018.07.18 total es input o text
                ViewBag.editmonto = editmonto;//B20180710 MGC 2018.07.18 total es input o text

                return View(cv);
            }
        }

        // POST: CartaV/Details/5
        [HttpPost]
        //[ValidateAntiForgeryToken] //B20180710 MGC 2018.07.16 Modificaciones para editar los campos de distribución se agrego los objetos
        //public ActionResult Create([Bind(Include = "num_doc, listaCuerpo, DOCUMENTOP")] CartaV v)
        public ActionResult Create(CartaV v, string monto_enviar, string guardar_param)
        {
            v.monto = monto_enviar; //B20180720P MGC
            int pos = 0;//B20180720P MGC Guardar Carta

            CARTA ca = new CARTA();
            ca.NUM_DOC = v.num_doc;
            ca.CLIENTE = v.cliente;
            ca.CLIENTEX = v.cliente_x;
            ca.COMPANY = v.company;
            ca.COMPANYC = v.companyC;
            ca.COMPANYCC = v.companyCC;
            ca.COMPANYCCX = v.companyCC_x;
            ca.COMPANYCX = v.companyC_x;
            ca.COMPANYX = v.company_x;
            ca.CONCEPTO = v.concepto;
            ca.CONCEPTOX = v.concepto_x;
            ca.DIRECCION = v.direccion;
            ca.DIRECCIONX = v.direccion_x;
            //ca.DOCUMENTO = v.DOCUMENTO;
            ca.ESTIMADO = v.estimado;
            ca.ESTIMADOX = v.estimado_x;
            //ca.FECHAC = v.FECHAC;
            ca.FOLIO = v.folio;
            ca.FOLIOX = v.folio_x;
            ca.LEGAL = v.legal;
            ca.LEGALX = v.legal_x;
            ca.LUGARFECH = v.lugarFech;
            ca.LUGARFECHX = v.lugarFech_x;
            ca.LUGAR = v.lugar;
            ca.LUGARX = v.lugar_x;
            ca.MAIL = v.mail;
            ca.MAILX = v.mail_x;
            ca.MECANICA = v.mecanica;
            ca.MECANICAX = v.mecanica_x;
            ca.NOMBREC = v.nombreC;
            ca.NOMBRECX = v.nombreC_x;
            ca.NOMBREE = v.nombreE;
            ca.NOMBREEX = v.nombreE_x;
            ca.NUM_DOC = v.num_doc;
            ca.PAYER = v.payerId;
            ca.PAYERX = v.payerId_x;
            ca.PUESTO = v.puesto;
            ca.PUESTOC = v.puestoC;
            ca.PUESTOCX = v.puestoC_x;
            ca.PUESTOE = v.puestoE;
            ca.PUESTOEX = v.puestoE_x;
            ca.PUESTOX = v.puestoE_x;
            ca.TAXID = v.taxid;
            ca.TAXIDX = v.taxid_x;
            ca.MONTO = v.monto;
            ca.MONEDA = v.moneda;
            //ca.TIPO = v.TIPO;
            //ca.USUARIO = v.USUARIO;
            //ca.USUARIO_ID = v.USUARIO_ID;
            ca.USUARIO_ID = User.Identity.Name;
            ca.FECHAC = DateTime.Now;

                //CartaFEsqueleto cfe = new CartaFEsqueleto();//B20180720P MGC Guardar Carta
                //TEXTOCARTAF f = new TEXTOCARTAF();//B20180720P MGC Guardar Carta
                string u = User.Identity.Name;
            //string recibeRuta = ""; //B20180720P MGC Guardar Carta
            using (TAT001Entities db = new TAT001Entities())
                {
                    var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                    var cartas = db.CARTAs.Where(a => a.NUM_DOC.Equals(ca.NUM_DOC)).ToList();
                    //int pos = 0;//B20180720P MGC Guardar Carta
                    if (cartas.Count > 0)
                        pos = cartas.OrderByDescending(a => a.POS).First().POS;

                    ca.POS = pos + 1;
                pos = ca.POS; //B20180720P MGC Guardar Carta
                if (guardar_param == "guardar_param")//B20180720P MGC Guardar Carta
                    {
                        db.CARTAs.Add(ca);
                        db.SaveChanges();
                    }
                }
                //bool aprob = false;//B20180720P MGC Guardar Carta
                //B20180720P MGC Guardar Carta
                //using (TAT001Entities db = new TAT001Entities())
                //{
                //    DOCUMENTO d = db.DOCUMENTOes.Find(c.num_doc);
                //    aprob = (d.ESTATUS_WF.Equals("A"));

                //    cfe.crearPDF(c, f, aprob);
                //    recibeRuta = Convert.ToString(Session["rutaCompletaf"]);
                //    return RedirectToAction("Details", new { ruta = recibeRuta });
                //}
            
            using (TAT001Entities db = new TAT001Entities())
            {
                //string u = User.Identity.Name; //B20180720P MGC Guardar Carta
                var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();

                List<string> encabezadoFech = new List<string>();
                List<string> armadoCuerpoTab = new List<string>(); //B20180710 MGC 2018.07.10 Modificaciones para editar los campos de distribución se agrego los objetos
                List<string> armadoCuerpoTab2 = new List<string>();
                List<int> numfilasTab = new List<int>();

                int contadorTabla = 0;
                DOCUMENTO d = db.DOCUMENTOes.Find(v.num_doc);


                /////////////////////////////////////////////DATOS PARA LA TABLA 1 MATERIALES EN EL PDF///////////////////////////////////////
                var con = db.DOCUMENTOPs.Select(x => new { x.NUM_DOC, x.VIGENCIA_DE, x.VIGENCIA_AL }).Where(a => a.NUM_DOC.Equals(v.num_doc)).GroupBy(f => new { f.VIGENCIA_DE, f.VIGENCIA_AL }).ToList();
                //B20180710 MGC 2018.07.17 Modificación de monto
                //v.monto = monto_enviar; //B20180720P MGC
                //B20180710 MGC 2018.07.17 Modificación 9 y 10 dependiendo del campo de factura en tsol............
                bool fact = false;
                try
                {
                    fact = db.TSOLs.Where(ts => ts.ID == d.TSOL_ID).FirstOrDefault().FACTURA;
                }
                catch (Exception)
                {

                }
                //B20180710 MGC 2018.07.17 Modificación 9 y 10 dependiendo del campo de factura en tsol..............

                foreach (var item in con)
                {
                    encabezadoFech.Add(item.Key.VIGENCIA_DE.ToString() + item.Key.VIGENCIA_AL.ToString());
                }

                //B20180710 MGC 2018.07.19 Provisional obtener la siguiente posición para carta......................
                //B20180720P MGC Guardar Carta.......................................................................
                //int pos = 0; //B20180720P MGC Guardar Carta

                //try
                //{
                //    pos = db.CARTAs.Where(ca => ca.NUM_DOC == v.num_doc).Max(ca => ca.POS);
                //    pos++;
                //}
                //catch (Exception)
                //{

                //}

                ////Guardar carta
                //if (guardar_param == "guardar_param")
                //{
                //    CARTA car = new CARTA();
                //    car.NUM_DOC = v.num_doc;
                //    car.POS = pos;

                //    try
                //    {
                //        db.CARTAs.Add(car);
                //        db.SaveChanges();
                //    }
                //    catch (Exception e)
                //    {

                //    }
                //}
                //B20180720P MGC Guardar Carta......................................................................

                //B20180710 MGC 2018.07.19 Provisional obtener la siguiente posición para carta......................
                int indexp = 1; //B20180710 MGC 2018.07.17
                for (int i = 0; i < encabezadoFech.Count; i++)
                {
                    contadorTabla = 0;
                    DateTime a1 = DateTime.Parse(encabezadoFech[i].Remove(encabezadoFech[i].Length / 2));
                    DateTime a2 = DateTime.Parse(encabezadoFech[i].Remove(0, encabezadoFech[i].Length / 2));

                    var con2 = db.DOCUMENTOPs
                                      .Where(x => x.NUM_DOC.Equals(v.num_doc) & x.VIGENCIA_DE == a1 && x.VIGENCIA_AL == a2)
                                      .Join(db.MATERIALs, x => x.MATNR, y => y.ID, (x, y) => new { x.MATNR, x.MATKL, y.MAKTX, x.MONTO, y.PUNIT, x.PORC_APOYO, x.MONTO_APOYO, resta = (x.MONTO - x.MONTO_APOYO), x.PRECIO_SUG, x.APOYO_EST, x.APOYO_REAL, x.VIGENCIA_DE, x.VIGENCIA_AL }) //B20180710 MGC 2018.07.19
                                      .ToList();


                    if (con2.Count > 0)
                    {
                        foreach (var item2 in con2)
                        {
                            //B20180710 MGC 2018.07.17 Pasar los documentos almacenados pero con los nuevos valores editados
                            //B20180710 MGC 2018.07.10 Modificaciones para editar los campos de distribución se agrego los objetos
                            //armadoCuerpoTab.Add(item2.MATNR.TrimStart('0'));
                            //armadoCuerpoTab.Add(item2.MATKL);
                            //armadoCuerpoTab.Add(item2.MAKTX);                        
                            //if (v.costoun_x == true) { armadoCuerpoTab.Add(Math.Round(item2.MONTO, 2).ToString()); }
                            //if (v.apoyo_x == true) { armadoCuerpoTab.Add(Math.Round(item2.PORC_APOYO, 2).ToString()); }
                            //if (v.apoyop_x == true) { armadoCuerpoTab.Add(Math.Round(item2.MONTO_APOYO, 2).ToString()); }
                            //if (v.costoap_x == true) { armadoCuerpoTab.Add(Math.Round(item2.resta, 2).ToString()); }
                            //if (v.precio_x == true) { armadoCuerpoTab.Add(Math.Round(item2.PRECIO_SUG, 2).ToString()); }
                            //if (v.apoyoEst_x == true) { armadoCuerpoTab.Add(Math.Round(Convert.ToDouble(item2.APOYO_EST), 2).ToString()); }
                            //if (v.apoyoRea_x == true) { armadoCuerpoTab.Add(Math.Round(Convert.ToDouble(item2.APOYO_REAL), 2).ToString()); }
                            DOCUMENTOP_MOD docmod = new DOCUMENTOP_MOD();

                            try
                            {
                                docmod = v.DOCUMENTOP.Where(x => x.MATNR == item2.MATNR.TrimStart('0')).FirstOrDefault();

                                if (docmod != null)
                                {
                                    armadoCuerpoTab.Add(item2.MATNR.TrimStart('0'));
                                    armadoCuerpoTab.Add(item2.MATKL);
                                    armadoCuerpoTab.Add(item2.MAKTX);

                                    if (v.costoun_x == true) { armadoCuerpoTab.Add(Math.Round(docmod.MONTO, 2).ToString()); }
                                    if (v.apoyo_x == true) { armadoCuerpoTab.Add(Math.Round(docmod.PORC_APOYO, 2).ToString()); }
                                    if (v.apoyop_x == true) { armadoCuerpoTab.Add(Math.Round(docmod.MONTO_APOYO, 2).ToString()); }
                                    if (v.costoap_x == true) { armadoCuerpoTab.Add(Math.Round((docmod.MONTO - docmod.MONTO_APOYO), 2).ToString()); }
                                    if (v.precio_x == true) { armadoCuerpoTab.Add(Math.Round(docmod.PRECIO_SUG, 2).ToString()); }
                                    ////B20180710 MGC 2018.07.12 Apoyo es real o es estimado
                                    ////fact = true es real
                                    ////Apoyo
                                    //if (fact)
                                    //{
                                    //    if (v.apoyoRea_x == true) { armadoCuerpoTab.Add(Math.Round(Convert.ToDouble(docmod.APOYO_REAL), 2).ToString()); }
                                    //}
                                    //else
                                    //{
                                    //    if (v.apoyoEst_x == true) { armadoCuerpoTab.Add(Math.Round(Convert.ToDouble(docmod.APOYO_EST), 2).ToString()); }
                                    //}
                                    //Volumen
                                    //Volumen
                                    if (v.apoyoEst_x == true)
                                    {
                                        if (fact)
                                        {
                                            armadoCuerpoTab.Add(Math.Round(Convert.ToDouble(docmod.VOLUMEN_REAL), 2).ToString());
                                            //carp.VOLUMEN_REAL = docmod.VOLUMEN_REAL;
                                        }
                                        else
                                        {
                                            armadoCuerpoTab.Add(Math.Round(Convert.ToDouble(docmod.VOLUMEN_EST), 2).ToString());
                                            //carp.VOLUMEN_EST = docmod.VOLUMEN_EST;
                                        }
                                    }

                                    //Apoyo
                                    if (v.apoyoRea_x == true)
                                    {
                                        if (fact)
                                        {
                                            armadoCuerpoTab.Add(Math.Round(Convert.ToDouble(docmod.APOYO_REAL), 2).ToString());
                                        }
                                        else
                                        {
                                            armadoCuerpoTab.Add(Math.Round(Convert.ToDouble(docmod.APOYO_EST), 2).ToString());
                                        }
                                    }

                                    //Guardar carta
                                    if (guardar_param == "guardar_param")
                                    {
                                        CARTAP carp = new CARTAP();
                                        //Armado para registro en bd
                                        carp.NUM_DOC = v.num_doc;
                                        carp.POS_ID = pos;
                                        carp.POS = indexp;
                                        //carp.MATNR = item2.MATNR.TrimStart('0');
                                        carp.MATNR = item2.MATNR;
                                        carp.MATKL = item2.MATKL;
                                        carp.CANTIDAD = 1;
                                        if (v.costoun_x == true) { carp.MONTO = docmod.MONTO; }
                                        if (v.apoyo_x == true) { carp.PORC_APOYO = docmod.PORC_APOYO; }
                                        if (v.apoyop_x == true) { carp.MONTO_APOYO = docmod.MONTO_APOYO; }
                                        if (v.precio_x == true) { carp.PRECIO_SUG = docmod.PRECIO_SUG; }

                                        //Volumen
                                        if (v.apoyoEst_x == true)
                                        {
                                            if (fact)
                                            {
                                                carp.VOLUMEN_REAL = docmod.VOLUMEN_REAL;
                                                carp.VOLUMEN_EST = 0;
                                            }
                                            else
                                            {
                                                carp.VOLUMEN_EST = docmod.VOLUMEN_EST;
                                                carp.VOLUMEN_REAL = 0;
                                            }
                                        }

                                        //Apoyo
                                        if (v.apoyoRea_x == true)
                                        {
                                            if (fact)
                                            {
                                                carp.APOYO_REAL = docmod.APOYO_REAL;
                                                carp.APOYO_EST = 0;
                                            }
                                            else
                                            {
                                                carp.APOYO_EST = docmod.APOYO_EST;
                                                carp.APOYO_REAL = 0;
                                            }
                                        }

                                        //Fechas
                                        carp.VIGENCIA_DE = item2.VIGENCIA_DE;
                                        carp.VIGENCIA_AL = item2.VIGENCIA_AL;

                                        try
                                        {
                                            //Guardar en CARPETAP
                                            db.CARTAPs.Add(carp);
                                            db.SaveChanges();
                                            indexp++;
                                        }
                                        catch (Exception e)
                                        {

                                        }
                                    }

                                }
                            }
                            catch (Exception e)
                            {

                            }
                            contadorTabla++;
                        }
                    }
                    else
                    {
                        var con3 = db.DOCUMENTOPs
                                            .Where(x => x.NUM_DOC.Equals(v.num_doc) & x.VIGENCIA_DE == a1 && x.VIGENCIA_AL == a2)
                                            .Join(db.MATERIALGPs, x => x.MATKL, y => y.ID, (x, y) => new { x.NUM_DOC, x.MATNR, x.MATKL, y.ID, x.MONTO, x.PORC_APOYO, y.MATERIALGPTs.Where(a => a.SPRAS_ID.Equals(d.CLIENTE.SPRAS)).FirstOrDefault().TXT50, x.MONTO_APOYO, resta = (x.MONTO - x.MONTO_APOYO), x.PRECIO_SUG, x.APOYO_EST, x.APOYO_REAL, x.VIGENCIA_DE, x.VIGENCIA_AL }) //B20180710 MGC 2018.07.19
                                            .ToList();

                        foreach (var item2 in con3)
                        {
                            //B20180710 MGC 2018.07.17 Pasar los documentos almacenados pero con los nuevos valores editados
                            //B20180710 MGC 2018.07.10 Modificaciones para editar los campos de distribución se agrego los objetos
                            //armadoCuerpoTab.Add("");
                            //armadoCuerpoTab.Add(item2.MATKL);
                            //armadoCuerpoTab.Add(item2.TXT50);
                            //if (v.costoun_x == true) { armadoCuerpoTab.Add(Math.Round(item2.MONTO, 2).ToString()); }
                            //if (v.apoyo_x == true) { armadoCuerpoTab.Add(Math.Round(item2.PORC_APOYO, 2).ToString()); }
                            //if (v.apoyop_x == true) { armadoCuerpoTab.Add(Math.Round(item2.MONTO_APOYO, 2).ToString()); }
                            //if (v.costoap_x == true) { armadoCuerpoTab.Add(Math.Round(item2.resta, 2).ToString()); }
                            //if (v.precio_x == true) { armadoCuerpoTab.Add(Math.Round(item2.PRECIO_SUG, 2).ToString()); }
                            //if (v.apoyoEst_x == true) { armadoCuerpoTab.Add(Math.Round(Convert.ToDouble(item2.APOYO_EST), 2).ToString()); }
                            //if (v.apoyoRea_x == true) { armadoCuerpoTab.Add(Math.Round(Convert.ToDouble(item2.APOYO_REAL), 2).ToString()); }
                            DOCUMENTOP_MOD docmod = new DOCUMENTOP_MOD();

                            try
                            {
                                docmod = v.DOCUMENTOP.Where(x => x.MATKL_ID == item2.MATKL).FirstOrDefault();

                                if (docmod != null)
                                {
                                    armadoCuerpoTab.Add("");
                                    armadoCuerpoTab.Add(item2.MATKL);
                                    armadoCuerpoTab.Add(item2.TXT50);

                                    if (v.costoun_x == true) { armadoCuerpoTab.Add(Math.Round(docmod.MONTO, 2).ToString()); }
                                    if (v.apoyo_x == true) { armadoCuerpoTab.Add(Math.Round(docmod.PORC_APOYO, 2).ToString()); }
                                    if (v.apoyop_x == true) { armadoCuerpoTab.Add(Math.Round(docmod.MONTO_APOYO, 2).ToString()); }
                                    if (v.costoap_x == true) { armadoCuerpoTab.Add(Math.Round((docmod.MONTO - docmod.MONTO_APOYO), 2).ToString()); }
                                    if (v.precio_x == true) { armadoCuerpoTab.Add(Math.Round(docmod.PRECIO_SUG, 2).ToString()); }
                                    //B20180710 MGC 2018.07.12 Apoyo es real o es estimado
                                    //fact = true es real
                                    //Apoyo
                                    //if (fact)
                                    //{
                                    //    if (v.apoyoRea_x == true) { armadoCuerpoTab.Add(Math.Round(Convert.ToDouble(docmod.APOYO_REAL), 2).ToString()); }
                                    //}
                                    //else
                                    //{
                                    //    if (v.apoyoEst_x == true) { armadoCuerpoTab.Add(Math.Round(Convert.ToDouble(docmod.APOYO_EST), 2).ToString()); }
                                    //}

                                    //Volumen
                                    if (v.apoyoEst_x == true)
                                    {
                                        if (fact)
                                        {
                                            armadoCuerpoTab.Add(Math.Round(Convert.ToDouble(docmod.VOLUMEN_REAL), 2).ToString());
                                            //carp.VOLUMEN_REAL = docmod.VOLUMEN_REAL;
                                        }
                                        else
                                        {
                                            armadoCuerpoTab.Add(Math.Round(Convert.ToDouble(docmod.VOLUMEN_EST), 2).ToString());
                                            //carp.VOLUMEN_EST = docmod.VOLUMEN_EST;
                                        }
                                    }

                                    //Apoyo
                                    if (v.apoyoRea_x == true)
                                    {
                                        if (fact)
                                        {
                                            armadoCuerpoTab.Add(Math.Round(Convert.ToDouble(docmod.APOYO_REAL), 2).ToString());
                                        }
                                        else
                                        {
                                            armadoCuerpoTab.Add(Math.Round(Convert.ToDouble(docmod.APOYO_EST), 2).ToString());
                                        }
                                    }


                                    //Guardar carta
                                    if (guardar_param == "guardar_param")
                                    {
                                        CARTAP carp = new CARTAP();
                                        //Armado para registro en bd
                                        carp.NUM_DOC = v.num_doc;
                                        carp.POS_ID = pos;
                                        carp.POS = indexp;
                                        carp.MATNR = "";
                                        carp.MATKL = item2.MATKL;
                                        carp.CANTIDAD = 1;
                                        if (v.costoun_x == true) { carp.MONTO = docmod.MONTO; }
                                        if (v.apoyo_x == true) { carp.PORC_APOYO = docmod.PORC_APOYO; }
                                        if (v.apoyop_x == true) { carp.MONTO_APOYO = docmod.MONTO_APOYO; }
                                        if (v.precio_x == true) { carp.PRECIO_SUG = docmod.PRECIO_SUG; }

                                        //Volumen
                                        if (v.apoyoEst_x == true)
                                        {
                                            if (fact)
                                            {
                                                carp.VOLUMEN_REAL = docmod.VOLUMEN_REAL;
                                                carp.VOLUMEN_EST = 0;
                                            }
                                            else
                                            {
                                                carp.VOLUMEN_EST = docmod.VOLUMEN_EST;
                                                carp.VOLUMEN_REAL = 0;
                                            }
                                        }

                                        //Apoyo
                                        if (v.apoyoRea_x == true)
                                        {
                                            if (fact)
                                            {
                                                carp.APOYO_REAL = docmod.APOYO_REAL;
                                                carp.APOYO_EST = 0;
                                            }
                                            else
                                            {
                                                carp.APOYO_REAL = 0;
                                                carp.APOYO_EST = docmod.APOYO_EST;
                                            }
                                        }

                                        //Fechas
                                        carp.VIGENCIA_DE = item2.VIGENCIA_DE;
                                        carp.VIGENCIA_AL = item2.VIGENCIA_AL;

                                        try
                                        {
                                            //Guardar en CARPETAP
                                            db.CARTAPs.Add(carp);
                                            db.SaveChanges();
                                            indexp++;
                                        }
                                        catch (Exception e)
                                        {

                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {

                            }
                            contadorTabla++;
                        }
                    }
                    numfilasTab.Add(contadorTabla);
                }

                var cabeza = new List<string>();
                cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "materialC").Select(x => x.TEXTO).FirstOrDefault());
                cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "categoriaC").Select(x => x.TEXTO).FirstOrDefault());
                cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "descripcionC").Select(x => x.TEXTO).FirstOrDefault());
                if (v.costoun_x == true) { cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "costouC").Select(x => x.TEXTO).FirstOrDefault()); }
                if (v.apoyo_x == true) { cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "apoyopoC").Select(x => x.TEXTO).FirstOrDefault()); }
                if (v.apoyop_x == true) { cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "apoyopiC").Select(x => x.TEXTO).FirstOrDefault()); }
                if (v.costoap_x == true) { cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "costoaC").Select(x => x.TEXTO).FirstOrDefault()); }
                if (v.precio_x == true) { cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "preciosC").Select(x => x.TEXTO).FirstOrDefault()); }
                //B20180710 MGC 2018.07.12 Apoyo es real o es estimado
                //fact = true es real
                //Volumen
                if (v.apoyoEst_x == true)
                {
                    if (fact)
                    {
                        cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "volumenrC").Select(x => x.TEXTO).FirstOrDefault());
                    }
                    else
                    {
                        cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "volumeneC").Select(x => x.TEXTO).FirstOrDefault());
                    }
                }
                //Apoyo
                if (v.apoyoRea_x == true)
                {
                    if (fact)
                    {
                        cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "apoyorC").Select(x => x.TEXTO).FirstOrDefault());
                    }
                    else
                    {
                        cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "apoyoeC").Select(x => x.TEXTO).FirstOrDefault());
                    }
                }

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                /////////////////////////////////////////////DATOS PARA LA TABLA 2 RECURRENCIAS EN PDF///////////////////////////////////////
                var cabeza2 = new List<string>();
                cabeza2.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "posC2").Select(x => x.TEXTO).FirstOrDefault());
                cabeza2.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "tipoC2").Select(x => x.TEXTO).FirstOrDefault());
                cabeza2.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "fechaC2").Select(x => x.TEXTO).FirstOrDefault());
                cabeza2.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "montoC2").Select(x => x.TEXTO).FirstOrDefault());
                cabeza2.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "porcentajeC2").Select(x => x.TEXTO).FirstOrDefault());

                var con4 = db.DOCUMENTORECs
                                            .Where(x => x.NUM_DOC.Equals(v.num_doc))
                                            .Join(db.DOCUMENTOes, x => x.NUM_DOC, y => y.NUM_DOC, (x, y) => new { x.POS, y.TSOL_ID, x.FECHAF, x.MONTO_BASE, x.PORC })
                                            .ToList();

                foreach (var item in con4)
                {
                    DateTime a = Convert.ToDateTime(item.FECHAF);
                    armadoCuerpoTab2.Add(item.POS.ToString());
                    armadoCuerpoTab2.Add(db.TSOLs.Where(x => x.ID == item.TSOL_ID).Select(x => x.DESCRIPCION).First());
                    armadoCuerpoTab2.Add(a.ToShortDateString());
                    armadoCuerpoTab2.Add(item.MONTO_BASE.ToString());
                    armadoCuerpoTab2.Add(item.PORC.ToString());
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //MARCA DE AGUA
                bool aprob = false;
                aprob = (d.ESTATUS_WF.Equals("A") | d.ESTATUS_WF.Equals("S"));

                //PARA LA TABLA 1 MATERIALES
                v.numColEncabezado = cabeza;
                v.listaFechas = encabezadoFech;
                v.numfilasTabla = numfilasTab;
                v.listaCuerpo = armadoCuerpoTab;
                //PARA LA TABLA 2 RECURRENCIAS
                v.numColEncabezado2 = cabeza2;
                v.numfilasTabla2 = con4.Count();
                v.listaCuerpoRec = armadoCuerpoTab2;

                CartaV carta = v;
                CartaVEsqueleto cve = new CartaVEsqueleto();
                cve.crearPDF(carta, user.SPRAS_ID, aprob);
                string recibeRuta = Convert.ToString(Session["rutaCompletaV"]);
                return RedirectToAction("Index", new { ruta = recibeRuta, ids = v.num_doc });
            }
        }

        // POST: CartaV/Details/5
        [HttpPost]
        //[ValidateAntiForgeryToken] //B20180710 MGC 2018.07.16 Modificaciones para editar los campos de distribución se agrego los objetos
        //public ActionResult Create([Bind(Include = "num_doc, listaCuerpo, DOCUMENTOP")] CartaV v)
        public ActionResult Visualizar(CartaV v, string monto_enviar)
        {
            using (TAT001Entities db = new TAT001Entities())
            {
                string u = User.Identity.Name;
                var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();

                List<string> encabezadoFech = new List<string>();
                List<string> armadoCuerpoTab = new List<string>(); //B20180710 MGC 2018.07.10 Modificaciones para editar los campos de distribución se agrego los objetos
                List<string> armadoCuerpoTab2 = new List<string>();
                List<int> numfilasTab = new List<int>();

                int contadorTabla = 0;
                DOCUMENTO d = db.DOCUMENTOes.Find(v.num_doc);

                /////////////////////////////////////////////DATOS PARA LA TABLA 1 MATERIALES EN EL PDF///////////////////////////////////////
                var con = db.DOCUMENTOPs.Select(x => new { x.NUM_DOC, x.VIGENCIA_DE, x.VIGENCIA_AL }).Where(a => a.NUM_DOC.Equals(v.num_doc)).GroupBy(f => new { f.VIGENCIA_DE, f.VIGENCIA_AL }).ToList();
                //B20180710 MGC 2018.07.17 Modificación de monto
                v.monto = monto_enviar;
                //B20180710 MGC 2018.07.17 Modificación 9 y 10 dependiendo del campo de factura en tsol............
                bool fact = false;
                try
                {
                    fact = db.TSOLs.Where(ts => ts.ID == d.TSOL_ID).FirstOrDefault().FACTURA;
                }
                catch (Exception)
                {

                }
                //B20180710 MGC 2018.07.17 Modificación 9 y 10 dependiendo del campo de factura en tsol..............

                foreach (var item in con)
                {
                    encabezadoFech.Add(item.Key.VIGENCIA_DE.ToString() + item.Key.VIGENCIA_AL.ToString());
                }

                for (int i = 0; i < encabezadoFech.Count; i++)
                {
                    contadorTabla = 0;
                    DateTime a1 = DateTime.Parse(encabezadoFech[i].Remove(encabezadoFech[i].Length / 2));
                    DateTime a2 = DateTime.Parse(encabezadoFech[i].Remove(0, encabezadoFech[i].Length / 2));

                    var con2 = db.DOCUMENTOPs
                                      .Where(x => x.NUM_DOC.Equals(v.num_doc) & x.VIGENCIA_DE == a1 && x.VIGENCIA_AL == a2)
                                      .Join(db.MATERIALs, x => x.MATNR, y => y.ID, (x, y) => new { x.MATNR, x.MATKL, y.MAKTX, x.MONTO, y.PUNIT, x.PORC_APOYO, x.MONTO_APOYO, resta = (x.MONTO - x.MONTO_APOYO), x.PRECIO_SUG, x.APOYO_EST, x.APOYO_REAL })
                                      .ToList();


                    if (con2.Count > 0)
                    {
                        foreach (var item2 in con2)
                        {
                            //B20180710 MGC 2018.07.17 Pasar los documentos almacenados pero con los nuevos valores editados
                            //B20180710 MGC 2018.07.10 Modificaciones para editar los campos de distribución se agrego los objetos
                            //armadoCuerpoTab.Add(item2.MATNR.TrimStart('0'));
                            //armadoCuerpoTab.Add(item2.MATKL);
                            //armadoCuerpoTab.Add(item2.MAKTX);                        
                            //if (v.costoun_x == true) { armadoCuerpoTab.Add(Math.Round(item2.MONTO, 2).ToString()); }
                            //if (v.apoyo_x == true) { armadoCuerpoTab.Add(Math.Round(item2.PORC_APOYO, 2).ToString()); }
                            //if (v.apoyop_x == true) { armadoCuerpoTab.Add(Math.Round(item2.MONTO_APOYO, 2).ToString()); }
                            //if (v.costoap_x == true) { armadoCuerpoTab.Add(Math.Round(item2.resta, 2).ToString()); }
                            //if (v.precio_x == true) { armadoCuerpoTab.Add(Math.Round(item2.PRECIO_SUG, 2).ToString()); }
                            //if (v.apoyoEst_x == true) { armadoCuerpoTab.Add(Math.Round(Convert.ToDouble(item2.APOYO_EST), 2).ToString()); }
                            //if (v.apoyoRea_x == true) { armadoCuerpoTab.Add(Math.Round(Convert.ToDouble(item2.APOYO_REAL), 2).ToString()); }
                            DOCUMENTOP_MOD docmod = new DOCUMENTOP_MOD();

                            try
                            {
                                docmod = v.DOCUMENTOP.Where(x => x.MATNR == item2.MATNR.TrimStart('0')).FirstOrDefault();

                                if (docmod != null)
                                {
                                    armadoCuerpoTab.Add(item2.MATNR.TrimStart('0'));
                                    armadoCuerpoTab.Add(item2.MATKL);
                                    armadoCuerpoTab.Add(item2.MAKTX);

                                    if (v.costoun_x == true) { armadoCuerpoTab.Add(Math.Round(docmod.MONTO, 2).ToString()); }
                                    if (v.apoyo_x == true) { armadoCuerpoTab.Add(Math.Round(docmod.PORC_APOYO, 2).ToString()); }
                                    if (v.apoyop_x == true) { armadoCuerpoTab.Add(Math.Round(docmod.MONTO_APOYO, 2).ToString()); }
                                    if (v.costoap_x == true) { armadoCuerpoTab.Add(Math.Round((docmod.MONTO - docmod.MONTO_APOYO), 2).ToString()); }
                                    if (v.precio_x == true) { armadoCuerpoTab.Add(Math.Round(docmod.PRECIO_SUG, 2).ToString()); }
                                    //B20180710 MGC 2018.07.12 Apoyo es real o es estimado
                                    //fact = true es real
                                    //Apoyo
                                    if (fact)
                                    {
                                        if (v.apoyoRea_x == true) { armadoCuerpoTab.Add(Math.Round(Convert.ToDouble(docmod.APOYO_REAL), 2).ToString()); }
                                    }
                                    else
                                    {
                                        if (v.apoyoEst_x == true) { armadoCuerpoTab.Add(Math.Round(Convert.ToDouble(docmod.APOYO_EST), 2).ToString()); }
                                    }
                                }
                            }
                            catch (Exception e)
                            {

                            }
                            contadorTabla++;
                        }
                    }
                    else
                    {
                        var con3 = db.DOCUMENTOPs
                                            .Where(x => x.NUM_DOC.Equals(v.num_doc) & x.VIGENCIA_DE == a1 && x.VIGENCIA_AL == a2)
                                            .Join(db.MATERIALGPs, x => x.MATKL, y => y.ID, (x, y) => new { x.NUM_DOC, x.MATNR, x.MATKL, y.ID, x.MONTO, x.PORC_APOYO, y.MATERIALGPTs.Where(a => a.SPRAS_ID.Equals(d.CLIENTE.SPRAS)).FirstOrDefault().TXT50, x.MONTO_APOYO, resta = (x.MONTO - x.MONTO_APOYO), x.PRECIO_SUG, x.APOYO_EST, x.APOYO_REAL })
                                            .ToList();

                        foreach (var item2 in con3)
                        {
                            //B20180710 MGC 2018.07.17 Pasar los documentos almacenados pero con los nuevos valores editados
                            //B20180710 MGC 2018.07.10 Modificaciones para editar los campos de distribución se agrego los objetos
                            //armadoCuerpoTab.Add("");
                            //armadoCuerpoTab.Add(item2.MATKL);
                            //armadoCuerpoTab.Add(item2.TXT50);
                            //if (v.costoun_x == true) { armadoCuerpoTab.Add(Math.Round(item2.MONTO, 2).ToString()); }
                            //if (v.apoyo_x == true) { armadoCuerpoTab.Add(Math.Round(item2.PORC_APOYO, 2).ToString()); }
                            //if (v.apoyop_x == true) { armadoCuerpoTab.Add(Math.Round(item2.MONTO_APOYO, 2).ToString()); }
                            //if (v.costoap_x == true) { armadoCuerpoTab.Add(Math.Round(item2.resta, 2).ToString()); }
                            //if (v.precio_x == true) { armadoCuerpoTab.Add(Math.Round(item2.PRECIO_SUG, 2).ToString()); }
                            //if (v.apoyoEst_x == true) { armadoCuerpoTab.Add(Math.Round(Convert.ToDouble(item2.APOYO_EST), 2).ToString()); }
                            //if (v.apoyoRea_x == true) { armadoCuerpoTab.Add(Math.Round(Convert.ToDouble(item2.APOYO_REAL), 2).ToString()); }
                            DOCUMENTOP_MOD docmod = new DOCUMENTOP_MOD();

                            try
                            {
                                docmod = v.DOCUMENTOP.Where(x => x.MATKL_ID == item2.MATKL).FirstOrDefault();

                                if (docmod != null)
                                {
                                    armadoCuerpoTab.Add("");
                                    armadoCuerpoTab.Add(item2.MATKL);
                                    armadoCuerpoTab.Add(item2.TXT50);

                                    if (v.costoun_x == true) { armadoCuerpoTab.Add(Math.Round(docmod.MONTO, 2).ToString()); }
                                    if (v.apoyo_x == true) { armadoCuerpoTab.Add(Math.Round(docmod.PORC_APOYO, 2).ToString()); }
                                    if (v.apoyop_x == true) { armadoCuerpoTab.Add(Math.Round(docmod.MONTO_APOYO, 2).ToString()); }
                                    if (v.costoap_x == true) { armadoCuerpoTab.Add(Math.Round((docmod.MONTO - docmod.MONTO_APOYO), 2).ToString()); }
                                    if (v.precio_x == true) { armadoCuerpoTab.Add(Math.Round(docmod.PRECIO_SUG, 2).ToString()); }
                                    //B20180710 MGC 2018.07.12 Apoyo es real o es estimado
                                    //fact = true es real
                                    //Apoyo
                                    if (fact)
                                    {
                                        if (v.apoyoRea_x == true) { armadoCuerpoTab.Add(Math.Round(Convert.ToDouble(docmod.APOYO_REAL), 2).ToString()); }
                                    }
                                    else
                                    {
                                        if (v.apoyoEst_x == true) { armadoCuerpoTab.Add(Math.Round(Convert.ToDouble(docmod.APOYO_EST), 2).ToString()); }
                                    }
                                }
                            }
                            catch (Exception e)
                            {

                            }
                            contadorTabla++;
                        }
                    }
                    numfilasTab.Add(contadorTabla);
                }

                var cabeza = new List<string>();
                cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "materialC").Select(x => x.TEXTO).FirstOrDefault());
                cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "categoriaC").Select(x => x.TEXTO).FirstOrDefault());
                cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "descripcionC").Select(x => x.TEXTO).FirstOrDefault());
                if (v.costoun_x == true) { cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "costouC").Select(x => x.TEXTO).FirstOrDefault()); }
                if (v.apoyo_x == true) { cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "apoyopoC").Select(x => x.TEXTO).FirstOrDefault()); }
                if (v.apoyop_x == true) { cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "apoyopiC").Select(x => x.TEXTO).FirstOrDefault()); }
                if (v.costoap_x == true) { cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "costoaC").Select(x => x.TEXTO).FirstOrDefault()); }
                if (v.precio_x == true) { cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "preciosC").Select(x => x.TEXTO).FirstOrDefault()); }
                //B20180710 MGC 2018.07.12 Apoyo es real o es estimado
                //fact = true es real
                //Apoyo
                if (fact)
                {
                    if (v.apoyoRea_x == true) { cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "apoyorC").Select(x => x.TEXTO).FirstOrDefault()); }
                }
                else
                {
                    if (v.apoyoEst_x == true) { cabeza.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "apoyoeC").Select(x => x.TEXTO).FirstOrDefault()); }
                }

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                /////////////////////////////////////////////DATOS PARA LA TABLA 2 RECURRENCIAS EN PDF///////////////////////////////////////
                var cabeza2 = new List<string>();
                cabeza2.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "posC2").Select(x => x.TEXTO).FirstOrDefault());
                cabeza2.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "tipoC2").Select(x => x.TEXTO).FirstOrDefault());
                cabeza2.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "fechaC2").Select(x => x.TEXTO).FirstOrDefault());
                cabeza2.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "montoC2").Select(x => x.TEXTO).FirstOrDefault());
                cabeza2.Add(db.TEXTOCVs.Where(x => x.SPRAS_ID == user.SPRAS_ID & x.CAMPO == "porcentajeC2").Select(x => x.TEXTO).FirstOrDefault());

                var con4 = db.DOCUMENTORECs
                                            .Where(x => x.NUM_DOC.Equals(v.num_doc))
                                            .Join(db.DOCUMENTOes, x => x.NUM_DOC, y => y.NUM_DOC, (x, y) => new { x.POS, y.TSOL_ID, x.FECHAF, x.MONTO_BASE, x.PORC })
                                            .ToList();

                foreach (var item in con4)
                {
                    DateTime a = Convert.ToDateTime(item.FECHAF);
                    armadoCuerpoTab2.Add(item.POS.ToString());
                    armadoCuerpoTab2.Add(db.TSOLs.Where(x => x.ID == item.TSOL_ID).Select(x => x.DESCRIPCION).First());
                    armadoCuerpoTab2.Add(a.ToShortDateString());
                    armadoCuerpoTab2.Add(item.MONTO_BASE.ToString());
                    armadoCuerpoTab2.Add(item.PORC.ToString());
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //MARCA DE AGUA
                bool aprob = false;
                aprob = (d.ESTATUS_WF.Equals("A") | d.ESTATUS_WF.Equals("S"));

                //PARA LA TABLA 1 MATERIALES
                v.numColEncabezado = cabeza;
                v.listaFechas = encabezadoFech;
                v.numfilasTabla = numfilasTab;
                v.listaCuerpo = armadoCuerpoTab;
                //PARA LA TABLA 2 RECURRENCIAS
                v.numColEncabezado2 = cabeza2;
                v.numfilasTabla2 = con4.Count();
                v.listaCuerpoRec = armadoCuerpoTab2;

                CartaV carta = v;
                CartaVEsqueleto cve = new CartaVEsqueleto();
                cve.crearPDF(carta, user.SPRAS_ID, aprob);
                string recibeRuta = Convert.ToString(Session["rutaCompletaV"]);
                return RedirectToAction("Index", new { ruta = recibeRuta, ids = v.num_doc });
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

        //B20180710 MGC 2018.07.13 Modificaciones para editar los campos de distribución se agrego los objetos
        [HttpPost]
        public ActionResult getPartialMat(List<DOCUMENTOP_MOD> docs)
        {

            CartaV doc = new CartaV();

            doc.DOCUMENTOP = docs;
            return PartialView("~/Views/CartaV/_PartialMatTr.cshtml", doc);
        }
    }
}