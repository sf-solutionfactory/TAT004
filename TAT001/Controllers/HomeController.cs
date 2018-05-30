﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TAT001.Entities;
using TAT001.Models;
using ClosedXML.Excel;

namespace TAT001.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private TAT001Entities db = new TAT001Entities();

        [Authorize]
        public ActionResult Index(string pais)
        {
            using (TAT001Entities db = new TAT001Entities())
            {
                int pagina = 101; //ID EN BASE DE DATOS
                string u = User.Identity.Name;
                if (pais != null)
                    Session["pais"] = pais;
                //string u = "admin";
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
                    //ViewBag.pais = "mx.svg";
                    ////return RedirectToAction("Pais", "Home");
                }
                Session["spras"] = user.SPRAS_ID;

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
            dOCUMENTOes = dOCUMENTOes.OrderByDescending(a => a.FECHAC).OrderByDescending(a => a.NUM_DOC).ToList();
            ViewBag.Clientes = db.CLIENTEs.ToList();
            ViewBag.Cuentas = db.CUENTAs.ToList();
            ViewBag.DOCF = db.DOCUMENTOFs.ToList();
            return View(dOCUMENTOes);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelPais(string pais)
        {
            Session["pais"] = pais.ToUpper() ;
            return View();
        }

        [Authorize]
        public ActionResult Pais(string pais)
        {
            using (TAT001Entities db = new TAT001Entities())
            {
                int pagina = 102; //ID EN BASE DE DATOS
                string u = User.Identity.Name;
                var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                ViewBag.permisos = db.PAGINAVs.Where(a => a.ID.Equals(user.ID)).ToList();
                ViewBag.carpetas = db.CARPETAVs.Where(a => a.USUARIO_ID.Equals(user.ID)).ToList();
                ViewBag.usuario = user;
                //ViewBag.rol = user.PUESTO.PUESTOTs.Where(a => a.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                ViewBag.rol = user.PUESTO.PUESTOTs.Where(a => a.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                ViewBag.flag = true;
                ViewBag.Title = db.PAGINAs.Where(a => a.ID.Equals(pagina)).FirstOrDefault().PAGINATs.Where(b => b.SPRAS_ID.Equals(user.SPRAS_ID)).FirstOrDefault().TXT50;
                ViewBag.warnings = db.WARNINGVs.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();
                ViewBag.textos = db.TEXTOes.Where(a => (a.PAGINA_ID.Equals(pagina) || a.PAGINA_ID.Equals(0)) && a.SPRAS_ID.Equals(user.SPRAS_ID)).ToList();

                //var p = db.PAIS.Where(a => a.ACTIVO.Equals(true));

                //ViewBag.creador = db.CREADORs.Where(a => a.ID.Equals(u) & a.ACTIVO == true).ToList();

                var p = from P in db.PAIS
                    join C in db.CREADOR2 on P.LAND equals C.LAND
                    where P.ACTIVO == true
                    & C.ID == u & C.ACTIVO == true
                    select P;

                return View(p.ToList());
            }
            //return View();

        }

        [HttpPost]
        public FileResult Descargar()
        {
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
                dOCUMENTOes.Add(d);
            }
            dOCUMENTOes = dOCUMENTOes.Distinct(new DocumentoComparer()).ToList();
            dOCUMENTOes = dOCUMENTOes.OrderByDescending(a => a.FECHAC).OrderByDescending(a => a.NUM_DOC).ToList();
            generarExcelHome(dOCUMENTOes, Server.MapPath("~/pdfTemp/"));
            return File(Server.MapPath("~/pdfTemp/Doc" + DateTime.Now.ToShortDateString() + ".xlsx"), "application /vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Doc" + DateTime.Now.ToShortDateString() + ".xlsx");
        }

        public void generarExcelHome(List<DOCUMENTO> lst, string ruta)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sheet 1");
            try
            {
                //Creamos el encabezado
                worksheet.Cell("A1").Value = new[]
             {
                  new {
                      BANNER = "Número Solicitud"
                      },
                    };
                worksheet.Cell("B1").Value = new[]
            {
                  new {
                      BANNER = "Company Code"
                      },
                    };
                worksheet.Cell("C1").Value = new[]
            {
                  new {
                      BANNER = "País"
                      },
                    };
                worksheet.Cell("D1").Value = new[]
            {
                  new {
                      BANNER = "Fecha Solicitud"
                      },
                    };
                worksheet.Cell("E1").Value = new[]
            {
                  new {
                      BANNER = "Hora de Solicitud"
                      },
                    };
                worksheet.Cell("F1").Value = new[]
            {
                  new {
                      BANNER = "Periodo Contable"
                      },
                    };
                worksheet.Cell("G1").Value = new[]
              {
                  new {
                      BANNER = "Status"
                      },
                    };
                worksheet.Cell("H1").Value = new[]
            {
                  new {
                      BANNER = "Payer"
                      },
                    };
                worksheet.Cell("I1").Value = new[]
            {
                  new {
                      BANNER = "Cliente"
                      },
                    };
                worksheet.Cell("J1").Value = new[]
                {
                    new {
                        BANNER = "Canal"
                    },
                };
                worksheet.Cell("K1").Value = new[]
            {
                  new {
                      BANNER = "Tipo Solicitud"
                      },
                    };
                worksheet.Cell("L1").Value = new[]
            {
                  new {
                      BANNER = "Clasificación Allowances"
                      },
                    };
                worksheet.Cell("M1").Value = new[]
      {
                  new {
                      BANNER = "Cuenta Contable Gasto"
                      },
                    };
                worksheet.Cell("N1").Value = new[]
                {
                  new {
                      BANNER = "Descripción Solicitud"
                      },
                    };
                worksheet.Cell("O1").Value = new[]
      {
                  new {
                      BANNER = "$ Importe Solicitud"
                      },
                    };
                worksheet.Cell("P1").Value = new[]
      {
                  new {
                      BANNER = "Número Factura Proveedor"
                      },
                    };
                worksheet.Cell("Q1").Value = new[]
      {
                  new {
                      BANNER = "Número Factura"
                      },
                    };
                worksheet.Cell("R1").Value = new[]
                {
                  new {
                      BANNER = "Created By"
                      },
                    };
                worksheet.Cell("S1").Value = new[]
     {
                  new {
                      BANNER = "Modified By"
                      },
                    };
                worksheet.Cell("T1").Value = new[]
      {
                  new {
                      BANNER = "Número Nota Crédito/Orden de Pago"
                      },
                    };
                worksheet.Cell("U1").Value = new[]
      {
                  new {
                      BANNER = "Núm. Registro Provisión"
                      },
                    };
                worksheet.Cell("V1").Value = new[]
     {
                  new {
                      BANNER = "Núm. Registro NC/OP"
                      },
                    };
                worksheet.Cell("W1").Value = new[]
  {
                  new {
                      BANNER = "Num Registro AP"
                      },
                    };
                worksheet.Cell("X1").Value = new[]
                {
                  new {
                      BANNER = "Núm. Registro Reverso"
                      },
                    };
                worksheet.Cell("Y1").Value = new[]
                {
                  new {
                      BANNER = "Tipo Documento"
                      },
                    };
                worksheet.Cell("Z1").Value = new[]
                {
                  new {
                      BANNER = "Payer"
                      },
                    };
                worksheet.Cell("AA1").Value = new[]
               {
                  new {
                      BANNER = "Cliente"
                      },
                    };
                worksheet.Cell("AB1").Value = new[]
              {
                  new {
                      BANNER = "$ Importe Moneda Local"
                      },
                    };
                worksheet.Cell("AC1").Value = new[]
              {
                  new {
                      BANNER = "Cuenta Contable Gasto"
                      },
                    };
                for (int i = 2; i <= (lst.Count + 1); i++)
                {
                    worksheet.Cell("A" + i).Value = new[]
                 {
                  new {
                      BANNER       = lst[i-2].NUM_DOC
                      },
                    };
                    worksheet.Cell("B" + i).Value = new[]
                {
                  new {
                      BANNER       = lst[i-2].SOCIEDAD_ID
                      },
                    };
                    worksheet.Cell("C" + i).Value = new[]
                 {
                    new {
                        BANNER       = lst[i-2].PAIS_ID
                        },
                      };
                    worksheet.Cell("D" + i).Value = new[]
                  {
                   new {
                       BANNER       = lst[i-2].FECHAC.Value.Day+"/"+ lst[i-2].FECHAC.Value.Month+"/"+ lst[i-2].FECHAC.Value.Year
                       },
                     };
                    worksheet.Cell("E" + i).Value = new[]
               {
                  new {
                      BANNER       = lst[i-2].HORAC.ToString().Split('.')[0]
                      },
                    };
                    var fx = lst[i - 2].FECHAC.Value.Month;
                    worksheet.Cell("F" + i).Value = new[]
                 {
                  new {
                      BANNER       = fx
                      },
                    };
                    //G
                    if (lst[i - 2].ESTATUS_WF == "P")
                    {
                        worksheet.Cell("G" + i).Value = new[]
                    {
                        new
                        {
                            BANNER = "PENDIENTE"
                        },
                };
                        worksheet.Range("G" + i + ":G" + i).Style.Font.SetFontColor(XLColor.White).Fill.SetBackgroundColor(XLColor.FromHtml("#FFD700")).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Font.SetBold(true);
                    }
                    else if (lst[i - 2].ESTATUS_WF == "A")
                    {
                        if (lst[i - 2].ESTATUS == "C")
                        {
                            worksheet.Cell("G" + i).Value = new[]
                  {
                        new
                        {
                            BANNER = "Por Contabilizar"
                        },
                };
                            worksheet.Range("G" + i + ":G" + i).Style.Font.SetFontColor(XLColor.White).Fill.SetBackgroundColor(XLColor.FromHtml("#197319")).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Font.SetBold(true);
                        }
                        else if (lst[i - 2].ESTATUS == "P")
                        {
                            worksheet.Cell("G" + i).Value = new[]
                  {
                        new
                        {
                            BANNER = "Contabilizar SAP"
                        },
                };
                            worksheet.Range("G" + i + ":G" + i).Style.Font.SetFontColor(XLColor.White).Fill.SetBackgroundColor(XLColor.FromHtml("#197319")).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Font.SetBold(true);
                        }
                        else
                        {
                            worksheet.Cell("G" + i).Value = new[]
                  {
                        new
                        {
                            BANNER = "Aprobada"
                        },
                };
                            worksheet.Range("G" + i + ":G" + i).Style.Font.SetFontColor(XLColor.White).Fill.SetBackgroundColor(XLColor.FromHtml("#197319")).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Font.SetBold(true);
                        }
                    }
                    else if (lst[i - 2].ESTATUS_WF == "R")
                    {
                        worksheet.Cell("G" + i).Value = new[]
                    {
                        new
                        {
                            BANNER = "RECHAZADA"
                        },
                };
                        worksheet.Range("G" + i + ":G" + i).Style.Font.SetFontColor(XLColor.White).Fill.SetBackgroundColor(XLColor.FromHtml("#cc3300")).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Font.SetBold(true);
                    }
                    worksheet.Cell("H" + i).Value = new[]
                {
                    new {
                        BANNER       = lst[i-2].PAYER_ID
                        },
                      };
                    //Para sacar el Nombre "I" y "J"
                    var cl = db.CLIENTEs.ToList();
                    for (int y = 0; y < cl.Count; y++)
                    {
                        if (cl[y].KUNNR == lst[i - 2].PAYER_ID)
                        {
                            worksheet.Cell("I" + i).Value = new[]
                            {
                                //List Clientes y for para sacar su name 
                                new {
                                    BANNER = cl[y].NAME1
                                },
                            };
                            break;
                        }
                    }
                    for (int y = 0; y < cl.Count; y++)
                    {
                        if (cl[y].KUNNR == lst[i - 2].PAYER_ID)
                        {
                            worksheet.Cell("J" + i).Value = new[]
                            {
                                //   List Clientes y for para sacar su name
                                new
                                {
                                    BANNER = cl[y].CANAL
                                },
                            };
                        }
                    }
                    worksheet.Cell("K" + i).Value = new[]
                    {
                        new {
                            BANNER       = lst[i-2].TSOL.TSOLTs.Where(a => a.SPRAS_ID.Equals("ES")).FirstOrDefault().TXT020
                        },
                    };
                    worksheet.Cell("L" + i).Value = new[]
                   {
                        new {
                            BANNER       = lst[i-2].TALL.TALLTs.Where(a => a.SPRAS_ID.Equals("ES")).FirstOrDefault().TXT50
                        },
                    };
                    var lstC = db.CUENTAs.ToList();
                    for (int y = 0; y < lstC.Count; y++)
                    {
                        if (lstC[y].SOCIEDAD_ID == lst[i - 2].SOCIEDAD_ID && lstC[y].GALL_ID == lst[i - 2].TALL.GALL_ID)
                        {
                            worksheet.Cell("M" + i).Value = new[]
                            {
                                new {
                                    BANNER       = lstC[y].CARGO
                                },
                            };
                            break;
                        }
                    }
                    worksheet.Cell("N" + i).Value = new[]
                   {
                        new {
                            BANNER       = lst[i-2].CONCEPTO
                        },
                    };
                    worksheet.Cell("O" + i).Value = new[]
                   {
                        new {
                            BANNER       = lst[i-2].MONTO_DOC_ML
                        },
                    };
                    var f = db.DOCUMENTOFs.ToList();
                    for (int y = 0; y < f.Count; y++)
                    {
                        if (f[y].NUM_DOC == lst[i - 2].NUM_DOC)
                        {
                            worksheet.Cell("P" + i).Value = new[]
                            {
                                new {
                                    BANNER       = f[y].FACTURA
                                },
                            };
                            break;
                        }
                    }
                    for (int y = 0; y < f.Count; y++)
                    {
                        if (f[y].NUM_DOC == lst[i - 2].NUM_DOC)
                        {
                            worksheet.Cell("Q" + i).Value = new[]
                            {
                                new {
                                    BANNER       = f[y].FACTURAK
                                },
                            };
                            break;
                        }
                    }
                    worksheet.Cell("R" + i).Value = new[]
                    {
                        new {
                            BANNER       = lst[i-2].USUARIOC_ID
                        },
                    };
                }
                var rt = ruta + @"\Doc" + DateTime.Now.ToShortDateString() + ".xlsx";
                workbook.SaveAs(rt);
            }
            catch (Exception e)
            {
                var ex = e.ToString();
            }

        }

        [HttpGet]
        public JsonResult Clientes(string Prefix)
        {
            if (Prefix == null)
                Prefix = "";

            TAT001Entities db = new TAT001Entities();

            var c = (from N in db.CLIENTEs
                     where N.KUNNR.Contains(Prefix)
                     select new { N.KUNNR, N.NAME1 });
            JsonResult cc = Json(c, JsonRequestBehavior.AllowGet);
            return cc;
        }
    }
}