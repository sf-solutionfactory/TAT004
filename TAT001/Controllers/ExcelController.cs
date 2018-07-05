using ExcelDataReader;
using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using TAT001.Entities;
using TAT001.Models;
using TAT001.Services;
using TAT001.Controllers;
using ClosedXML.Excel;

namespace TAT001.Controllers
{
    [Authorize]
    public class ExcelController : Controller
    {
        private TAT001Entities db = new TAT001Entities();
        // GET: Excel
        public ActionResult Index(int? miSop, int? miMas)
        {
            int pagina = 221; //ID EN BASE DE DATOS
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
                ViewBag.pais = p + ".svg";
            }
            catch
            {
                //return RedirectToAction("Pais", "Home");
            }
            Session["spras"] = user.SPRAS_ID;

            if (miSop == 1 & miMas == 1)
            {
                Session["indSop"] = 0;
                Session["indMas"] = 0;
                return RedirectToAction("Index2", new { hola = "hola" });
            }
            else
            {
                if (miSop == 1)
                {
                    ViewBag.contenidoSop = 1;
                }
                else if (miSop == 0 | miSop == null)
                {
                    ViewBag.contenidoSop = 0;
                }

                if (miMas == 1)
                {
                    ViewBag.contenidoMas = 1;
                }
                else if (miMas == 0 | miMas == null)
                {
                    ViewBag.contenidoMas = 0;
                }

                return View();
            }
        }

        public ActionResult Index2(string hola)
        {
            int pagina = 221; //ID EN BASE DE DATOS
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
                ViewBag.pais = p + ".svg";
            }
            catch { }

            Session["spras"] = user.SPRAS_ID;

            HttpPostedFileBase file;
            if (hola != null)
            {
                file = (HttpPostedFileBase)Session["archivoMas"];
            }
            else
            {
                file = null;
            }


            if (file != null)
            {
                DataSet dsHoja1 = new DataSet();
                DataSet dsHoja2 = new DataSet();
                string excelConnectionString = string.Empty;
                //if (Request.Files["file"].ContentLength > 0)
                if (file.ContentLength > 0)
                {
                    //string fileExtension = System.IO.Path.GetExtension(Request.Files["file"].FileName);
                    string fileExtension = System.IO.Path.GetExtension(file.FileName);

                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        string extension = System.IO.Path.GetExtension(file.FileName);
                        IExcelDataReader reader = ExcelReaderFactory.CreateReader(file.InputStream);
                        DataSet result = reader.AsDataSet();

                        DataSet dt = new DataSet();
                        DataTable dtt = result.Tables[0].Copy();
                        dt.Tables.Add(dtt);

                        DataSet dt1 = new DataSet();
                        DataTable dtt1 = result.Tables[1].Copy();
                        dt1.Tables.Add(dtt1);

                        DataSet dt3 = new DataSet();
                        DataTable dtt3 = result.Tables[2].Copy();
                        dt3.Tables.Add(dtt3);

                        DataSet dt4 = new DataSet();
                        DataTable dtt4 = result.Tables[3].Copy();
                        dt4.Tables.Add(dtt4);

                        Session["ds1"] = dt;
                        Session["ds2"] = dt1;
                        Session["ds3"] = dt3;
                        Session["ds4"] = dt4;
                    }
                }
                return RedirectToAction("Details");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Details()
        {
            int pagina = 222; //ID EN BASE DE DATOS
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
                ViewBag.pais = p + ".svg";
            }
            catch { }

            Session["spras"] = user.SPRAS_ID;

            ViewBag.data1 = Session["ds1"];
            ViewBag.data2 = Session["ds2"];
            ViewBag.data3 = Session["ds3"];
            ViewBag.data4 = Session["ds4"];
            DataSet hoja1 = (DataSet)(Session["ds1"]);
            DataSet hoja2 = (DataSet)(Session["ds2"]);
            DataSet hoja3 = (DataSet)(Session["ds3"]);
            DataSet hoja4 = (DataSet)(Session["ds4"]);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(string NUM_DOC)
        {
            string u = User.Identity.Name;
            var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
            DataSet dsHoja1 = (DataSet)(Session["ds1"]);
            DataSet dsHoja2 = (DataSet)(Session["ds2"]);
            DataSet dsHoja3 = (DataSet)(Session["ds3"]);
            DataSet dsHoja4 = (DataSet)(Session["ds4"]);
            IEnumerable<HttpPostedFileBase> archivos = (IEnumerable<HttpPostedFileBase>)(Session["archivosSop"]);

            List<DOCUMENTO> listD = new List<DOCUMENTO>();
            List<DOCUMENTOF> listF = new List<DOCUMENTOF>();

            //LECTURA DEL CONENIDO DEL EXCEL PARA LA CARGA MASIVA

            //SECCION 1.- LOGICA PARA LA INSERCION DE LA PRIMER HOJA (Las hojas posteriores dependen de la validacion de est)
            for (int i = 1; i < dsHoja1.Tables[0].Rows.Count; i++)
            {
                DOCUMENTO docu = new DOCUMENTO();
                string numC = dsHoja1.Tables[0].Rows[i][0].ToString();
                string a = dsHoja1.Tables[0].Rows[i][1].ToString().Trim();
                if (a.Length > 10) { a = ""; }
                string b = dsHoja1.Tables[0].Rows[i][2].ToString().Trim();
                b = db.TALLs.Where(x => x.DESCRIPCION == b).Select(x => x.GALL_ID).FirstOrDefault();
                string bbb = dsHoja1.Tables[0].Rows[i][2].ToString().Trim();
                string c = dsHoja1.Tables[0].Rows[i][3].ToString().Trim();
                if (c.Length > 4) { c = ""; }
                string d = dsHoja1.Tables[0].Rows[i][4].ToString().Trim();
                d = db.PAIS.Where(x => x.LANDX == d).Select(x => x.LAND).FirstOrDefault();
                string est = dsHoja1.Tables[0].Rows[i][5].ToString().Trim();
                if (est.Length > 50) { est = ""; }
                string ciu = dsHoja1.Tables[0].Rows[i][6].ToString().Trim();
                if (ciu.Length > 50) { ciu = ""; }
                var e = db.TSOLs.Where(x => x.ID == a).Select(x => x.ID);
                var f = db.GALLs.Where(x => x.ID == b).Select(x => x.ID);
                var g = db.SOCIEDADs.Where(x => x.BUKRS == c).Select(x => x.BUKRS);
                var h = db.PAIS.Where(x => x.LAND == d).Select(x => x.LAND);
                string con = dsHoja1.Tables[0].Rows[i][7].ToString().Trim();
                if (con.Length > 100) { con = ""; }
                string not = dsHoja1.Tables[0].Rows[i][8].ToString().Trim();
                if (not.Length > 255) { not = ""; }
                string dd = dsHoja1.Tables[0].Rows[i][9].ToString().Trim();
                if (dd.Length > 10) { dd = ""; }
                string pan = dsHoja1.Tables[0].Rows[i][10].ToString().Trim();
                if (pan.Length > 50) { pan = ""; }
                string pae = dsHoja1.Tables[0].Rows[i][11].ToString().Trim();
                if (pae.Length > 255) { pae = ""; }

                string fec1 = dsHoja1.Tables[0].Rows[i][12].ToString().Trim();
                string fec2 = dsHoja1.Tables[0].Rows[i][13].ToString().Trim();
                string mone = dsHoja1.Tables[0].Rows[i][14].ToString().Trim();
                if (mone.Length > 3) { mone = ""; }
                var ee = db.CLIENTEs.Where(x => x.KUNNR == dd);
                var monee = db.MONEDAs.Where(x => x.WAERS == mone).Select(x => x.WAERS);

                var per = from P in db.PAIS
                          join C in db.CREADOR2 on P.LAND equals C.LAND
                          where P.ACTIVO == true
                          & C.ID == u & C.ACTIVO == true
                          select P;

                var perm = per.Where(x => x.SOCIEDAD_ID == c & x.LAND == d).FirstOrDefault();

                if (e.Count() > 0 && f.Count() > 0 && g.Count() > 0 && h.Count() > 0 & ee.Count() > 0 & monee.Count() > 0)
                {
                    if (perm != null)
                    {
                        if (IsNumeric(numC))
                        {
                            fec1 = validaFechI(fec1);
                            fec2 = validaFechF(fec2);

                            docu.NUM_DOC = Convert.ToDecimal(numC);
                            docu.TSOL_ID = a;
                            docu.GALL_ID = b;
                            docu.SOCIEDAD_ID = c;
                            docu.PAIS_ID = d;
                            docu.ESTADO = est;
                            docu.CIUDAD = ciu;
                            docu.PERIODO = Convert.ToInt32(System.DateTime.Now.Month);
                            docu.EJERCICIO = Convert.ToString(System.DateTime.Now.Year);
                            docu.CANTIDAD_EV = 1;
                            docu.USUARIOC_ID = user.ID;
                            docu.PUESTO_ID = user.PUESTO_ID;
                            docu.FECHAD = System.DateTime.Today;
                            docu.FECHAC = System.DateTime.Today;
                            docu.HORAC = System.DateTime.Now.TimeOfDay;
                            docu.FECHAC_PLAN = System.DateTime.Today;
                            docu.FECHAC_USER = System.DateTime.Today;
                            docu.HORAC_USER = System.DateTime.Now.TimeOfDay;
                            docu.ESTATUS = "N";
                            docu.ESTATUS_WF = "P";
                            docu.CONCEPTO = con;
                            docu.NOTAS = not;
                            docu.VKORG = db.CLIENTEs.Where(x => x.KUNNR == dd).Select(x => x.VKORG).First();
                            docu.VTWEG = db.CLIENTEs.Where(x => x.KUNNR == dd).Select(x => x.VTWEG).First();
                            docu.SPART = db.CLIENTEs.Where(x => x.KUNNR == dd).Select(x => x.SPART).First();
                            docu.PAYER_ID = dd;
                            docu.PAYER_NOMBRE = pan;
                            docu.PAYER_EMAIL = pae;
                            docu.FECHAI_VIG = Convert.ToDateTime(fec1);
                            docu.FECHAF_VIG = Convert.ToDateTime(fec2);
                            docu.MONEDA_ID = mone;
                            docu.MONTO_DOC_MD = 0;
                            docu.TALL_ID = db.TALLs.Where(x => x.DESCRIPCION == bbb).FirstOrDefault().ID;
                            listD.Add(docu);
                        }
                    }
                    DOCUMENTO dop = listD.Where(x => x.NUM_DOC == docu.NUM_DOC).FirstOrDefault();
                    //FIN DE LA SECCION 1

                    //SI LOS DATOS DE CABECERA FUERON CORRECTOS PASA LA CONDICION
                    if (dop != null)
                    {
                        int ind1 = 0;

                        //SECCION 2.- VALIDACION DE DATOS DE LA SEGUNDA HOJA DEL EXCEL (DISTRIBUCION)
                        for (int k = 1; k < dsHoja2.Tables[0].Rows.Count; k++)
                        {
                            string numM = dsHoja2.Tables[0].Rows[k][0].ToString().Trim();
                            if (IsNumeric(numM))
                            {
                                decimal num = Convert.ToDecimal(numM);
                                if (num == docu.NUM_DOC)
                                {
                                    DOCUMENTOP docup = new DOCUMENTOP();
                                    int j = k;

                                    string feD = dsHoja2.Tables[0].Rows[k][1].ToString().Trim();
                                    string feA = dsHoja2.Tables[0].Rows[k][2].ToString().Trim();
                                    string mat = dsHoja2.Tables[0].Rows[k][3].ToString().Trim();
                                    if (mat.Length > 18) { mat = ""; }
                                    string mat2 = dsHoja2.Tables[0].Rows[k][4].ToString().Trim();
                                    if (mat2.Length > 9) { mat2 = ""; }
                                    string mon = dsHoja2.Tables[0].Rows[k][5].ToString().Trim();
                                    string porA = dsHoja2.Tables[0].Rows[k][6].ToString().Trim();
                                    string preS = dsHoja2.Tables[0].Rows[k][7].ToString().Trim();
                                    string volR = dsHoja2.Tables[0].Rows[k][8].ToString().Trim();
                                    string apo = dsHoja2.Tables[0].Rows[k][9].ToString().Trim();

                                    if (docu.DOCUMENTOPs.Count == 0)
                                    {
                                        if ((mat != null | mat != "") & (mat2 != null | mat2 != ""))
                                        {
                                            ind1 = 1;
                                        }
                                        else if ((mat != null | mat != "") & (mat2 == null | mat2 == ""))
                                        {
                                            ind1 = 1;
                                        }
                                        else if ((mat == null | mat == "") & (mat2 != null | mat2 != ""))
                                        {
                                            ind1 = 0;
                                        }
                                    }

                                    if (ind1 == 1)
                                    {
                                        DateTime fechD = Convert.ToDateTime(feD);
                                        DateTime fechA = Convert.ToDateTime(feA);
                                        var mate = db.MATERIALs.Where(x => x.ID == mat);

                                        if (mate.Count() > 0 && fechA > fechD)
                                        {
                                            //AQUI VALIDAMOS SI TIENE REAL 
                                            //DE NO TENERLO HACE EL CALCULO CON COSTO, PORCENTAJE, PRECIO Y VOLUMEN
                                            if (apo != "")
                                            {
                                                if (IsNumeric(apo) == true)
                                                {
                                                    docup.NUM_DOC = num;
                                                    docup.POS = Convert.ToInt32(dop.DOCUMENTOPs.Count() + 1);
                                                    docup.VIGENCIA_DE = fechD;
                                                    docup.VIGENCIA_AL = fechA;
                                                    docup.MATNR = mat;
                                                    docup.MATKL = "";

                                                    if (db.TSOLs.Where(ar => ar.ID == dop.TSOL_ID).FirstOrDefault().FACTURA == true)
                                                    {
                                                        docup.APOYO_REAL = Convert.ToDecimal(apo);
                                                        dop.MONTO_DOC_MD += docup.APOYO_REAL;
                                                        docup.VOLUMEN_REAL = 0;
                                                    }
                                                    else
                                                    {
                                                        docup.APOYO_EST = Convert.ToDecimal(apo);
                                                        dop.MONTO_DOC_MD += docup.APOYO_EST;
                                                        docup.VOLUMEN_EST = 0;
                                                    }

                                                    docup.CANTIDAD = 0;
                                                    dop.DOCUMENTOPs.Add(docup);
                                                }
                                            }
                                            else
                                            {
                                                if (IsNumeric(mon) & IsNumeric(porA) & IsNumeric(volR))
                                                {
                                                    docup.NUM_DOC = num;
                                                    docup.POS = Convert.ToInt32(dop.DOCUMENTOPs.Count() + 1);
                                                    docup.VIGENCIA_DE = fechD;
                                                    docup.VIGENCIA_AL = fechA;
                                                    docup.MATNR = mat;
                                                    docup.MATKL = "";
                                                    docup.MONTO = Convert.ToDecimal(mon);
                                                    docup.PORC_APOYO = Convert.ToDecimal(porA);
                                                    docup.PRECIO_SUG = Convert.ToDecimal(preS);
                                                    docup.VOLUMEN_REAL = Convert.ToDecimal(volR);

                                                    if (docup.VOLUMEN_REAL == null)
                                                        docup.VOLUMEN_REAL = 0;
                                                    decimal hola = (Convert.ToDecimal(mon) * Convert.ToDecimal(porA));
                                                    hola = (Convert.ToDecimal(mon) - hola) * (Convert.ToDecimal(volR));

                                                    if (db.TSOLs.Where(ar => ar.ID == dop.TSOL_ID).FirstOrDefault().FACTURA == true)
                                                    {
                                                        docup.APOYO_REAL = hola;
                                                        dop.MONTO_DOC_MD += docup.APOYO_REAL;
                                                    }
                                                    else
                                                    {
                                                        docup.APOYO_EST = hola;
                                                        docup.VOLUMEN_EST = (decimal)docup.VOLUMEN_REAL;
                                                        docup.VOLUMEN_REAL = 0;
                                                        dop.MONTO_DOC_MD += docup.APOYO_EST;
                                                    }
                                                    docup.CANTIDAD = 0;
                                                    dop.DOCUMENTOPs.Add(docup);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        DateTime fechD = Convert.ToDateTime(feD);
                                        DateTime fechA = Convert.ToDateTime(feA);
                                        var mate = db.MATERIALs.Where(x => x.ID == mat);

                                        if (mate.Count() > 0 && fechA > fechD)
                                        {
                                            //AQUI VALIDAMOS SI TIENE REAL 
                                            //DE NO TENERLO HACE EL CALCULO CON COSTO, PORCENTAJE, PRECIO Y VOLUMEN
                                            if (apo != "")
                                            {
                                                if (IsNumeric(apo) == true)
                                                {
                                                    docup.NUM_DOC = num;
                                                    docup.POS = Convert.ToInt32(dop.DOCUMENTOPs.Count() + 1);
                                                    docup.VIGENCIA_DE = fechD;
                                                    docup.VIGENCIA_AL = fechA;
                                                    docup.MATNR = "";
                                                    string desMat = mat2;
                                                    desMat = db.MATERIALGPs.Where(x => x.DESCRIPCION == desMat).Select(x => x.ID).FirstOrDefault();
                                                    docup.MATKL = desMat;

                                                    if (db.TSOLs.Where(ar => ar.ID == dop.TSOL_ID).FirstOrDefault().FACTURA == true)
                                                    {
                                                        docup.APOYO_REAL = Convert.ToDecimal(apo);
                                                        dop.MONTO_DOC_MD += docup.APOYO_REAL;
                                                        docup.VOLUMEN_REAL = 0;
                                                    }
                                                    else
                                                    {
                                                        docup.APOYO_EST = Convert.ToDecimal(apo);
                                                        dop.MONTO_DOC_MD += docup.APOYO_EST;
                                                        docup.VOLUMEN_REAL = 0;
                                                    }

                                                    docup.CANTIDAD = 0;
                                                    dop.DOCUMENTOPs.Add(docup);
                                                }
                                            }
                                            else
                                            {
                                                if (IsNumeric(mon) & IsNumeric(porA) & IsNumeric(volR))
                                                {
                                                    docup.NUM_DOC = num;
                                                    docup.POS = Convert.ToInt32(dop.DOCUMENTOPs.Count() + 1);
                                                    docup.VIGENCIA_DE = fechD;
                                                    docup.VIGENCIA_AL = fechA;
                                                    docup.MATNR = "";
                                                    string desMat = mat2;
                                                    desMat = db.MATERIALGPs.Where(x => x.DESCRIPCION == desMat).Select(x => x.ID).FirstOrDefault();
                                                    docup.MATKL = desMat;
                                                    docup.MONTO = Convert.ToDecimal(mon);
                                                    docup.PORC_APOYO = Convert.ToDecimal(porA);
                                                    docup.PRECIO_SUG = Convert.ToDecimal(preS);
                                                    docup.VOLUMEN_REAL = Convert.ToDecimal(volR);
                                                    decimal hola = (Convert.ToDecimal(mon) * Convert.ToDecimal(porA));
                                                    hola = (Convert.ToDecimal(mon) - hola) * (Convert.ToDecimal(volR));


                                                    if (db.TSOLs.Where(ar => ar.ID == dop.TSOL_ID).FirstOrDefault().FACTURA == true)
                                                    {
                                                        docup.APOYO_REAL = hola;
                                                        dop.MONTO_DOC_MD += docup.APOYO_REAL;
                                                    }
                                                    else
                                                    {
                                                        docup.APOYO_EST = hola;
                                                        docup.VOLUMEN_EST = (decimal)docup.VOLUMEN_REAL;
                                                        docup.VOLUMEN_REAL = null;
                                                        dop.MONTO_DOC_MD += docup.APOYO_EST;
                                                    }
                                                    docup.CANTIDAD = 0;
                                                    dop.DOCUMENTOPs.Add(docup);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //FIN DE LA SECCION 2


                        //SECCION 3.- VALIDACION DE DATOS DE LA TERCER HOJA DEL EXCEL (RELACIONADA)
                        for (int m = 1; m < dsHoja3.Tables[0].Rows.Count; m++)
                        {
                            string numR = dsHoja3.Tables[0].Rows[m][0].ToString().Trim();
                            if (IsNumeric(numR))
                            {
                                string ind = "";
                                decimal num = Convert.ToDecimal(numR);
                                if (num == docu.NUM_DOC)
                                {
                                    DOCUMENTOF docupF = new DOCUMENTOF();
                                    int j = m;

                                    var exist = db.FACTURASCONFs.Where(x => x.SOCIEDAD_ID == c & x.PAIS_ID == d & x.TSOL == a).FirstOrDefault();
                                    if (exist != null)
                                    {
                                        docupF.NUM_DOC = num;
                                        docupF.POS = m;

                                        if (exist.FACTURA == true)
                                        {
                                            string fac = dsHoja3.Tables[0].Rows[m][1].ToString().Trim();
                                            if (fac == null | fac == "")
                                            {
                                                ind = "no";
                                            }
                                            else
                                            {
                                                if (fac.Length > 50) { docupF.FACTURA = "0"; }
                                                else { docupF.FACTURA = fac; }
                                            }
                                        }
                                        else { docupF.FACTURA = "0"; }

                                        if (exist.FECHA == true)
                                        {
                                            string fech = dsHoja3.Tables[0].Rows[m][2].ToString().Trim();
                                            if (fech == null | fech == "")
                                            {
                                                ind = "no";
                                            }
                                            else
                                            {
                                                docupF.FECHA = Convert.ToDateTime(fech);
                                            }
                                        }
                                        else { docupF.FECHA = null; }

                                        if (exist.PROVEEDOR == true)
                                        {
                                            string prov = dsHoja3.Tables[0].Rows[m][3].ToString().Trim();
                                            if (prov == null | prov == "")
                                            {
                                                ind = "no";
                                            }
                                            else
                                            {
                                                if (prov.Length > 10) { docupF.PROVEEDOR = "0"; }
                                                else { docupF.PROVEEDOR = prov; }
                                            }
                                        }
                                        else { docupF.PROVEEDOR = "0"; }

                                        if (exist.CONTROL == true)
                                        {
                                            string cont = dsHoja3.Tables[0].Rows[m][4].ToString().Trim();
                                            if (cont == null | cont == "")
                                            {
                                                ind = "no";
                                            }
                                            else
                                            {
                                                if (cont.Length > 50) { docupF.CONTROL = "0"; }
                                                else { docupF.CONTROL = cont; }
                                            }
                                        }
                                        else { docupF.CONTROL = "0"; }

                                        if (exist.AUTORIZACION == true)
                                        {
                                            string aut = dsHoja3.Tables[0].Rows[m][5].ToString().Trim();
                                            if (aut == null | aut == "")
                                            {
                                                ind = "no";
                                            }
                                            else
                                            {
                                                if (aut.Length > 50) { docupF.AUTORIZACION = "0"; }
                                                else { docupF.AUTORIZACION = aut; }
                                            }
                                        }
                                        else { docupF.AUTORIZACION = "0"; }

                                        if (exist.VENCIMIENTO == true)
                                        {
                                            string venc = dsHoja3.Tables[0].Rows[m][6].ToString().Trim();
                                            if (venc == null | venc == "")
                                            {
                                                ind = "no";
                                            }
                                            else
                                            {
                                                docupF.VENCIMIENTO = Convert.ToDateTime(venc);
                                            }
                                        }
                                        else { docupF.VENCIMIENTO = null; }

                                        if (exist.FACTURAK == true)
                                        {
                                            string facK = dsHoja3.Tables[0].Rows[m][7].ToString().Trim();
                                            if (facK == null | facK == "")
                                            {
                                                ind = "no";
                                            }
                                            else
                                            {
                                                if (facK.Length > 50) { docupF.FACTURAK = "0"; }
                                                else { docupF.FACTURAK = facK; }
                                            }
                                        }
                                        else { docupF.FACTURAK = "0"; }

                                        if (exist.EJERCICIOK == true)
                                        {
                                            string ejeK = dsHoja3.Tables[0].Rows[m][8].ToString().Trim();
                                            if (ejeK == null | ejeK == "")
                                            {
                                                ind = "no";
                                            }
                                            else
                                            {
                                                if (ejeK.Length > 4) { docupF.EJERCICIOK = "0"; }
                                                else { docupF.EJERCICIOK = ejeK; }
                                            }
                                        }
                                        else { docupF.EJERCICIOK = "0"; }

                                        if (exist.BILL_DOC == true)
                                        {
                                            string bilD = dsHoja3.Tables[0].Rows[m][9].ToString().Trim();
                                            if (bilD == null | bilD == "")
                                            {
                                                ind = "no";
                                            }
                                            else
                                            {
                                                if (bilD.Length > 10) { docupF.BILL_DOC = "0"; }
                                                else { docupF.BILL_DOC = bilD; }
                                            }
                                        }
                                        else { docupF.BILL_DOC = "0"; }

                                        if (exist.BELNR == true)
                                        {
                                            string beln = dsHoja3.Tables[0].Rows[m][10].ToString().Trim();
                                            if (beln == null | beln == "")
                                            {
                                                ind = "no";
                                            }
                                            else
                                            {
                                                if (beln.Length > 10) { docupF.BELNR = "0"; }
                                                else { docupF.BELNR = beln; }
                                            }
                                        }
                                        else { docupF.BELNR = "0"; }

                                        if (ind != "no")
                                        {
                                            dop.DOCUMENTOFs.Add(docupF);
                                        }
                                    }
                                }
                            }
                        }
                        //FIN DE LA SECCION TRES

                        //VALIDAMOS LOS ARCHIVOS RECIBIDOS DE SOPORTE
                        if (archivos.Count() > 0)
                        {
                            //SECCION 4.- VALIDACION DE DATOS DE LA CUARTA HOJA DEL EXCEL (DISTRIBUCION)
                            dop.DOCUMENTOAs = subeArchivo(archivos, u, a, dsHoja4, docu.NUM_DOC);
                        }
                        //FIN DE LA SECCION 4
                    }
                }
            }


            List<string> li = new List<string>();
            foreach (DOCUMENTO doc in listD)
            {
                TAT001Entities db1 = new TAT001Entities();
                decimal N_DOC = getSolID(doc.TSOL_ID);
                doc.NUM_DOC = N_DOC;
                db1.DOCUMENTOes.Add(doc);

                try
                {
                    db1.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }

                updateRango(doc.TSOL_ID, doc.NUM_DOC);
                guardaArchivos(N_DOC);//ALMACENAMOS LOS ARCHIVOS DE SOPORTE
                li.Add(doc.NUM_DOC.ToString());
                ProcesaFlujo2 pf = new ProcesaFlujo2();

                try
                {
                    WORKFV wf = db.WORKFHs.Where(a => a.TSOL_ID.Equals(doc.TSOL_ID)).FirstOrDefault().WORKFVs.OrderByDescending(a => a.VERSION).FirstOrDefault();
                    if (wf != null)
                    {
                        WORKFP wp = wf.WORKFPs.OrderBy(a => a.POS).FirstOrDefault();
                        FLUJO f = new FLUJO();
                        f.WORKF_ID = wf.ID;
                        f.WF_VERSION = wf.VERSION;
                        f.WF_POS = wp.POS;
                        f.NUM_DOC = doc.NUM_DOC;
                        f.POS = 1;
                        f.LOOP = 1;
                        f.USUARIOA_ID = doc.USUARIOC_ID;
                        f.ESTATUS = "I";
                        f.FECHAC = DateTime.Now;
                        f.FECHAM = DateTime.Now;
                        string c = pf.procesa(f, "");
                        if (c == "1")
                        {
                            Email em = new Email();
                            string UrlDirectory = Request.Url.GetLeftPart(UriPartial.Path);
                            em.enviaMailC(f.NUM_DOC, true, Session["spras"].ToString(), UrlDirectory);
                        }
                    }
                }
                catch (Exception ee) { }
            }
            TempData["docs_masiva"] = li;
            return RedirectToAction("Index", "Home");
        }


        public string validaFechI(string date)
        {
            Calendario445 cale = new Calendario445();
            DateTime fecha;
            int anio, mes;

            if (date.Length == 7)
            {

                mes = Convert.ToInt32(date.Substring(0, 2));
                anio = Convert.ToInt32(date.Substring(3, 4));

                fecha = cale.getPrimerDia(anio, mes);
            }
            else
            {
                fecha = Convert.ToDateTime(date);
            }

            return Convert.ToString(fecha);
        }

        public string validaFechF(string date)
        {
            Calendario445 cale = new Calendario445();
            DateTime fecha;
            int anio, mes;

            if (date.Length == 7)
            {
                mes = Convert.ToInt32(date.Substring(0, 2));
                anio = Convert.ToInt32(date.Substring(3, 4));

                fecha = cale.getUltimoDia(anio, mes);
            }
            else
            {
                fecha = Convert.ToDateTime(date);
            }

            return Convert.ToString(fecha);
        }

        public ActionResult Archivo()//METODO PARA DESCARGAR EL ARCHIVO DEL SERVER
        {
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AddHeader("Content-Disposition", "attachment; filename=LAYOUT CARGA MASIVA.xlsx");
            Response.TransmitFile(Server.MapPath("~/files/masiva.xlsx"));
            Response.End();

            return RedirectToAction("Index");
        }

        public ActionResult ArchivoA(HttpPostedFileBase file)//METODO PARA DESCARGAR EL ARCHIVO DEL SERVER
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    string fileExtension = System.IO.Path.GetExtension(file.FileName);
                    string fileName = System.IO.Path.GetFileName(file.FileName.Substring(0, 6));

                    if ((fileExtension == ".xls" || fileExtension == ".xlsx") & fileName == "masiva")
                    {
                        var fileName2 = Path.GetFileName(file.FileName);
                        file.SaveAs(Server.MapPath("~/files/" + file.FileName));

                        string ruta = Server.MapPath("~/files/masiva.xlsx");
                        System.IO.File.Delete(ruta);
                    }
                }
            }
            return RedirectToAction("Index");
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

        public void updateRango(string TSOL_ID, decimal actual)
        {
            TAT001Entities db2 = new TAT001Entities();
            RANGO rango = getRango(TSOL_ID);

            if (rango.ACTUAL > rango.INICIO && rango.ACTUAL < rango.FIN)
            {
                rango.ACTUAL = actual;
            }

            db2.Entry(rango).State = EntityState.Modified;
            db2.SaveChanges();
            db2.Dispose();
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
        [HttpGet]
        public JsonResult Doc(string NUM_DOC)
        {
            List<DOCUMENTOP> ldp = new List<DOCUMENTOP>();
            DataSet tab = (DataSet)(Session["ds2"]);
            int ind1 = 0;

            for (var i = 1; i < tab.Tables[0].Rows.Count; i++)
            {
                if (tab.Tables[0].Rows[i][0].ToString() == NUM_DOC)
                {
                    DOCUMENTOP docup = new DOCUMENTOP();
                    int j = i;
                    docup.NUM_DOC = Convert.ToInt32(tab.Tables[0].Rows[i][0].ToString());
                    docup.POS = 0;
                    string mat = tab.Tables[0].Rows[i][3].ToString();
                    string mat2 = tab.Tables[0].Rows[i][4].ToString();

                    if ((mat != null | mat != "") & (mat2 != null | mat2 != ""))
                    {
                        ind1 = 1;
                    }
                    else if ((mat != null | mat != "") & (mat2 == null | mat2 == ""))
                    {
                        ind1 = 1;
                    }
                    else if ((mat == null | mat == "") & (mat2 != null | mat2 != ""))
                    {
                        ind1 = 0;
                    }

                    if (ind1 == 1)
                    {
                        if (IsNumeric(mat))
                        {
                            var e = db.MATERIALs.Where(x => x.ID == mat).Select(x => x.ID);
                            if (e.Count() > 0)
                            {
                                docup.MATNR = "<td>" + mat + "</td>";
                                docup.MATKL = "<td></td>";
                            }
                            else
                            {
                                docup.MATNR = "<td class='red white-text'>" + mat + "</td>";
                                docup.MATKL = "<td></td>";
                            }
                        }
                    }
                    else
                    {
                        var a = db.MATERIALs.Where(x => x.MATKL_ID == mat2).Select(x => x.MATKL_ID);
                        if (a.Count() > 0)
                        {
                            docup.MATNR = "<td></td>";
                            docup.MATKL = "<td>" + mat2 + "</td>";
                        }
                        else
                        {
                            docup.MATNR = "<td></td>";
                            docup.MATKL = "<td class='red white-text'>" + mat2 + "</td>";
                        }
                    }

                    //if (mat == "")
                    //{ docup.MATNR = "<td class='red white-text'>" + mat + "</td>"; }
                    //else
                    //{
                    //    if (IsNumeric(mat))
                    //    {
                    //        var e = db.MATERIALs.Where(x => x.ID == mat).Select(x => x.ID);
                    //        if (e.Count() > 0)
                    //        {
                    //            docup.MATNR = "<td>" + mat + "</td>";
                    //        }
                    //        else
                    //        {
                    //            docup.MATNR = "<td class='red white-text'>" + mat + "</td>";
                    //            ldp2.Add(i);
                    //        }
                    //    }
                    //}


                    //if (mat2 == "")
                    //{ docup.MATKL = "<td class='red white-text'>" + mat2 + "</td>"; }
                    //else
                    //{
                    //    var a = db.MATERIALs.Where(x => x.MATKL_ID == mat2).Select(x => x.MATKL_ID);
                    //    if (a.Count() > 0)
                    //    {
                    //        docup.MATKL = "<td>" + mat2 + "</td>";
                    //    }
                    //    else
                    //    {
                    //        docup.MATKL = "<td class='red white-text'>" + mat2 + "</td>";
                    //    }
                    //}

                    string mon = tab.Tables[0].Rows[i][5].ToString();
                    if (mon == "") { docup.MONTO = 0; }
                    else { docup.MONTO = Convert.ToDecimal(mon); }

                    string por = tab.Tables[0].Rows[i][6].ToString();
                    if (por == "") { docup.PORC_APOYO = 0; }
                    else { docup.PORC_APOYO = Convert.ToDecimal(por); }

                    string apo = tab.Tables[0].Rows[i][9].ToString();
                    if (apo == "") { docup.APOYO_EST = 0; }
                    else { docup.APOYO_EST = Convert.ToDecimal(apo); }

                    string pre = tab.Tables[0].Rows[i][7].ToString();
                    if (pre == "") { docup.PRECIO_SUG = 0; }
                    else { docup.PRECIO_SUG = Convert.ToDecimal(pre); }

                    string vol = tab.Tables[0].Rows[i][8].ToString();
                    if (vol == "") { docup.VOLUMEN_EST = 0; }
                    else { docup.VOLUMEN_EST = Convert.ToDecimal(vol); }

                    docup.VIGENCIA_DE = Convert.ToDateTime(tab.Tables[0].Rows[i][1]);
                    docup.VIGENCIA_AL = Convert.ToDateTime(tab.Tables[0].Rows[i][2]);
                    ldp.Add(docup);
                }
            }

            JsonResult jr = Json(ldp, JsonRequestBehavior.AllowGet);
            return jr;
        }

        [HttpGet]
        public JsonResult Hoja3(string NUM_DOC, string a, string c, string d)//a = TIPO SOLICITUD, c = SOCIEDAD, d = PAIS
        {
            List<object> ldf = new List<object>();
            List<string> ldf2 = new List<string>();
            List<string> ldf3 = new List<string>();
            List<object> ldf4 = new List<object>();
            //List<DOCUMENTOF> ldf = new List<DOCUMENTOF>();
            DataSet tab = (DataSet)(Session["ds3"]);
            d = db.PAIS.Where(x => x.LANDX == d).Select(x => x.LAND).FirstOrDefault();
            string cabeza = "";

            for (int m = 1; m < tab.Tables[0].Rows.Count; m++)
            {
                string ind = "";
                string numDo = tab.Tables[0].Rows[m][0].ToString();
                if (numDo == NUM_DOC)
                {
                    DOCUMENTOF docupF = new DOCUMENTOF();

                    var exist = db.FACTURASCONFs.Where(x => x.SOCIEDAD_ID == c & x.PAIS_ID == d & x.TSOL == a).FirstOrDefault();
                    if (exist != null)
                    {
                        docupF.NUM_DOC = Convert.ToInt32(tab.Tables[0].Rows[m][0].ToString());
                        docupF.POS = 0;

                        if (exist.FACTURA == true)
                        {
                            if (tab.Tables[0].Rows[m][1].ToString() == null | tab.Tables[0].Rows[m][1].ToString() == "")
                            {
                                ind = "no";
                                ldf3.Add("1");
                            }
                            else
                            {
                                docupF.FACTURA = tab.Tables[0].Rows[m][1].ToString();
                            }
                        }
                        else { docupF.FACTURA = "0"; }

                        if (exist.FECHA == true)
                        {
                            if (tab.Tables[0].Rows[m][2].ToString() == null | tab.Tables[0].Rows[m][2].ToString() == "")
                            {
                                ind = "no";
                                ldf3.Add("2");
                            }
                            else
                            {
                                docupF.FECHA = Convert.ToDateTime(tab.Tables[0].Rows[m][2]);
                            }
                        }
                        else { docupF.FECHA = null; }

                        if (exist.PROVEEDOR == true)
                        {
                            if (tab.Tables[0].Rows[m][3].ToString() == null | tab.Tables[0].Rows[m][3].ToString() == "")
                            {
                                ind = "no";
                                ldf3.Add("3");
                            }
                            else
                            {
                                docupF.PROVEEDOR = tab.Tables[0].Rows[m][3].ToString();
                            }
                        }
                        else { docupF.PROVEEDOR = "0"; }

                        if (exist.CONTROL == true)
                        {
                            if (tab.Tables[0].Rows[m][4].ToString() == null | tab.Tables[0].Rows[m][4].ToString() == "")
                            {
                                ind = "no";
                                ldf3.Add("4");
                            }
                            else
                            {
                                docupF.CONTROL = tab.Tables[0].Rows[m][4].ToString();
                            }
                        }
                        else { docupF.CONTROL = "0"; }

                        if (exist.AUTORIZACION == true)
                        {
                            if (tab.Tables[0].Rows[m][5].ToString() == null | tab.Tables[0].Rows[m][5].ToString() == "")
                            {
                                ind = "no";
                                ldf3.Add("5");
                            }
                            else
                            {
                                docupF.AUTORIZACION = tab.Tables[0].Rows[m][5].ToString();
                            }
                        }
                        else { docupF.AUTORIZACION = "0"; }

                        if (exist.VENCIMIENTO == true)
                        {
                            if (tab.Tables[0].Rows[m][6].ToString() == null | tab.Tables[0].Rows[m][6].ToString() == "")
                            {
                                ind = "no";
                                ldf3.Add("6");
                            }
                            else
                            {
                                docupF.VENCIMIENTO = Convert.ToDateTime(tab.Tables[0].Rows[m][6]);
                            }
                        }
                        else { docupF.VENCIMIENTO = null; }

                        if (exist.FACTURAK == true)
                        {
                            if (tab.Tables[0].Rows[m][7].ToString() == null | tab.Tables[0].Rows[m][7].ToString() == "")
                            {
                                ind = "no";
                                ldf3.Add("7");
                            }
                            else
                            {
                                docupF.FACTURAK = tab.Tables[0].Rows[m][7].ToString();
                            }
                        }
                        else { docupF.FACTURAK = "0"; }

                        if (exist.EJERCICIOK == true)
                        {
                            if (tab.Tables[0].Rows[m][8].ToString() == null | tab.Tables[0].Rows[m][8].ToString() == "")
                            {
                                ind = "no";
                                ldf3.Add("8");
                            }
                            else
                            {
                                docupF.EJERCICIOK = tab.Tables[0].Rows[m][8].ToString();
                            }
                        }
                        else { docupF.EJERCICIOK = "0"; }

                        if (exist.BILL_DOC == true)
                        {
                            if (tab.Tables[0].Rows[m][9].ToString() == null | tab.Tables[0].Rows[m][9].ToString() == "")
                            {
                                ind = "no";
                                ldf3.Add("9");
                            }
                            else
                            {
                                docupF.BILL_DOC = tab.Tables[0].Rows[m][9].ToString();
                            }
                        }
                        else { docupF.BILL_DOC = "0"; }

                        if (exist.BELNR == true)
                        {
                            if (tab.Tables[0].Rows[m][10].ToString() == null | tab.Tables[0].Rows[m][10].ToString() == "")
                            {
                                ind = "no";
                                ldf3.Add("10");
                            }
                            else
                            {
                                docupF.BELNR = tab.Tables[0].Rows[m][10].ToString();
                            }
                        }
                        else { docupF.BELNR = "0"; }

                        if (ind != "no")
                        {
                            cabeza = "<tr class='green white-text'>";
                        }
                        else
                        {
                            cabeza = "<tr class='red white-text'>";
                        }


                        ldf.Add(docupF);
                        ldf2.Add(cabeza);
                        //ldf.Add(docupF);
                    }
                }
            }
            ldf.Add(ldf2);

            JsonResult jr = Json(ldf, JsonRequestBehavior.AllowGet);
            return jr;
        }

        //[HttpGet]
        //public JsonResult Hoja4(string NUM_DOC)
        //{
        //    List<object> lda = new List<object>();

        //    JsonResult jr = Json(lda, JsonRequestBehavior.AllowGet);
        //    return jr;
        //}

        public bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

            return isNum;
        }

        public List<DOCUMENTOA> subeArchivo(IEnumerable<HttpPostedFileBase> files_soporte, string u, string tso, DataSet ds4, decimal numDo)
        {
            List<HttpPostedFileBase> archiArr = new List<HttpPostedFileBase>();
            List<DOCUMENTOA> docupA = new List<DOCUMENTOA>();
            SolicitudesController sc = new SolicitudesController();
            int numFiles = 0;

            //REVISAR SI HAY ARCHIVOS POR SUBIR
            try
            {
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
            }
            catch (Exception e) { }

            for (int jj = 1; jj < ds4.Tables[0].Rows.Count; jj++)
            {
                string num = ds4.Tables[0].Rows[jj][0].ToString().Trim();
                string descrip = ds4.Tables[0].Rows[jj][1].ToString().Trim();
                string ruta = ds4.Tables[0].Rows[jj][2].ToString().Trim();

                if (IsNumeric(num))
                {
                    if (num == numDo.ToString())
                    {
                        if (numFiles > 0)
                        {
                            foreach (HttpPostedFileBase file in files_soporte)
                            {
                                var clasefile = "";
                                try
                                {
                                    clasefile = descrip;
                                }
                                catch (Exception ex)
                                {
                                    clasefile = "";
                                }

                                if (file != null)
                                {
                                    if (file.ContentLength > 0)
                                    {
                                        string nombreV = file.FileName.ToUpper();

                                        if (nombreV == ruta.ToUpper())
                                        {
                                            string miDes = clasefile.ToUpper().Substring(0, 3);
                                            //VERIFICAMOS EL TIPO DE SOPORTE
                                            if (miDes == "FAC")
                                            {
                                                var exist = docupA.Where(x => x.NUM_DOC == Convert.ToDecimal(num) & x.CLASE == miDes).FirstOrDefault();

                                                //SI YA EXISTE UN TIPO DE SOPORTE FACTURA NO INSERTAMOS UNO NUEVO
                                                if (exist == null)
                                                {
                                                    string path = "";
                                                    DOCUMENTOA doc = new DOCUMENTOA();
                                                    doc.NUM_DOC = Convert.ToInt32(num);
                                                    doc.POS = jj;
                                                    doc.TIPO = Path.GetExtension(nombreV).Replace(".", "");
                                                    try
                                                    {
                                                        var clasefileM = clasefile.ToUpper();
                                                        doc.CLASE = clasefileM.Substring(0, 3);
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        doc.CLASE = "";
                                                    }

                                                    doc.STEP_WF = 1;
                                                    doc.USUARIO_ID = u;
                                                    doc.PATH = nombreV;
                                                    doc.ACTIVO = true;

                                                    docupA.Add(doc);
                                                    archiArr.Add(file);
                                                }
                                            }
                                            else
                                            {
                                                string path = "";
                                                DOCUMENTOA doc = new DOCUMENTOA();
                                                doc.NUM_DOC = Convert.ToInt32(num);
                                                doc.POS = jj;
                                                doc.TIPO = Path.GetExtension(nombreV).Replace(".", "");
                                                try
                                                {
                                                    var clasefileM = clasefile.ToUpper();
                                                    doc.CLASE = clasefileM.Substring(0, 3);
                                                }
                                                catch (Exception e)
                                                {
                                                    doc.CLASE = "";
                                                }

                                                doc.STEP_WF = 1;
                                                doc.USUARIO_ID = u;
                                                doc.PATH = nombreV;
                                                doc.ACTIVO = true;

                                                docupA.Add(doc);
                                                archiArr.Add(file);
                                            }
                                        }
                                    }
                                }
                            }//LLAVE FOREACH
                        }//IF NUMERO DE ARCHUVOS MAYOR A 0
                    }
                }
            }//LLAVE DE FOR PARA LOS REGISTROS DEL EXCEL
            List<HttpPostedFileBase> archivosRENew = (List<HttpPostedFileBase>)Session["arcReales"];

            if (archivosRENew == null)
            {
                archivosRENew = new List<HttpPostedFileBase>();
            }
            archivosRENew.AddRange(archiArr);
            Session["arcReales"] = archivosRENew;
            return docupA;
        }

        public void guardaArchivos(decimal numDoc)
        {
            IEnumerable<HttpPostedFileBase> archivosRE = (IEnumerable<HttpPostedFileBase>)Session["arcReales"];
            SolicitudesController sc = new SolicitudesController();
            string errorString = "";
            //Guardar los documentos cargados en la sección de soporte
            var res = "";
            string errorMessage = "";
            int numFiles = 0;
            //Checar si hay archivos para subir
            try
            {
                foreach (HttpPostedFileBase file in archivosRE)
                {
                    if (file != null)
                    {
                        if (file.ContentLength > 0)
                        {
                            numFiles++;
                        }
                    }
                }
            }
            catch (Exception e) { }

            if (numFiles > 0)
            {
                //Obtener las variables con los datos de sesión y ruta
                string url = ConfigurationManager.AppSettings["URL_SAVE"];
                //Crear el directorio
                string nomNum = numDoc.ToString();
                var dir = sc.createDir(url, nomNum);

                //Evaluar que se creo el directorio
                if (dir.Equals(""))
                {
                    foreach (HttpPostedFileBase file in archivosRE)
                    {
                        string errorfiles = "";
                        string path = "";
                        errorfiles = "";
                        res = sc.SaveFile(file, url, nomNum, out errorfiles, out path);
                        var cambio = db.DOCUMENTOAs.Where(x => x.NUM_DOC == numDoc & x.PATH == file.FileName).FirstOrDefault();
                        if (cambio != null)
                        {
                            cambio.PATH = path;
                            db.SaveChanges();
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
                Session["ERROR_FILES"] = errorMessage;
            }
        }

        [HttpPost]
        public ActionResult Soportes(HttpPostedFileBase[] file2)
        {
            int sop;

            if (file2[0] != null)
            {
                Session["archivosSop"] = file2;
                ViewBag.contenidoSop = 1;
                sop = 1;
            }
            else
            {
                ViewBag.contenidoSop = 0;
                sop = 0;
            }

            int? masSop = (int?)Session["indMas"];

            if (masSop == null)
            {
                masSop = 0;
            }

            Session["indSop"] = sop;
            return RedirectToAction("Index", new { miSop = sop, miMas = masSop });
        }

        [HttpPost]
        public ActionResult Masiva(HttpPostedFileBase file)
        {
            int mas;

            if (file != null)
            {
                Session["archivoMas"] = file;
                ViewBag.contenidoMas = 1;
                mas = 1;
            }
            else
            {
                ViewBag.contenidoMas = 0;
                mas = 0;
            }

            int? sopMas = (int?)Session["indSop"];

            if (sopMas == null)
            {
                sopMas = 0;
            }

            Session["indMas"] = mas;
            return RedirectToAction("Index", new { miSop = sopMas, miMas = mas });
        }
    }
}