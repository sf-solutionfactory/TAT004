﻿using TAT001.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace TAT001.Models
{
    public class ArchivoContable
    {
        public string generarArchivo(decimal docum, decimal relacion)
        {
            TAT001Entities db = new TAT001Entities();
            try
            {
                string dirFile = "";
                DOCUMENTO doc = db.DOCUMENTOes.Where(x => x.NUM_DOC == docum).Single();
                CONPOSAPH tab;
                try
                {
                    if (relacion == 0)
                    {
                        tab = db.CONPOSAPHs.Where(x => x.TIPO_SOL == doc.TSOL_ID
                        && x.SOCIEDAD == doc.SOCIEDAD_ID
                        && x.FECHA_FINVIG >= doc.FECHAF_VIG
                        ).Single();
                    }
                    else
                    {
                        tab = db.CONPOSAPHs.Where(x => x.SOCIEDAD == doc.SOCIEDAD_ID
                        && x.FECHA_FINVIG >= doc.FECHAF_VIG
                        && x.CONSECUTIVO == relacion
                        ).Single();

                    }
                }
                catch (Exception)
                {
                    return "No se encontro configuracion para generar documento para este tipo de solicitud";
                }

                string txt = "";
                string msj = "";
                string[] cc;
                string cta = "";
                if (tab.TIPO_DOC == "RN" || tab.TIPO_DOC == "KR")
                {
                    dirFile = ConfigurationManager.AppSettings["URL_SAVE"] + @"POSTING\INBOUND_" + docum.ToString() + "_2.txt";
                }
                else if (tab.TIPO_DOC == "KG")
                {
                    dirFile = ConfigurationManager.AppSettings["URL_SAVE"] + @"POSTING\INBOUND_" + docum.ToString() + "_3.txt";
                }
                else
                {
                    dirFile = ConfigurationManager.AppSettings["URL_SAVE"] + @"POSTING\INBOUND_" + docum.ToString() + "_1.txt";
                }
                cta = doc.GALL_ID;
                doc.GALL_ID = db.GALLs.Where(x => x.ID == doc.GALL_ID).Select(x => x.GRUPO_ALL).Single();
                var ppd = doc.GetType().GetProperties();
                tab.HEADER_TEXT.Trim();
                if (String.IsNullOrEmpty(tab.HEADER_TEXT) == false)
                {
                    cc = tab.HEADER_TEXT.Trim().Split('+');
                    try
                    {
                        foreach (string c in cc)
                        {
                            txt += ppd.Where(x => x.Name == c).Single().GetValue(doc).ToString();
                        }
                    }
                    catch (Exception)
                    {
                        try
                        {
                            List<DOCUMENTOF> docf = db.DOCUMENTOFs.Where(x => x.NUM_DOC == docum).ToList();
                            var ppdf = docf[0].GetType().GetProperties();
                            foreach (string c in cc)
                            {
                                for (int i = 0; i < docf.Count; i++)
                                {
                                    txt += ppdf.Where(x => x.Name == c).Single().GetValue(docf[i]).ToString();
                                    break;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            txt = tab.REFERENCIA.Trim();
                        }
                    }
                }
                else
                {
                    return "Agrege comando para generar texto de encabezado";
                }
                tab.HEADER_TEXT = txt;
                txt = "";
                tab.REFERENCIA.Trim();
                if (String.IsNullOrEmpty(tab.REFERENCIA) == false)
                {
                    cc = tab.REFERENCIA.Trim().Split('+');
                    try
                    {
                        foreach (string c in cc)
                        {
                            txt += ppd.Where(x => x.Name == c).Single().GetValue(doc).ToString();
                        }
                    }
                    catch (Exception)
                    {
                        try
                        {
                            List<DOCUMENTOF> docf = db.DOCUMENTOFs.Where(x => x.NUM_DOC == docum).ToList();
                            var ppdf = docf[0].GetType().GetProperties();
                            foreach (string c in cc)
                            {
                                for (int i = 0; i < docf.Count; i++)
                                {
                                    if (i == 0)
                                    {
                                        txt += ppdf.Where(x => x.Name == c).Single().GetValue(docf[i]).ToString();
                                    }
                                    else
                                    {
                                        if (doc.PAIS_ID == "MX")
                                        {
                                            tab.NOTA += ppdf.Where(x => x.Name == c).Single().GetValue(docf[i]).ToString();
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            if (doc.PAIS_ID == "MX")
                            {
                                tab.CORRESPONDENCIA = docf[0].EJERCICIOK;
                            }
                        }
                        catch (Exception e)
                        {
                            txt = tab.REFERENCIA.Trim();
                        }
                    }
                }
                else
                {
                    return "Agrege comando para generar referencia";
                }
                tab.REFERENCIA = txt;
                doc.GALL_ID = cta;
                if (String.IsNullOrEmpty(tab.MONEDA))
                {
                    doc.MONEDA_ID = "";
                }

                doc.FECHAC = Fecha(tab.FECHA_CONTAB);

                List<DetalleContab> det = new List<DetalleContab>();
                msj = Detalle(doc, ref det, tab);
                if (msj != "")
                {
                    return msj;
                }

                using (StreamWriter sw = new StreamWriter(dirFile))
                {
                    CONPOSAPH dir = tab;
                    sw.WriteLine(
                        tab.TIPO_DOC + "|" +
                        dir.SOCIEDAD.Trim() + "|"
                        + String.Format("{0:MM.dd.yyyy}", doc.FECHAC).Replace(".", "") + "|"
                        + doc.MONEDA_ID.Trim() + "|"
                        + dir.HEADER_TEXT.Trim() + "|"
                        + dir.REFERENCIA.Trim() + "|"
                        + dir.CALC_TAXT.Trim() + "|"
                        + dir.NOTA.Trim() + "|"
                        + dir.CORRESPONDENCIA.Trim()
                        );
                    sw.WriteLine("");
                    for (int i = 0; i < det.Count; i++)
                    {
                        sw.WriteLine(
                            det[i].POS_TYPE + "|" +
                            det[i].COMP_CODE + "|" +
                            det[i].BUS_AREA + "|" +
                            det[i].POST_KEY + "|" +
                            det[i].ACCOUNT + "|" +
                            det[i].COST_CENTER + "|" +
                            det[i].BALANCE + "|" +
                            det[i].TEXT + "|" +
                            det[i].SALES_ORG + "|" +
                            det[i].DIST_CHANEL + "|" +
                            det[i].DIVISION + "|" +
                            //"|" +
                            //"|" +
                            //"|" +
                            //"|" +
                            //"|" +
                            det[i].INV_REF + "|" +
                            det[i].PAY_TERM + "|" +
                            det[i].JURIS_CODE + "|" +
                            //"|" +
                            det[i].CUSTOMER + "|" +
                            det[i].PRODUCT + "|" +
                            det[i].TAX_CODE + "|" +
                            det[i].PLANT + "|" +
                            det[i].REF_KEY1 + "|" +
                            det[i].REF_KEY3 + "|" +
                            det[i].ASSIGNMENT + "|" +
                            det[i].QTY + "|" +
                            det[i].BASE_UNIT + "|" +
                            det[i].AMOUNT_LC + "|"
                            );
                    }
                    sw.Close();
                }
                if (tab.RELACION != 0 && tab.RELACION != null)
                {
                    return generarArchivo(docum, Convert.ToInt32(tab.RELACION));
                }
                else
                {
                    return "";
                }
            }
            catch (Exception e)
            {
                return "Error al generar el documento contable";
            }
        }
        private DateTime Fecha(string id_fecha)
        {
            DateTime fecha = DateTime.Today;
            switch (id_fecha)
            {
                case "U":
                    fecha = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    fecha = fecha.AddMonths(1).AddDays(-1);
                    break;
                case "P":
                    fecha = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    break;
            }
            return fecha;
        }
        private string Detalle(DOCUMENTO doc, ref List<DetalleContab> contas, CONPOSAPH enca)
        {
            contas = new List<DetalleContab>();
            TAT001Entities db = new TAT001Entities();
            List<CONPOSAPP> conp = new List<CONPOSAPP>();
            CLIENTE clien;
            CUENTA cuent;
            TAXEOH tax = new TAXEOH();
            try
            {
                try
                {
                    conp = db.CONPOSAPPs.Where(x => x.CONSECUTIVO == enca.CONSECUTIVO).ToList();
                }
                catch (Exception f)
                {
                    return "No se encontro datos de configuracion para detalle contable";
                }
                try
                {
                    clien = db.CLIENTEs.Where(x => x.KUNNR == doc.PAYER_ID).Single();
                }
                catch (Exception g)
                {
                    return "No se encontro datos de cliente para detalle contable";
                }
                try
                {
                    cuent = db.CUENTAs.Where(x => x.PAIS_ID == doc.PAIS_ID && x.SOCIEDAD_ID == doc.SOCIEDAD_ID && x.GALL_ID == doc.GALL_ID).Single();
                }
                catch (Exception h)
                {
                    return "No se encontro datos de cuenta para detalle contable";
                }
                if (doc.PAIS_ID == "CO")
                {
                    tax = db.TAXEOHs.Where(x => x.PAIS_ID == doc.PAIS_ID && x.SOCIEDAD_ID == doc.SOCIEDAD_ID && x.KUNNR == clien.KUNNR && x.CONCEPTO_ID == doc.CONCEPTO_ID).Single();
                }
                for (int i = 0; i < conp.Count; i++)
                {
                    if (conp[i].POSICION == 1)
                    {
                        DetalleContab conta = new DetalleContab();
                        conta.POS_TYPE = conp[i].KEY;
                        conta.ACCOUNT = cuent.ABONO.ToString();
                        conta.BALANCE = doc.MONTO_DOC_MD.ToString();
                        conta.COMP_CODE = doc.SOCIEDAD_ID;
                        conta.BUS_AREA = conp[i].BUS_AREA;
                        conta.POST_KEY = conp[i].POSTING_KEY;
                        conta.TEXT = doc.CONCEPTO;
                        if (conp[i].POSTING_KEY == "11")
                        {
                            if (enca.TIPO_DOC == "BB")
                            {
                                conta.BALANCE = (doc.MONTO_DOC_MD + (doc.MONTO_DOC_MD * tax.PORC / 100)).ToString();
                                conta.REF_KEY1 = clien.STCD1;
                                conta.REF_KEY3 = clien.NAME1;
                                conta.TAX_CODE = conp[i].TAX_CODE;
                            }
                            else
                            {
                                conta.REF_KEY1 = conp[i].REF_KEY1;
                                conta.REF_KEY3 = conp[i].REF_KEY3;
                            }
                            conta.ACCOUNT = clien.PAYER;
                        }
                        if (conp[i].POSTING_KEY == "31")
                        {
                            conta.ACCOUNT = clien.PROVEEDOR_ID;
                            if (enca.TIPO_DOC == "KR" && doc.PAIS_ID == "CO")
                            {
                                conta.ASSIGNMENT = clien.PAYER;
                                conta.TAX_CODE = tax.IMPUESTO_ID;
                            }
                            if (enca.TIPO_DOC == "KG" && doc.PAIS_ID == "CO")
                            {
                                conta.BALANCE = (doc.MONTO_DOC_MD + (doc.MONTO_DOC_MD * tax.PORC / 100)).ToString();
                            }
                        }
                        contas.Add(conta);
                    }
                    else
                    {
                        List<DOCUMENTOP> docp = db.DOCUMENTOPs.Where(x => x.NUM_DOC == doc.NUM_DOC).ToList();

                        for (int j = 0; j < docp.Count; j++)
                        {
                            DetalleContab conta = new DetalleContab();
                            conta.POS_TYPE = conp[i].KEY;
                            conta.COMP_CODE = doc.SOCIEDAD_ID;
                            conta.BUS_AREA = conp[i].BUS_AREA;
                            conta.POST_KEY = conp[i].POSTING_KEY;
                            conta.TEXT = doc.CONCEPTO;
                            if (enca.TIPO_DOC != "RN" && doc.PAIS_ID != "CO")
                            {
                                conta.SALES_ORG = clien.VKORG;
                                conta.DIST_CHANEL = clien.VTWEG;
                                conta.DIVISION = clien.SPART;
                                conta.CUSTOMER = doc.PAYER_ID;
                                conta.PRODUCT = docp[j].MATNR;
                                if (conp[i].QUANTITY != null && conp[i].QUANTITY != 0)
                                {
                                    conta.QTY = conp[i].QUANTITY.ToString();
                                }
                                conta.AMOUNT_LC = conp[i].BASE_UNIT;
                                conta.ACCOUNT = cuent.CARGO.ToString();
                                conta.BALANCE = (docp[j].MONTO_APOYO * docp[j].VOLUMEN_EST).ToString();
                                if (enca.TIPO_DOC == "BB")
                                {
                                    conta.BALANCE = (docp[j].VOLUMEN_REAL * docp[j].MONTO_APOYO).ToString();
                                }
                                else
                                {
                                    conta.BALANCE = (docp[j].MONTO_APOYO * docp[j].VOLUMEN_EST).ToString();
                                }
                            }
                            else
                            {
                                conta.SALES_ORG =
                                conta.DIST_CHANEL =
                                conta.DIVISION =
                                conta.CUSTOMER =
                                conta.PRODUCT = "";
                                conta.ACCOUNT = cuent.ABONO.ToString();
                                conta.BALANCE = doc.MONTO_DOC_MD.ToString();
                                if (enca.TIPO_DOC == "BB" && doc.PAIS_ID == "CO")
                                {
                                    conta.SALES_ORG = clien.VKORG;
                                    conta.DIST_CHANEL = clien.VTWEG;
                                    conta.DIVISION = clien.SPART;
                                    conta.CUSTOMER = doc.PAYER_ID;
                                    conta.PRODUCT = docp[j].MATNR;
                                    conta.REF_KEY1 = clien.STCD1;
                                    conta.REF_KEY3 = clien.NAME1;
                                    conta.ACCOUNT = cuent.CARGO.ToString();
                                }
                                if (enca.TIPO_DOC != "KG" && doc.PAIS_ID == "CO")
                                {
                                    conta.BALANCE = (docp[j].MONTO_APOYO * docp[j].VOLUMEN_EST).ToString();
                                }
                                if (enca.TIPO_DOC == "KG" && doc.PAIS_ID == "CO")
                                {
                                    conta.BALANCE = (doc.MONTO_DOC_MD + (doc.MONTO_DOC_MD * tax.PORC / 100)).ToString();
                                }
                            }
                            if (enca.TIPO_DOC == "KR" && doc.PAIS_ID == "CO")
                            {
                                conta.TAX_CODE = tax.IMPUESTO_ID;
                                conta.ASSIGNMENT = clien.PAYER;
                            }
                            else
                            {
                                conta.TAX_CODE = conp[i].TAX_CODE;
                            }
                            contas.Add(conta);
                            if (enca.TIPO_DOC == "RN" || enca.TIPO_DOC == "KR" || enca.TIPO_DOC == "KG")
                            {
                                break;
                            }
                        }
                        if (enca.TIPO_DOC == "BB" && doc.PAIS_ID == "CO")
                        {
                            DetalleContab conta = new DetalleContab();
                            conta.POS_TYPE = conp[i].KEY;
                            conta.ACCOUNT = cuent.ABONO.ToString();
                            conta.BALANCE = (doc.MONTO_DOC_MD * tax.PORC / 100).ToString();
                            conta.COMP_CODE = doc.SOCIEDAD_ID;
                            conta.BUS_AREA = conp[i].BUS_AREA;
                            conta.POST_KEY = conp[i].POSTING_KEY;
                            conta.TEXT = doc.CONCEPTO;
                            conta.REF_KEY1 = clien.STCD1;
                            conta.REF_KEY3 = clien.NAME1;
                            contas.Add(conta);
                        }
                    }
                }
                return "";
            }
            catch (Exception e)
            {
                return "Error al obtener detalle contable";
            }
        }
    }
    public class DetalleContab
    {
        public string POS_TYPE;
        public string COMP_CODE;
        public string BUS_AREA;
        public string POST_KEY;
        public string ACCOUNT;
        public string COST_CENTER;
        public string BALANCE;
        public string TEXT;
        public string SALES_ORG;
        public string DIST_CHANEL;
        public string DIVISION;
        public string INV_REF;
        public string PAY_TERM;
        public string JURIS_CODE;
        public string CUSTOMER;
        public string PRODUCT;
        public string TAX_CODE;
        public string PLANT;
        public string REF_KEY1;
        public string REF_KEY3;
        public string ASSIGNMENT;
        public string QTY;
        public string BASE_UNIT;
        public string AMOUNT_LC;
    }

}