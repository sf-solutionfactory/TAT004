using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using TAT001.Entities;
using TAT001.Models;

namespace TAT001.Controllers
{
    [Authorize]
    public class ExcelController : Controller
    {
        private TAT001Entities db = new TAT001Entities();
        // GET: Excel
        public ActionResult Index()
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

            var doc = db.DOCUMENTOPs;
            //return View(doc.ToList());
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
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

            DataSet dsHoja1 = new DataSet();
            DataSet dsHoja2 = new DataSet();
            string excelConnectionString = string.Empty;
            if (Request.Files["file"].ContentLength > 0)
            {
                string fileExtension = System.IO.Path.GetExtension(Request.Files["file"].FileName);

                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    ////string fileLocation = Server.MapPath("~/Content/") + Request.Files["file"].FileName;

                    ////Request.Files["file"].SaveAs(fileLocation);

                    //////connection String for xls file format.
                    ////if (fileExtension == ".xls")
                    ////{
                    ////    excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                    ////    fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    ////}
                    //////connection String for xlsx file format.
                    ////else if (fileExtension == ".xlsx")
                    ////{
                    ////    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                    ////    fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    ////}

                    //////Create Connection to Excel work book and add oledb namespace
                    ////OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    ////excelConnection.Open();
                    ////DataTable dt = new DataTable();





                    ////HttpPostedFileBase file2 = Request.Files["FileUpload"];
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
                    DataSet dt = new DataSet();
                    DataTable dtt = result.Tables[0].Copy();
                    dt.Tables.Add(dtt);
                    DataSet dt1 = new DataSet();
                    DataTable dtt1 = result.Tables[1].Copy();
                    dt1.Tables.Add(dtt1);


                    Session["ds1"] = dt;
                    Session["ds2"] = dt1;


                    ////dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    ////if (dt == null)
                    ////        {
                    ////            return null;
                    ////        }

                    ////        String[] excelSheets = new String[dt.Rows.Count];
                    ////        int t = 0;

                    ////        //excel data saves in temp file here.
                    ////        foreach (DataRow row in dt.Rows)
                    ////        {
                    ////            excelSheets[t] = row["TABLE_NAME"].ToString();
                    ////            t++;
                    ////        }

                    ////        OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

                    ////        string hoja1 = string.Format("Select * from [{0}]", excelSheets[1]);
                    ////        string hoja2 = string.Format("Select * from [{0}]", excelSheets[0]);

                    ////        using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(hoja1, excelConnection1))
                    ////        {
                    ////            dataAdapter.Fill(dsHoja1);
                    ////        }

                    ////        using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(hoja2, excelConnection1))
                    ////        {
                    ////            dataAdapter.Fill(dsHoja2);
                    ////        }
                }
            }
            ////Session["ds1"] = dsHoja1;
            ////Session["ds2"] = dsHoja2;
            return RedirectToAction("Details");
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
            catch
            {
                //return RedirectToAction("Pais", "Home");
            }
            Session["spras"] = user.SPRAS_ID;

            ViewBag.data1 = Session["ds1"];
            ViewBag.data2 = Session["ds2"];
            DataSet hoja1 = (DataSet)(Session["ds1"]);
            DataSet hoja2 = (DataSet)(Session["ds2"]);

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

            List<DOCUMENTO> listD = new List<DOCUMENTO>();
            for (int i = 1; i < dsHoja1.Tables[0].Rows.Count; i++)
            {
                DOCUMENTO docu = new DOCUMENTO();
                string a = dsHoja1.Tables[0].Rows[i][1].ToString();
                string b = dsHoja1.Tables[0].Rows[i][2].ToString();
                string c = dsHoja1.Tables[0].Rows[i][3].ToString();
                string d = dsHoja1.Tables[0].Rows[i][4].ToString();
                var e = db.TSOLs.Where(x => x.ID == a).Select(x => x.ID);
                var f = db.GALLs.Where(x => x.ID == b).Select(x => x.ID);
                var g = db.SOCIEDADs.Where(x => x.BUKRS == c).Select(x => x.BUKRS);
                var h = db.PAIS.Where(x => x.LAND == d).Select(x => x.LAND);

                string aa = dsHoja1.Tables[0].Rows[i][9].ToString();
                string bb = dsHoja1.Tables[0].Rows[i][10].ToString();
                string cc = dsHoja1.Tables[0].Rows[i][11].ToString();
                string dd = dsHoja1.Tables[0].Rows[i][12].ToString();
                string mone = dsHoja1.Tables[0].Rows[i][17].ToString();
                var ee = db.CLIENTEs.Where(x => x.VKORG == aa & x.VTWEG == bb & x.SPART == cc & x.KUNNR == dd);
                var monee = db.MONEDAs.Where(x => x.WAERS == mone).Select(x => x.WAERS);

                if (e.Count() > 0 && f.Count() > 0 && g.Count() > 0 && h.Count() > 0 & ee.Count() > 0 & monee.Count() > 0)
                {
                    docu.NUM_DOC = Convert.ToDecimal(dsHoja1.Tables[0].Rows[i][0].ToString());
                    docu.TSOL_ID = dsHoja1.Tables[0].Rows[i][1].ToString();
                    docu.GALL_ID = dsHoja1.Tables[0].Rows[i][2].ToString();
                    docu.SOCIEDAD_ID = dsHoja1.Tables[0].Rows[i][3].ToString();
                    docu.PAIS_ID = dsHoja1.Tables[0].Rows[i][4].ToString();
                    docu.ESTADO = dsHoja1.Tables[0].Rows[i][5].ToString();
                    docu.CIUDAD = dsHoja1.Tables[0].Rows[i][6].ToString();
                    docu.PERIODO = Convert.ToInt32(System.DateTime.Now.Month);
                    docu.EJERCICIO = Convert.ToString(System.DateTime.Now.Year);
                    docu.CANTIDAD_EV = 1;
                    docu.USUARIOC_ID = user.ID;
                    docu.FECHAD = System.DateTime.Today;
                    docu.FECHAC = System.DateTime.Today;
                    docu.HORAC = System.DateTime.Now.TimeOfDay;
                    docu.FECHAC_PLAN = System.DateTime.Today;
                    docu.FECHAC_USER = System.DateTime.Today;
                    docu.HORAC_USER = System.DateTime.Now.TimeOfDay;
                    docu.ESTATUS = "N";
                    docu.ESTATUS_WF = "P";
                    docu.CONCEPTO = dsHoja1.Tables[0].Rows[i][7].ToString();
                    docu.NOTAS = dsHoja1.Tables[0].Rows[i][8].ToString();
                    docu.VKORG = dsHoja1.Tables[0].Rows[i][9].ToString();
                    docu.VTWEG = dsHoja1.Tables[0].Rows[i][10].ToString();
                    docu.SPART = dsHoja1.Tables[0].Rows[i][11].ToString();
                    docu.PAYER_ID = dsHoja1.Tables[0].Rows[i][12].ToString();
                    docu.PAYER_NOMBRE = dsHoja1.Tables[0].Rows[i][13].ToString();
                    docu.PAYER_EMAIL = dsHoja1.Tables[0].Rows[i][14].ToString();
                    docu.FECHAI_VIG = Convert.ToDateTime(dsHoja1.Tables[0].Rows[i][15].ToString());
                    docu.FECHAF_VIG = Convert.ToDateTime(dsHoja1.Tables[0].Rows[i][16].ToString());
                    docu.MONEDA_ID = dsHoja1.Tables[0].Rows[i][17].ToString();
                    docu.MONTO_DOC_MD = 0;
                    docu.TALL_ID = db.TALLs.Where(x => x.GALL_ID == docu.GALL_ID).FirstOrDefault().ID;
                    listD.Add(docu);

                    DOCUMENTO dop = listD.Where(x => x.NUM_DOC == docu.NUM_DOC).FirstOrDefault();
                    if (dop != null)
                    {
                        for (int k = 1; k < dsHoja2.Tables[0].Rows.Count; k++)
                        {
                            decimal num = Convert.ToDecimal(dsHoja2.Tables[0].Rows[k][0].ToString());
                            if (num == docu.NUM_DOC)
                            {
                                DOCUMENTOP docup = new DOCUMENTOP();
                                int j = k;

                                string mat = dsHoja2.Tables[0].Rows[k][3].ToString();
                                string mat2 = dsHoja2.Tables[0].Rows[k][4].ToString();
                                DateTime fechD = Convert.ToDateTime(dsHoja2.Tables[0].Rows[k][1].ToString());
                                DateTime fechA = Convert.ToDateTime(dsHoja2.Tables[0].Rows[k][2].ToString());
                                //var mate = db.MATERIALs.Where(x => x.ID == mat && x.MATKL_ID == mat2);
                                var mate = db.MATERIALs.Where(x => x.ID == mat);

                                if (mate.Count() > 0 && fechA > fechD)
                                {
                                    if (dsHoja2.Tables[0].Rows[k][9].ToString() != "")
                                    {
                                        docup.NUM_DOC = Convert.ToInt32(dsHoja2.Tables[0].Rows[k][0].ToString());
                                        docup.POS = Convert.ToInt32(j + 1);
                                        docup.VIGENCIA_DE = Convert.ToDateTime(dsHoja2.Tables[0].Rows[k][1].ToString());
                                        docup.VIGENCIA_AL = Convert.ToDateTime(dsHoja2.Tables[0].Rows[k][2].ToString());
                                        docup.MATNR = dsHoja2.Tables[0].Rows[k][3].ToString();
                                        docup.MATKL = dsHoja2.Tables[0].Rows[k][4].ToString();

                                        if (db.TSOLs.Where(ar => ar.ID == dop.TSOL_ID).FirstOrDefault().FACTURA == true)
                                        {
                                            docup.APOYO_REAL = Convert.ToDecimal(dsHoja2.Tables[0].Rows[k][9].ToString());
                                            dop.MONTO_DOC_MD += docup.APOYO_REAL;
                                        }
                                        else
                                        {
                                            docup.APOYO_EST = Convert.ToDecimal(dsHoja2.Tables[0].Rows[k][9].ToString());
                                            dop.MONTO_DOC_MD += docup.APOYO_EST;
                                        }

                                        docup.CANTIDAD = 0;
                                        dop.DOCUMENTOPs.Add(docup);
                                        //db.SaveChanges();
                                    }
                                    else
                                    {
                                        docup.NUM_DOC = Convert.ToInt32(dsHoja2.Tables[0].Rows[k][0].ToString());
                                        docup.POS = Convert.ToInt32(j + 1);
                                        docup.VIGENCIA_DE = Convert.ToDateTime(dsHoja2.Tables[0].Rows[k][1].ToString());
                                        docup.VIGENCIA_AL = Convert.ToDateTime(dsHoja2.Tables[0].Rows[k][2].ToString());
                                        docup.MATNR = dsHoja2.Tables[0].Rows[k][3].ToString();
                                        docup.MATKL = dsHoja2.Tables[0].Rows[k][4].ToString();
                                        docup.MONTO = Convert.ToDecimal(dsHoja2.Tables[0].Rows[k][5].ToString());
                                        docup.PORC_APOYO = Convert.ToDecimal(dsHoja2.Tables[0].Rows[k][6].ToString());
                                        docup.PRECIO_SUG = Convert.ToDecimal(dsHoja2.Tables[0].Rows[k][7].ToString());
                                        docup.VOLUMEN_REAL = Convert.ToDecimal(dsHoja2.Tables[0].Rows[k][8].ToString());
                                        decimal hola = (Convert.ToDecimal(dsHoja2.Tables[0].Rows[k][5].ToString()) * Convert.ToDecimal(dsHoja2.Tables[0].Rows[k][6].ToString()));
                                        hola = (Convert.ToDecimal(dsHoja2.Tables[0].Rows[k][5].ToString()) - hola) * (Convert.ToDecimal(dsHoja2.Tables[0].Rows[k][8].ToString()));


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
                                        //db.SaveChanges();
                                    }
                                }
                            }
                        }
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
                db1.SaveChanges();
                updateRango(doc.TSOL_ID, doc.NUM_DOC);
                li.Add(doc.NUM_DOC.ToString());
            }

            //JsonResult jr = Json(li, JsonRequestBehavior.AllowGet);
            TempData["docs_masiva"] = li;
            return RedirectToAction("Index", "Home");
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

            for (var i = 1; i < tab.Tables[0].Rows.Count; i++)
            {
                if (tab.Tables[0].Rows[i][0].ToString() == NUM_DOC)
                {
                    DOCUMENTOP docup = new DOCUMENTOP();
                    int j = i;
                    docup.NUM_DOC = Convert.ToInt32(tab.Tables[0].Rows[i][0].ToString());
                    docup.POS = Convert.ToInt32(j + 1);

                    string material = tab.Tables[0].Rows[i][3].ToString();


                    if (tab.Tables[0].Rows[i][3].ToString() == "")
                    { docup.MATNR = "0"; }
                    else
                    {
                        string mat = tab.Tables[0].Rows[i][3].ToString();
                        var e = db.MATERIALs.Where(x => x.ID == mat).Select(x => x.ID);
                        if (e.Count() > 0)
                        {
                            docup.MATNR = mat;
                        }
                        else
                        {
                            docup.MATNR = "0";
                        }
                    }

                    if (tab.Tables[0].Rows[i][4].ToString() == "")
                    { docup.MATKL = "0"; }
                    else
                    {
                        string mat2 = tab.Tables[0].Rows[i][4].ToString();
                        var a = db.MATERIALs.Where(x => x.MATKL_ID == mat2).Select(x => x.MATKL_ID);
                        if (a.Count() > 0)
                        {
                            docup.MATKL = mat2;
                        }
                        else
                        {
                            docup.MATKL = "0";
                        }
                    }

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
        public bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

            return isNum;
        }
    }
}