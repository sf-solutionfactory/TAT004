using ExcelDataReader;
using SimpleImpersonation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using TAT001.Entities;
using TAT001.Models;

namespace TAT001.Controllers
{
    [Authorize]
    public class SolicitudesController : Controller
    {
        private TAT001Entities db = new TAT001Entities();

        // GET: Solicitudes
        public ActionResult Index()
        {
            int pagina = 201; //ID EN BASE DE DATOS
            using (TAT001Entities db = new TAT001Entities())
            {
                string u = User.Identity.Name;
                //string u = "admin";
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
                    //ViewBag.pais = "mx.svg";
                    return RedirectToAction("Pais", "Home");
                }
                Session["spras"] = user.SPRAS_ID;
            }

            try//Mensaje de documento creado
            {
                string p = Session["NUM_DOC"].ToString();
                ViewBag.NUM_DOC = p;
                Session["NUM_DOC"] = null;
            }
            catch
            {
                ViewBag.NUM_DOC = "";
            }

            try//Mensaje de documento creado
            {
                string error_files = Session["ERROR_FILES"].ToString();
                ViewBag.ERROR_FILES = error_files;
                Session["ERROR_FILES"] = null;
            }
            catch
            {
                ViewBag.ERROR_FILES = "";
            }



            var dOCUMENTOes = db.DOCUMENTOes.Where(a => a.USUARIOC_ID.Equals(User.Identity.Name)).Include(d => d.TALL).Include(d => d.TSOL).Include(d => d.USUARIO).Include(d => d.CLIENTE).Include(d => d.PAI).Include(d => d.SOCIEDAD).ToList();
            var dOCUMENTOVs = db.DOCUMENTOVs.Where(a => a.USUARIOA_ID.Equals(User.Identity.Name)).ToList();
            var tsol = db.TSOLs.ToList();
            var tall = db.TALLs.ToList();
            foreach (DOCUMENTOV v in dOCUMENTOVs)
            {
                DOCUMENTO d = new DOCUMENTO();
                var ppd = d.GetType().GetProperties();
                var ppv = v.GetType().GetProperties();
                foreach (var pv in ppv)
                {
                    foreach (var pd in ppd)
                    {
                        if (pd.Name == pv.Name)
                        {
                            pd.SetValue(d, pv.GetValue(v));
                            break;
                        }
                    }
                }
                d.TSOL = tsol.Where(a => a.ID.Equals(d.TSOL_ID)).FirstOrDefault();
                d.TALL = tall.Where(a => a.ID.Equals(d.TALL_ID)).FirstOrDefault();
                //d.ESTADO = db.STATES.Where(a => a.ID.Equals(v.ESTADO)).FirstOrDefault().NAME;
                //d.CIUDAD = db.CITIES.Where(a => a.ID.Equals(v.CIUDAD)).FirstOrDefault().NAME;
                dOCUMENTOes.Add(d);
            }
            dOCUMENTOes = dOCUMENTOes.Distinct(new DocumentoComparer()).ToList();
            return View(dOCUMENTOes);
        }

        // GET: Solicitudes/Details/5
        public ActionResult Details(decimal id, string pais)
        {
            int pagina = 203; //ID EN BASE DE DATOS
            using (TAT001Entities db = new TAT001Entities())
            {
                string u = User.Identity.Name;
                //string u = "admin";
                var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                ViewBag.permisos = db.PAGINAVs.Where(a => a.ID.Equals(user.ID)).ToList();
                ViewBag.carpetas = db.CARPETAVs.Where(a => a.USUARIO_ID.Equals(user.ID)).ToList();
                ViewBag.usuario = user;
                ViewBag.rol = user.MIEMBROS.FirstOrDefault().ROL.NOMBRE;
                ViewBag.Title = db.PAGINAs.Where(a => a.ID.Equals(pagina)).FirstOrDefault().PAGINATs.Where(b => b.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                ViewBag.Title += " " + id;
                ViewBag.warnings = db.WARNINGVs.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();
                ViewBag.textos = db.TEXTOes.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();

                try
                {
                    string p = Session["pais"].ToString();
                    ViewBag.pais = p + ".svg";
                }
                catch
                {
                    if (pais != "")
                    {
                        ViewBag.pais = pais + ".svg";
                        Session["pais"] = pais;
                    }
                    else
                    {
                        //ViewBag.pais = "mx.svg";
                        return RedirectToAction("Pais", "Home");
                    }
                }
                Session["spras"] = user.SPRAS_ID;
            }
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOCUMENTO dOCUMENTO = db.DOCUMENTOes.Find(id);
            if (dOCUMENTO == null)
            {
                return HttpNotFound();
            }
            dOCUMENTO.CLIENTE = db.CLIENTEs.Where(a => a.VKORG.Equals(dOCUMENTO.VKORG)
                                                    & a.VTWEG.Equals(dOCUMENTO.VTWEG)
                                                    & a.SPART.Equals(dOCUMENTO.SPART)
                                                    & a.KUNNR.Equals(dOCUMENTO.PAYER_ID)).First();
            ViewBag.workflow = db.FLUJOes.Where(a => a.NUM_DOC.Equals(id)).OrderBy(a => a.POS).ToList();
            FLUJO f = db.FLUJOes.Where(a => a.NUM_DOC.Equals(id) & a.ESTATUS.Equals("P") & a.USUARIOA_ID.Equals(User.Identity.Name)).FirstOrDefault();
            ViewBag.acciones = f;
            if (f != null)
                ViewBag.accion = db.WORKFPs.Where(a => a.ID.Equals(f.WORKF_ID) & a.POS.Equals(f.WF_POS) & a.VERSION.Equals(f.WF_VERSION)).FirstOrDefault().ACCION.TIPO;
            return View(dOCUMENTO);
        }

        // GET: Solicitudes/Create
        public ActionResult Create()
        {

            DOCUMENTO d = new DOCUMENTO();
            string errorString = "";
            int pagina = 202; //ID EN BASE DE DATOS
            using (TAT001Entities db = new TAT001Entities())
            {
                string p = "";
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
                    p = Session["pais"].ToString();
                    ViewBag.pais = p + ".svg";
                }
                catch
                {
                    ViewBag.pais = "mx.svg";
                    //return RedirectToAction("Pais", "Home");
                }
                //Tipo de solicitud
                try
                {
                    string st = Session["sol_tipo"].ToString();
                    ViewBag.soltipo = st;
                }
                catch
                {
                    return RedirectToAction("Index", "Solicitudes");
                }

                Session["spras"] = user.SPRAS_ID;

                //tipo de solicitud
                var id_sol = db.TSOLs.Where(sol => sol.ESTATUS != "X")
                                    .Join(
                                    db.TSOLTs.Where(solt => solt.SPRAS_ID == user.SPRAS_ID),
                                    sol => sol.ID,
                                    solt => solt.TSOL_ID,
                                    (sol, solt) => new
                                    {
                                        solt.SPRAS_ID,
                                        solt.TSOL_ID,
                                        TEXT = solt.TSOL_ID + " " + solt.TXT020
                                    })
                                .ToList();

                ViewBag.TSOL_ID = new SelectList(id_sol, "TSOL_ID", "TEXT");

                //Grupos de solicitud
                var id_grupo = db.GALLs.Where(g => g.ACTIVO == true)
                                .Join(
                                db.GALLTs.Where(gt => gt.SPRAS_ID == user.SPRAS_ID),
                                g => g.ID,
                                gt => gt.GALL_ID,
                                (g, gt) => new
                                {
                                    gt.SPRAS_ID,
                                    gt.GALL_ID,
                                    TEXT = g.DESCRIPCION + " " + gt.TXT50
                                }).ToList();

                ViewBag.GALL_ID = new SelectList(id_grupo, "GALL_ID", "TEXT");

                //Select clasificación
                //var id_clas = db.TALLs.Where(t => t.ACTIVO == true)
                //                .Join(
                //                db.TALLTs.Where(tallt => tallt.SPRAS_ID == user.SPRAS_ID),
                //                tall => tall.ID,
                //                tallt => tallt.TALL_ID,
                //                (tall, tallt) => tallt)
                //            .ToList();

                List<TAT001.Entities.GALL> id_clas = new List<TAT001.Entities.GALL>();
                ViewBag.TALL_ID = new SelectList(id_clas, "TALL_ID", "TXT50");


                //Datos del país
                var id_bukrs = db.SOCIEDADs.Where(soc => soc.LAND.Equals(p) && soc.ACTIVO == true).FirstOrDefault();
                var id_pais = db.PAIS.Where(pais => pais.LAND.Equals(id_bukrs.LAND)).FirstOrDefault();

                var id_states = (from st in db.STATES
                                 join co in db.COUNTRIES
                                 on st.COUNTRY_ID equals co.ID
                                 where co.SORTNAME.Equals(id_pais.LAND)
                                 select new
                                 {
                                     st.ID,
                                     st.NAME,
                                     st.COUNTRY_ID
                                 }).ToList();

                var id_waers = db.MONEDAs.Where(m => m.ACTIVO == true).ToList();

                List<TAT001.Entities.CITy> id_city = new List<TAT001.Entities.CITy>();

                //ViewBag.BUKRS = id_bukrs;
                ViewBag.SOCIEDAD_ID = id_bukrs;
                ViewBag.PAIS_ID = id_pais;
                ViewBag.STATE_ID = "";// new SelectList(id_states, "ID", dataTextField: "NAME");
                ViewBag.CITY_ID = "";// new SelectList(id_city, "ID", dataTextField: "NAME");
                ViewBag.MONEDA = new SelectList(id_waers, "WAERS", dataTextField: "WAERS", selectedValue: id_bukrs.WAERS);

                //Información del cliente
                var id_clientes = db.CLIENTEs.Where(c => c.LAND.Equals(p) && c.ACTIVO == true).ToList();

                ViewBag.PAYER_ID = new SelectList(id_clientes, "KUNNR", dataTextField: "NAME1");

                //Información de categorías

                var id_cat = db.CATEGORIAs.Where(c => c.ACTIVO == true)
                                .Join(
                                db.CATEGORIATs.Where(ct => ct.SPRAS_ID == user.SPRAS_ID),
                                c => c.ID,
                                ct => ct.CATEGORIA_ID,
                                (c, ct) => new
                                {                                    
                                    ct.CATEGORIA_ID,
                                    TEXT = ct.TXT50
                                }).ToList();

                ViewBag.CATEGORIA_ID = new SelectList(id_cat, "CATEGORIA_ID", "TEXT");

                d.SOCIEDAD_ID = id_bukrs.BUKRS;
                d.MONEDA_ID = id_bukrs.WAERS;
                var date = DateTime.Now.Date;
                TAT001.Entities.TCAMBIO tcambio = new TAT001.Entities.TCAMBIO();
                try
                {
                    tcambio = db.TCAMBIOs.Where(t => t.FCURR.Equals(id_bukrs.WAERS) && t.TCURR.Equals("USD") && t.GDATU.Equals(date)).FirstOrDefault();
                    if (tcambio == null)
                    {
                        var max = db.TCAMBIOs.Where(t => t.FCURR.Equals(id_bukrs.WAERS) && t.TCURR.Equals("USD")).Max(a => a.GDATU);
                        tcambio = db.TCAMBIOs.Where(t => t.FCURR.Equals(id_bukrs.WAERS) && t.TCURR.Equals("USD") && t.GDATU.Equals(max)).FirstOrDefault();
                    }
                    decimal con = Convert.ToDecimal(tcambio.UKURS);
                    var cons = con.ToString("0.##");

                    ViewBag.tcambio = cons;
                }
                catch (Exception e)
                {
                    errorString = e.Message + "detail: conversion " + id_bukrs.WAERS + " to " + "USD" + " in date " + DateTime.Now.Date;
                    ViewBag.tcambio = "";
                }


            }

            d.PERIODO = DateTime.Now.ToString("MM");
            d.EJERCICIO = Convert.ToString(DateTime.Now.Year);
            //ViewBag.FECHAD = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.FECHAD = DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.STCD1 = "";
            ViewBag.MONTO_DOC_ML2 = "";
            ViewBag.error = errorString;
            ViewBag.NAME1 = "";
            ViewBag.notas_soporte = "";

            return View(d);
        }

        // POST: Solicitudes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NUM_DOC,TSOL_ID,TALL_ID,SOCIEDAD_ID,PAIS_ID,ESTADO,CIUDAD,PERIODO," +
            "EJERCICIO,TIPO_TECNICO,TIPO_RECURRENTE,CANTIDAD_EV,USUARIOC_ID,FECHAD,FECHAC,ESTATUS,ESTATUS_C,ESTATUS_SAP," +
            "ESTATUS_WF,DOCUMENTO_REF,NOTAS,MONTO_DOC_MD,MONTO_FIJO_MD,MONTO_BASE_GS_PCT_MD,MONTO_BASE_NS_PCT_MD,MONTO_DOC_ML," +
            "MONTO_FIJO_ML,MONTO_BASE_GS_PCT_ML,MONTO_BASE_NS_PCT_ML,MONTO_DOC_ML2,MONTO_FIJO_ML2,MONTO_BASE_GS_PCT_ML2," +
            "MONTO_BASE_NS_PCT_ML2,IMPUESTO,FECHAI_VIG,FECHAF_VIG,ESTATUS_EXT,SOLD_TO_ID,PAYER_ID,GRUPO_CTE_ID,CANAL_ID," +
            "MONEDA_ID,TIPO_CAMBIO,NO_FACTURA,FECHAD_SOPORTE,METODO_PAGO,NO_PROVEEDOR,PASO_ACTUAL,AGENTE_ACTUAL,FECHA_PASO_ACTUAL," +
            "VKORG,VTWEG,SPART,HORAC,FECHAC_PLAN,FECHAC_USER,HORAC_USER,CONCEPTO,PORC_ADICIONAL,PAYER_NOMBRE,PAYER_EMAIL," +
            "MONEDAL_ID,MONEDAL2_ID,TIPO_CAMBIOL,TIPO_CAMBIOL2")] DOCUMENTO dOCUMENTO, IEnumerable<HttpPostedFileBase> files_soporte, string notas_soporte)
        {
            string errorString = "";
            SOCIEDAD id_bukrs = new SOCIEDAD();
            string p = "";
            if (ModelState.IsValid)
            {
                try
                {
                    //Provisional tipo de cambio
                    //dOCUMENTO.TIPO_CAMBIO = dOCUMENTO.TIPO_CAMBIO / 60000000000;
                    //dOCUMENTO.MONTO_DOC_ML2 = dOCUMENTO.MONTO_DOC_ML2 / 60000000000;

                    //Obtener datos ocultos o deshabilitados                    
                    try
                    {
                        p = Session["pais"].ToString();
                        ViewBag.pais = p + ".svg";
                    }
                    catch
                    {
                        ViewBag.pais = "mx.svg";
                        //return RedirectToAction("Pais", "Home");
                    }
                    //Obtener el número de documento
                    decimal N_DOC = getSolID(dOCUMENTO.TSOL_ID);
                    dOCUMENTO.NUM_DOC = N_DOC;

                    //Obtener SOCIEDAD_ID 
                    id_bukrs = db.SOCIEDADs.Where(soc => soc.LAND.Equals(p)).FirstOrDefault();
                    dOCUMENTO.SOCIEDAD_ID = id_bukrs.BUKRS;

                    //Obtener el país
                    dOCUMENTO.PAIS_ID = p.ToUpper();

                    //CANTIDAD_EV > 1 si son recurrentes
                    dOCUMENTO.CANTIDAD_EV = 1;

                    //Obtener usuarioc
                    dOCUMENTO.USUARIOC_ID = User.Identity.Name;

                    //Fechac
                    dOCUMENTO.FECHAC = DateTime.Now;

                    //Horac
                    dOCUMENTO.HORAC = DateTime.Now.TimeOfDay;

                    //FECHAC_PLAN
                    dOCUMENTO.FECHAC_PLAN = DateTime.Now.Date;

                    //FECHAC_USER
                    dOCUMENTO.FECHAC_USER = DateTime.Now.Date;

                    //HORAC_USER
                    dOCUMENTO.HORAC_USER = DateTime.Now.TimeOfDay;

                    //Estatus
                    dOCUMENTO.ESTATUS = "N";

                    //Estatus wf
                    dOCUMENTO.ESTATUS_WF = "P";

                    ///////////////////Montos
                    //MONTO_DOC_MD
                    var MONTO_DOC_MD = dOCUMENTO.MONTO_DOC_MD;
                    dOCUMENTO.MONTO_DOC_MD = Convert.ToDecimal(MONTO_DOC_MD);

                    //Obtener el monto de la sociedad
                    dOCUMENTO.MONTO_DOC_ML = getValSoc(id_bukrs.WAERS, dOCUMENTO.MONEDA_ID, Convert.ToDecimal(dOCUMENTO.MONTO_DOC_MD), out errorString);
                    if (!errorString.Equals(""))
                    {
                        throw new Exception();
                    }

                    //MONTO_DOC_ML2 
                    var MONTO_DOC_ML2 = dOCUMENTO.MONTO_DOC_ML2;
                    dOCUMENTO.MONTO_DOC_ML2 = Convert.ToDecimal(MONTO_DOC_ML2);

                    //MONEDAL_ID moneda de la sociedad
                    dOCUMENTO.MONEDAL_ID = id_bukrs.WAERS;

                    //MONEDAL2_ID moneda en USD
                    dOCUMENTO.MONEDAL2_ID = "USD";

                    //Tipo cambio de la moneda de la sociedad TIPO_CAMBIOL
                    dOCUMENTO.TIPO_CAMBIOL = getUkurs(id_bukrs.WAERS, dOCUMENTO.MONEDA_ID, out errorString);

                    //Tipo cambio dolares TIPO_CAMBIOL2
                    dOCUMENTO.TIPO_CAMBIOL2 = getUkursUSD(dOCUMENTO.MONEDA_ID, "USD", out errorString);
                    if (!errorString.Equals(""))
                    {
                        throw new Exception();
                    }
                    //Obtener datos del payer
                    CLIENTE payer = getCliente(dOCUMENTO.PAYER_ID);

                    dOCUMENTO.VKORG = payer.VKORG;
                    dOCUMENTO.VTWEG = payer.VTWEG;
                    dOCUMENTO.SPART = payer.SPART;


                    //Guardar el documento
                    db.DOCUMENTOes.Add(dOCUMENTO);
                    db.SaveChanges();

                    //Actualizar el rango
                    updateRango(dOCUMENTO.TSOL_ID, dOCUMENTO.NUM_DOC);

                    //Redireccionar al inicio
                    //Guardar número de documento creado
                    Session["NUM_DOC"] = dOCUMENTO.NUM_DOC;

                    //Guardar las notas
                    if (notas_soporte != null && notas_soporte != "")
                    {
                        DOCUMENTON doc_notas = new DOCUMENTON();
                        doc_notas.NUM_DOC = dOCUMENTO.NUM_DOC;
                        doc_notas.POS = 1;
                        doc_notas.STEP = 1;
                        doc_notas.USUARIO_ID = dOCUMENTO.USUARIOC_ID;
                        doc_notas.TEXTO = notas_soporte.ToString();

                        db.DOCUMENTONs.Add(doc_notas);
                        db.SaveChanges();
                    }

                    //Guardar los documentos cargados en la sección de soporte
                    var res = "";
                    string errorMessage = "";
                    int numFiles = 0;
                    //Checar si hay archivos para subir
                    foreach (HttpPostedFileBase file in files_soporte)
                    {
                        if (file != null)
                        {
                            if (file.ContentLength > 0)
                            {
                                numFiles++;
                            }
                        }
                    }

                    if (numFiles > 0) { 
                        //Obtener las variables con los datos de sesión y ruta
                        string url = ConfigurationManager.AppSettings["URL_SAVE"];
                        //Crear el directorio
                        var dir = createDir(url, dOCUMENTO.NUM_DOC.ToString());

                        //Evaluar que se creo el directorio
                        if (dir.Equals(""))
                        {
                            foreach (HttpPostedFileBase file in files_soporte)
                            {
                                string errorfiles = "";

                                if (file != null)
                                {
                                    if (file.ContentLength > 0)
                                    {
                                        string filename = file.FileName;
                                        errorfiles = "";
                                        res = SaveFile(file, url, dOCUMENTO.NUM_DOC.ToString(), out errorfiles);
                                    }
                                }
                                if (errorfiles != "")
                                {
                                    errorMessage += "Error con el archivo " + errorfiles;
                                }

                            }
                        }
                        else
                        {
                            errorMessage = dir;
                        }

                        errorString = errorMessage;
                        //Guardar número de documento creado
                        Session["ERROR_FILES"] = errorMessage;
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    if (errorString == "")
                    {
                        errorString = e.Message.ToString();
                    }
                    ViewBag.error = errorString;

                }
            }

            //ViewBag.TALL_ID = new SelectList(db.TALLs, "ID", "DESCRIPCION");
            //ViewBag.TSOL_ID = new SelectList(db.TSOLs, "ID", "DESCRIPCION");
            //ViewBag.USUARIOC_ID = new SelectList(db.USUARIOs, "ID", "NOMBRE");
            //ViewBag.VKORG = new SelectList(db.CLIENTEs, "VKORG", "NAME1");
            //ViewBag.VTWEG = new SelectList(db.CLIENTEs, "VTWEG", "NAME1");
            //ViewBag.SPART = new SelectList(db.CLIENTEs, "SPART", "NAME1");
            //ViewBag.PAYER_ID = new SelectList(db.CLIENTEs, "KUNNR", "NAME1");
            //ViewBag.PAIS_ID = new SelectList(db.PAIS, "LAND", "SPRAS");
            //ViewBag.SOCIEDAD_ID = new SelectList(db.SOCIEDADs, "BUKRS", "BUTXT");
            int pagina = 202; //ID EN BASE DE DATOS
            using (TAT001Entities db = new TAT001Entities())
            {
                //Obtener datos ocultos o deshabilitados                    
                try
                {
                    p = Session["pais"].ToString();
                    ViewBag.pais = p + ".svg";
                }
                catch
                {
                    ViewBag.pais = "mx.svg";
                    //return RedirectToAction("Pais", "Home");
                }
                string u = User.Identity.Name;
                //string u = "admin";
                var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                ViewBag.permisos = db.PAGINAVs.Where(a => a.ID.Equals(user.ID)).ToList();
                ViewBag.carpetas = db.CARPETAVs.Where(a => a.USUARIO_ID.Equals(user.ID)).ToList();
                ViewBag.usuario = user;
                ViewBag.rol = user.MIEMBROS.FirstOrDefault().ROL.NOMBRE;
                ViewBag.Title = db.PAGINAs.Where(a => a.ID.Equals(pagina)).FirstOrDefault().PAGINATs.Where(b => b.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                ViewBag.warnings = db.WARNINGVs.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();
                ViewBag.textos = db.TEXTOes.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();

                //tipo de solicitud
                var id_sol = db.TSOLs.Where(sol => sol.ESTATUS != "X")
                                    .Join(
                                    db.TSOLTs.Where(solt => solt.SPRAS_ID == user.SPRAS_ID),
                                    sol => sol.ID,
                                    solt => solt.TSOL_ID,
                                    (sol, solt) => new
                                    {
                                        solt.SPRAS_ID,
                                        solt.TSOL_ID,
                                        TEXT = solt.TSOL_ID + " " + solt.TXT020
                                    })
                                .ToList();

                ViewBag.TSOL_ID = new SelectList(id_sol, "TSOL_ID", "TEXT", selectedValue: dOCUMENTO.TSOL_ID);

                //                      

                //Select clasificación
                var id_clas = db.TALLs.Where(t => t.ACTIVO == true)
                                .Join(
                                db.TALLTs.Where(tallt => tallt.SPRAS_ID == user.SPRAS_ID),
                                tall => tall.ID,
                                tallt => tallt.TALL_ID,
                                (tall, tallt) => tallt)
                            .ToList();

                var id_clas_sel = db.TALLs.Where(t => t.ID == dOCUMENTO.TALL_ID).FirstOrDefault().GALL_ID;

                //Grupos de solicitud
                var id_grupo = db.GALLs.Where(g => g.ACTIVO == true)
                                .Join(
                                db.GALLTs.Where(gt => gt.SPRAS_ID == user.SPRAS_ID),
                                g => g.ID,
                                gt => gt.GALL_ID,
                                (g, gt) => new
                                {
                                    gt.SPRAS_ID,
                                    gt.GALL_ID,
                                    TEXT = g.DESCRIPCION + " " + gt.TXT50
                                }).ToList();

                var id_grupo_sel = id_grupo.Where(g => g.GALL_ID == id_clas_sel).FirstOrDefault().GALL_ID;

                ViewBag.GALL_ID = new SelectList(id_grupo, "GALL_ID", "TEXT", selectedValue: id_grupo_sel);

                //ViewBag.tall = db.TALLs.ToList();
                ViewBag.TALL_ID = new SelectList(id_clas, "TALL_ID", "TXT50", selectedValue: dOCUMENTO.TALL_ID);
                //Datos del país
                id_bukrs = db.SOCIEDADs.Where(soc => soc.LAND.Equals(p) && soc.ACTIVO == true).FirstOrDefault();
                var id_pais = db.PAIS.Where(pais => pais.LAND.Equals(id_bukrs.LAND)).FirstOrDefault();

                var id_states = (from st in db.STATES
                                 join co in db.COUNTRIES
                                 on st.COUNTRY_ID equals co.ID
                                 where co.SORTNAME.Equals(id_pais.LAND)
                                 select new
                                 {
                                     st.ID,
                                     st.NAME,
                                     st.COUNTRY_ID
                                 }).ToList();

                var id_waers = db.MONEDAs.Where(m => m.ACTIVO == true).ToList();

                List<TAT001.Entities.CITy> id_city = new List<TAT001.Entities.CITy>();

                //ViewBag.BUKRS = id_bukrs;
                ViewBag.SOCIEDAD_ID = id_bukrs;
                dOCUMENTO.SOCIEDAD_ID = id_bukrs.BUKRS;
                ViewBag.PAIS_ID = id_pais;
                ViewBag.STATE_ID = new SelectList(id_states, "ID", dataTextField: "NAME", selectedValue: dOCUMENTO.ESTADO);
                ViewBag.CITY_ID = new SelectList(id_city, "ID", dataTextField: "NAME");
                ViewBag.MONEDA = new SelectList(id_waers, "WAERS", dataTextField: "WAERS", selectedValue: id_bukrs.WAERS);

                //Información del cliente
                var id_clientes = db.CLIENTEs.Where(c => c.LAND.Equals(p) && c.ACTIVO == true).ToList();
                //Obtener datos del payer
                CLIENTE payer = getCliente(dOCUMENTO.PAYER_ID);

                dOCUMENTO.VKORG = payer.VKORG;
                dOCUMENTO.VTWEG = payer.VTWEG;
                dOCUMENTO.SPART = payer.SPART;
                ViewBag.STCD1 = payer.STCD1;
                ViewBag.NAME1 = payer.NAME1;

                try
                {
                    //Obtener y dar formato a fecha
                    var fecha = dOCUMENTO.FECHAD.ToString();
                    string[] words = fecha.Split(' ');

                    //DateTime theTime = DateTime.ParseExact(fecha, //"06/04/2018 12:00:00 a.m."
                    //                        "dd/MM/yyyy hh:mm:ss t.t.",
                    //                        System.Globalization.CultureInfo.InvariantCulture,
                    //                        System.Globalization.DateTimeStyles.None);

                    DateTime theTime = DateTime.ParseExact(words[0], //"06/04/2018 12:00:00 a.m."
                                            "dd/MM/yyyy",
                                            System.Globalization.CultureInfo.InvariantCulture,
                                            System.Globalization.DateTimeStyles.None);
                    ViewBag.FECHAD = theTime.ToString("yyyy-MM-dd");
                }
                catch (Exception e)
                {
                    ViewBag.FECHAD = "";
                }



                ViewBag.PAYER_ID = new SelectList(id_clientes, "KUNNR", dataTextField: "NAME1", selectedValue: dOCUMENTO.PAYER_ID);
            }

            ViewBag.tcambio = dOCUMENTO.TIPO_CAMBIO;
            ViewBag.MONTO_DOC_ML2 = dOCUMENTO.MONTO_DOC_ML2;
            if(notas_soporte == null || notas_soporte == "")
            {
                notas_soporte = "";
            }
            ViewBag.notas_soporte = notas_soporte;

            return View(dOCUMENTO);
        }

        public decimal getValSoc(string waers, string moneda_id, decimal monto_doc_md, out string errorString)
        {
            decimal val = 0;

            //Siempre la conversión va a la sociedad    

            var UKURS = getUkurs(waers, moneda_id, out errorString);

            if (errorString.Equals(""))
            {

                decimal uk = Convert.ToDecimal(UKURS);

                if (UKURS > 0)
                {
                    val = uk * Convert.ToDecimal(monto_doc_md);
                }
            }

            return val;
        }

        public decimal getUkurs(string waers, string moneda_id, out string errorString)
        {
            decimal ukurs = 0;
            errorString = string.Empty;
            using (TAT001Entities db = new TAT001Entities())
            {
                try
                {
                    //Siempre la conversión va a la sociedad    
                    var date = DateTime.Now.Date;
                    var tcambio = db.TCAMBIOs.Where(t => t.FCURR.Equals(moneda_id) && t.TCURR.Equals(waers) && t.GDATU.Equals(date)).FirstOrDefault();
                    if (tcambio == null)
                    {
                        var max = db.TCAMBIOs.Where(t => t.FCURR.Equals(moneda_id) && t.TCURR.Equals(waers)).Max(a => a.GDATU);
                        tcambio = db.TCAMBIOs.Where(t => t.FCURR.Equals(moneda_id) && t.TCURR.Equals(waers) && t.GDATU.Equals(max)).FirstOrDefault();
                    }

                    ukurs = Convert.ToDecimal(tcambio.UKURS);

                }
                catch (Exception e)
                {
                    errorString = "detail: conversion " + moneda_id + " to " + waers + " in date " + DateTime.Now.Date;
                    return 0;
                }
            }

            return ukurs;
        }

        public decimal getUkursUSD(string waers, string waersusd, out string errorString)
        {
            decimal ukurs = 0;
            errorString = string.Empty;
            using (TAT001Entities db = new TAT001Entities())
            {
                try
                {
                    var date = DateTime.Now.Date;
                    var tcambio = db.TCAMBIOs.Where(t => t.FCURR.Equals(waers) && t.TCURR.Equals(waersusd) && t.GDATU.Equals(date)).FirstOrDefault();
                    if (tcambio == null)
                    {
                        var max = db.TCAMBIOs.Where(t => t.FCURR.Equals(waers) && t.TCURR.Equals(waersusd)).Max(a => a.GDATU);
                        tcambio = db.TCAMBIOs.Where(t => t.FCURR.Equals(waers) && t.TCURR.Equals(waersusd) && t.GDATU.Equals(max)).FirstOrDefault();
                    }

                    ukurs = Convert.ToDecimal(tcambio.UKURS);
                }
                catch (Exception e)
                {
                    errorString = "detail: conversion " + waers + " to " + waersusd + " in date " + DateTime.Now.Date;
                    return 0;
                }

            }

            return ukurs;
        }

        public RANGO getRango(string TSOL_ID)
        {
            RANGO rango = new RANGO();
            using (TAT001Entities db = new TAT001Entities())
            {

                rango = (from r in db.RANGOes
                         join s in db.TSOLs
                         on r.ID equals s.RANGO_ID
                         where s.ID == TSOL_ID && r.ACTIVO == true
                         select r).FirstOrDefault();

            }

            return rango;

        }

        public decimal getSolID(string TSOL_ID)
        {

            decimal id = 0;

            RANGO rango = getRango(TSOL_ID);

            if (rango.ACTUAL > rango.INICIO && rango.ACTUAL < rango.FIN)
            {
                rango.ACTUAL++;
                id = (decimal)rango.ACTUAL;
            }

            return id;
        }

        public STATE getEstado(int id)
        {
            STATE state = new STATE();

            using (TAT001Entities db = new TAT001Entities())
            {

                state = (from c in db.CITIES
                         join s in db.STATES
                         on c.STATE_ID equals s.ID
                         where s.ID == id
                         select s).FirstOrDefault();

            }

            return state;
        }


        public CLIENTE getCliente(string PAYER_ID)
        {
            CLIENTE payer = new CLIENTE();

            using (TAT001Entities db = new TAT001Entities())
            {

                payer = db.CLIENTEs.Where(c => c.KUNNR.Equals(PAYER_ID)).FirstOrDefault();

            }
            return payer;

        }


        public void updateRango(string TSOL_ID, decimal actual)
        {

            RANGO rango = getRango(TSOL_ID);

            if (rango.ACTUAL > rango.INICIO && rango.ACTUAL < rango.FIN)
            {
                rango.ACTUAL = actual;
            }

            db.Entry(rango).State = EntityState.Modified;
            db.SaveChanges();

        }

        // GET: Solicitudes/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOCUMENTO dOCUMENTO = db.DOCUMENTOes.Find(id);
            if (dOCUMENTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.TALL_ID = new SelectList(db.TALLs, "ID", "DESCRIPCION", dOCUMENTO.TALL_ID);
            ViewBag.TSOL_ID = new SelectList(db.TSOLs, "ID", "DESCRIPCION", dOCUMENTO.TSOL_ID);
            ViewBag.USUARIOC_ID = new SelectList(db.USUARIOs, "ID", "PASS", dOCUMENTO.USUARIOC_ID);
            ViewBag.VKORG = new SelectList(db.CLIENTEs, "VKORG", "NAME1", dOCUMENTO.VKORG);
            ViewBag.PAIS_ID = new SelectList(db.PAIS, "LAND", "SPRAS", dOCUMENTO.PAIS_ID);
            ViewBag.SOCIEDAD_ID = new SelectList(db.SOCIEDADs, "BUKRS", "BUTXT", dOCUMENTO.SOCIEDAD_ID);
            return View(dOCUMENTO);
        }

        // POST: Solicitudes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NUM_DOC,TSOL_ID,TALL_ID,SOCIEDAD_ID,PAIS_ID,PERIODO,EJERCICIO,TIPO_TECNICO,TIPO_RECURRENTE,CANTIDAD_EV,USUARIOC_ID,FECHAD,FECHAC,ESTATUS,ESTATUS_C,ESTATUS_SAP,ESTATUS_WF,DOCUMENTO_REF,NOTAS,MONTO_DOC_MD,MONTO_FIJO_MD,MONTO_BASE_GS_PCT_MD,MONTO_BASE_NS_PCT_MD,MONTO_DOC_ML,MONTO_FIJO_ML,MONTO_BASE_GS_PCT_ML,MONTO_BASE_NS_PCT_ML,MONTO_DOC_ML2,MONTO_FIJO_ML2,MONTO_BASE_GS_PCT_ML2,MONTO_BASE_NS_PCT_ML2,IMPUESTO,FECHAI_VIG,FECHAF_VIG,ESTATUS_EXT,SOLD_TO_ID,PAYER_ID,GRUPO_CTE_ID,CANAL_ID,MONEDA_ID,TIPO_CAMBIO,NO_FACTURA,FECHAD_SOPORTE,METODO_PAGO,NO_PROVEEDOR,PASO_ACTUAL,AGENTE_ACTUAL,FECHA_PASO_ACTUAL,VKORG,VTWEG,SPART,HORAC,FECHAC_PLAN,FECHAC_USER,HORAC_USER,CONCEPTO,PORC_ADICIONAL,PAYER_NOMBRE,PAYER_EMAIL,MONEDAL_ID,MONEDAL2_ID,TIPO_CAMBIOL,TIPO_CAMBIOL2")] DOCUMENTO dOCUMENTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dOCUMENTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TALL_ID = new SelectList(db.TALLs, "ID", "DESCRIPCION", dOCUMENTO.TALL_ID);
            ViewBag.TSOL_ID = new SelectList(db.TSOLs, "ID", "DESCRIPCION", dOCUMENTO.TSOL_ID);
            ViewBag.USUARIOC_ID = new SelectList(db.USUARIOs, "ID", "PASS", dOCUMENTO.USUARIOC_ID);
            ViewBag.VKORG = new SelectList(db.CLIENTEs, "VKORG", "NAME1", dOCUMENTO.VKORG);
            ViewBag.PAIS_ID = new SelectList(db.PAIS, "LAND", "SPRAS", dOCUMENTO.PAIS_ID);
            ViewBag.SOCIEDAD_ID = new SelectList(db.SOCIEDADs, "BUKRS", "BUTXT", dOCUMENTO.SOCIEDAD_ID);
            return View(dOCUMENTO);
        }

        // GET: Solicitudes/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == 0)
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

        // POST: Solicitudes/Delete/5
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

        public ActionResult CreateNew()
        {

            using (TAT001Entities db = new TAT001Entities())
            {
                string u = User.Identity.Name;
                var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                ViewBag.permisos = db.PAGINAVs.Where(a => a.ID.Equals(user.ID)).ToList();
                ViewBag.carpetas = db.CARPETAVs.Where(a => a.USUARIO_ID.Equals(user.ID)).ToList();
                ViewBag.nombre = user.NOMBRE + " " + user.APELLIDO_P + " " + user.APELLIDO_M;
                ViewBag.email = user.EMAIL;
                ViewBag.rol = user.MIEMBROS.FirstOrDefault().ROL.NOMBRE;
                ViewBag.returnUrl = Request.UrlReferrer;
                ViewBag.usuario = user;
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

            return View();
        }

        public ActionResult Sol_Tipo()
        {

            var btnRadioe = Request.Form["radio_tiposol"];

            try
            {
                string p = Session["pais"].ToString();
                ViewBag.pais = p + ".svg";
            }
            catch
            {
                return RedirectToAction("Pais", "Home");
            }

            if (btnRadioe != "" || btnRadioe != null)
            {
                Session["sol_tipo"] = null;
                Session["sol_tipo"] = btnRadioe;

                //return RedirectToAction("Informacion", "Solicitud", new { tipo = btnRadioe });
                return RedirectToAction("Create", "Solicitudes");
            }
            else
            {
                return RedirectToAction("Index", "Solicitudes");
            }
        }

        public ActionResult Cancelar()
        {
            Session["sol_tipo"] = null;

            return RedirectToAction("Index", "Solicitudes");
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult SelectCity(int id)
        {

            TAT001Entities db = new TAT001Entities();

            var id_cl = db.CITIES.Where(city => city.STATE_ID.Equals(id)).Select(c => new { ID = c.ID.ToString(), NAME = c.NAME.ToString() }).ToList();

            JsonResult jc = Json(id_cl, JsonRequestBehavior.AllowGet);
            return jc;
        }


        [HttpPost]
        [AllowAnonymous]
        public JsonResult SelectTall(string id)
        {

            TAT001Entities db = new TAT001Entities();

            string u = User.Identity.Name;
            var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();

            var id_clas = db.TALLs.Where(t => t.ACTIVO == true && t.GALL_ID.Equals(id))
                                .Join(
                                db.TALLTs.Where(tallt => tallt.SPRAS_ID == user.SPRAS_ID),
                                tall => tall.ID,
                                tallt => tallt.TALL_ID,
                                (tall, tallt) => new
                                {
                                    ID = tallt.TALL_ID.ToString(),
                                    TEXT = tallt.TXT50.ToString()
                                })
                            .ToList();

            JsonResult jc = Json(id_clas, JsonRequestBehavior.AllowGet);
            return jc;
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult SelectCliente(string kunnr)
        {

            TAT001Entities db = new TAT001Entities();

            var id_cl = (from c in db.CLIENTEs
                         join co in db.CONTACTOCs
                         on new { c.VKORG, c.VTWEG, c.SPART, c.KUNNR } equals new { co.VKORG, co.VTWEG, co.SPART, co.KUNNR } into jjcont
                         from co in jjcont.DefaultIfEmpty()
                         where c.KUNNR == kunnr
                         select new
                         {
                             c.VKORG,
                             c.VTWEG,
                             c.NAME1,
                             c.KUNNR,
                             c.STCD1,
                             PAYER_NOMBRE = co == null ? String.Empty : co.NOMBRE,
                             PAYER_EMAIL = co == null ? String.Empty : co.EMAIL,
                         }).FirstOrDefault();

            JsonResult jc = Json(id_cl, JsonRequestBehavior.AllowGet);
            return jc;
        }

        [HttpPost]
        [AllowAnonymous]
        public string SelectTcambio(string fcurr)
        {
            string p = "";
            string errorString = "";
            decimal tcambio = 0;
            string tcurr = "USD";
            SOCIEDAD id_bukrs = new SOCIEDAD();
            try
            {
                p = Session["pais"].ToString();
            }
            catch
            {
            }

            TAT001Entities db = new TAT001Entities();
            try
            {
                id_bukrs = db.SOCIEDADs.Where(soc => soc.LAND.Equals(p)).FirstOrDefault();
                var date = DateTime.Now.Date;
                //var tc = db.TCAMBIOs.Where(t => t.FCURR.Equals(fcurr) && t.TCURR.Equals(tcurr) && t.GDATU.Equals(date)).FirstOrDefault().UKURS;
                var tc = db.TCAMBIOs.Where(t => t.FCURR.Equals(fcurr) && t.TCURR.Equals(tcurr) && t.GDATU.Equals(date)).FirstOrDefault();
                if (tc == null)
                {
                    var max = db.TCAMBIOs.Where(t => t.FCURR.Equals(fcurr) && t.TCURR.Equals(tcurr)).Max(a => a.GDATU);
                    tc = db.TCAMBIOs.Where(t => t.FCURR.Equals(fcurr) && t.TCURR.Equals(tcurr) && t.GDATU.Equals(max)).FirstOrDefault();
                }

                tcambio = Convert.ToDecimal(tc.UKURS);

            }
            catch (Exception e)
            {
                errorString = e.Message + "detail: conversion " + fcurr + " to " + tcurr + " in date " + DateTime.Now.Date;
                //throw new System.Exception(errorString);
                return errorString;
            }

            return Convert.ToString(tcambio);
        }

        [HttpPost]
        [AllowAnonymous]
        public string SelectVcambio(string moneda_id, decimal monto_doc_md)
        {
            string p = "";
            string tcurr = "USD";
            string errorString = "";
            decimal monto = 0;
            try
            {
                p = Session["pais"].ToString();
            }
            catch
            {
            }

            TAT001Entities db = new TAT001Entities();

            var id_bukrs = db.SOCIEDADs.Where(soc => soc.LAND.Equals(p)).FirstOrDefault();
            try
            {
                var date = DateTime.Now.Date;
                //var UKURS = db.TCAMBIOs.Where(t => t.FCURR.Equals(moneda_id) && t.TCURR.Equals(tcurr) && t.GDATU.Equals(date)).FirstOrDefault().UKURS;
                var tc = db.TCAMBIOs.Where(t => t.FCURR.Equals(moneda_id) && t.TCURR.Equals(tcurr) && t.GDATU.Equals(date)).FirstOrDefault();
                if (tc == null)
                {
                    var max = db.TCAMBIOs.Where(t => t.FCURR.Equals(moneda_id) && t.TCURR.Equals(tcurr)).Max(a => a.GDATU);
                    tc = db.TCAMBIOs.Where(t => t.FCURR.Equals(moneda_id) && t.TCURR.Equals(tcurr) && t.GDATU.Equals(max)).FirstOrDefault();
                }

                decimal uk = Convert.ToDecimal(tc.UKURS);

                if (tc.UKURS > 0)
                {
                    monto = Convert.ToDecimal(monto_doc_md) / uk;
                }
            }
            catch (Exception e)
            {
                errorString = e.Message + "detail: conversion " + moneda_id + " to " + tcurr + " in date " + DateTime.Now.Date;
                //throw new System.Exception(errorString);
                return errorString;
            }
            return Convert.ToString(monto);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult LoadExcel()
        {
            List<DOCUMENTOP_MOD> ld = new List<DOCUMENTOP_MOD>();


            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files["FileUpload"];
                //using (var stream2 = System.IO.File.Open(url, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                //{
                string extension = System.IO.Path.GetExtension(file.FileName);
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                //using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(file.InputStream))
                //{
                IExcelDataReader reader = ExcelReaderFactory.CreateReader(file.InputStream);
                // 2. Use the AsDataSet extension method
                DataSet result = reader.AsDataSet();

                // The result of each spreadsheet is in result.Tables
                // 3.DataSet - Create column names from first row
                DataTable dt = result.Tables[0];

                //Rows
                var rowsc = dt.Rows.Count;
                //columns
                var columnsc = dt.Columns.Count;

                //Columnd and row to start
                var rows = 4; // 5
                var cols = 1; // B
                var pos = 1;

                for (int i = rows; i < rowsc; i++)
                {
                    //for (var j = 0; j < columnsc; j++)
                    //{
                    //    var data = dt.Rows[i][j];
                    //}
                    if (i >= 4)
                    {
                        var v = dt.Rows[i][1];
                        if (Convert.ToString(v) == "")
                        {
                            break;
                        }
                    }
                    DOCUMENTOP_MOD doc = new DOCUMENTOP_MOD();
                    //Entities.DOCUMENTOP p = new DOCUMENTOP();

                    var poss = dt.Rows[i][1];
                    string a = Convert.ToString(pos);

                    doc.POS = Convert.ToDecimal(a);
                    doc.VIGENCIA_DE = Convert.ToDateTime(dt.Rows[i][1]); //DEL
                    doc.VIGENCIA_AL = Convert.ToDateTime(dt.Rows[i][2]); //AL
                    doc.MATNR = (string)dt.Rows[i][3]; //Material
                    doc.MATKL = (string)dt.Rows[i][4]; //Categoría
                    doc.DESC = (string)dt.Rows[i][5]; //Descripción
                    double monto = (double)dt.Rows[i][6]; //Costo unitario    
                    doc.MONTO = Convert.ToDecimal(monto);      
                    double porc_apoyo = (double)dt.Rows[i][7]; //% apoyo
                    doc.PORC_APOYO = Convert.ToDecimal(porc_apoyo);
                    double monto_apoyo = (double)dt.Rows[i][8]; //Apoyo por pieza
                    doc.MONTO_APOYO = Convert.ToDecimal(monto_apoyo);
                    double montoc_apoyo = (double)dt.Rows[i][9]; //Costo con apoyo
                    doc.MONTOC_APOYO = Convert.ToDecimal(montoc_apoyo);
                    double precio_sug = (double)dt.Rows[i][10]; //Precio sugerido
                    doc.PRECIO_SUG = Convert.ToDecimal(precio_sug);
                    double volumen_est = (double)dt.Rows[i][11]; //Volumen estimado
                    doc.VOLUMEN_EST = Convert.ToDecimal(volumen_est);
                    double porc_apoyoest = (double)dt.Rows[i][12]; //Estimado $ apoyo
                    doc.PORC_APOYOEST = Convert.ToDecimal(porc_apoyoest);
                    ld.Add(doc);
                    pos++;
                }

                reader.Close();

            }
            JsonResult jl = Json(ld, JsonRequestBehavior.AllowGet);
            return jl;
        }

        [HttpPost]
        [AllowAnonymous]
        public string saveFiles()
        {
            var res = "";
            string error = "";
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    string url = ConfigurationManager.AppSettings["URL_SAVE"];
                    HttpPostedFileBase file = Request.Files[i];
                    string filename = file.FileName;
                    res = SaveFile(file, url, "100" ,out error);
                }
                //HttpPostedFileBase file1 = Request.Files["f_carta"];
                //HttpPostedFileBase file2 = Request.Files["f_contratos"];
                //HttpPostedFileBase file3 = Request.Files["f_factura"];
                //HttpPostedFileBase file4 = Request.Files["f_jbp"];

                //string filename1 = System.IO.Path.GetExtension(file1.FileName);
                //string filename2 = System.IO.Path.GetExtension(file2.FileName);
                //string filename3 = System.IO.Path.GetExtension(file3.FileName);
                //string filename4 = System.IO.Path.GetExtension(file4.FileName);

                //string url = ConfigurationManager.AppSettings["URL_SAVE"];

                //res = SaveFile(file1, url);
                //res += " : "+SaveFile(file2, @"C:\Users\EQUIPO\Desktop\files");
                //res += " : " + SaveFile(file3, @"C:\Users\EQUIPO\Desktop\files");
                //res += " : " + SaveFile(file4, @"C:\Users\EQUIPO\Desktop\files");

            }

            return res;
        }

        public string createDir(string path, string documento) {

            string ex = "";

            // Specify the path to save the uploaded file to.
            string savePath = path + documento + "\\";

            // Create the path and file name to check for duplicates.
            string pathToCheck = savePath;

            try
                {
                if (!System.IO.File.Exists(pathToCheck))
                {
                    //No existe, se necesita crear
                    DirectoryInfo dir = new DirectoryInfo(pathToCheck);

                    dir.Create();
                            
                }
            //file.SaveAs(Server.MapPath(savePath)); //Guardarlo el cualquier parte dentro del proyecto <add key="URL_SAVE" value="\Archivos\" />
            //System.IO.File.Create(savePath,100,FileOptions.DeleteOnClose, )
            //System.IO.File.Copy(copyFrom, savePath);
            //f.CopyTo(savePath,true);
            }catch(Exception e)
            {
                ex = "No se puede crear el directorio para guardar los archivos";
            }

            return ex;
        }

        public string SaveFile(HttpPostedFileBase file, string path,string documento, out string exception)
        {
            string ex = "";
            string exdir = "";
            // Get the name of the file to upload.
            string fileName = file.FileName;//System.IO.Path.GetExtension(file.FileName);    // must be declared in the class above

            // Specify the path to save the uploaded file to.
            string savePath = path + documento + "\\";         

            // Create the path and file name to check for duplicates.
            string pathToCheck = savePath;

            //
            //FileInfo f = new FileInfo(fileName);
            //string fullname = f.FullName;

            //string pathl = Path.GetFullPath(f.DirectoryName.ToString());

            //Get full file path
            // string copyFrom = fullname;

            // Create a temporary file name to use for checking duplicates.
            //string tempfileName = "";

            // Check to see if a file already exists with the
            // same name as the file to upload.
            //if (System.IO.File.Exists(Server.MapPath(pathToCheck)))
            //if (!System.IO.File.Exists(savePath))
            //{
            //    //No existe se necesita crear
            ////    int counter = 2;
            ////    while (System.IO.File.Exists(Server.MapPath(pathToCheck)))
            ////    {
            ////        // if a file with this name already exists,
            ////        // prefix the filename with a number.
            ////        tempfileName = counter.ToString() + fileName;
            ////        pathToCheck = savePath + tempfileName;
            ////        counter++;
            ////    }

            ////    fileName = tempfileName;

            ////    // Notify the user that the file name was changed.
            //}

            


            // Append the name of the file to upload to the path.
            savePath += fileName;

            // Call the SaveAs method to save the uploaded
            // file to the specified directory.
            //file.SaveAs(Server.MapPath(savePath));

            //file to domain
            //Parte para guardar archivo
            using (Impersonation.LogonUser("SF-0003", "EQUIPO", "0906", LogonType.NewCredentials))
            {
                //fileName = file.SaveAs(file, Server.MapPath("~/Nueva carpeta/") + file.FileName);
                try
                {
              

                    //Guardar el archivo
                    file.SaveAs(savePath);

                }
                catch (Exception e)
                {
                    ex = "";
                    ex = fileName;
                }
            }
            exception = ex;            
            return fileName;
        }

        //static void CopyToSharedFolder()
        //{
        //    IntPtr admin_token = default(IntPtr);
        //    WindowsIdentity wid_current = WindowsIdentity.GetCurrent();
        //    WindowsIdentity wid_admin = null;
        //    WindowsImpersonationContext wic = null;

        //    try
        //    {
        //        //if (LogonUser("Local Admin name", "Local computer name", "pwd", 9, 0, ref admin_token) != 0)
        //        if (new WindowsImpersonationContext.LogonUser("EQUIPO", "SF-0003", "0906", 9, 0, ref admin_token) != 0)
        //        {
        //            wid_admin = new WindowsIdentity(admin_token);
        //            wic = wid_admin.Impersonate();
        //            //System.IO.File.Copy("C:\\right.bmp", "\\\\157.60.113.28\\testnew\\right.bmp", true);
        //            System.IO.File.Copy(@"D:\ram\Test.txt", @"\\10.245.66.50\pdc\Test.txt", true);
        //        }
        //    }
        //    catch (System.Exception se)
        //    {
        //        int ret = Marshal.GetLastWin32Error();
        //    }
        //    finally
        //    {
        //        if (wic != null)
        //        {
        //            wic.Undo();
        //        }
        //    }
        //}

        [HttpPost]
        [AllowAnonymous]
        public string selectMatCat(string catid)
        {
            
            return "dfdf";
        }
    }
}
