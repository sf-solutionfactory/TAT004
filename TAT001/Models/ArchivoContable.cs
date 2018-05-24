using TAT001.Entities;
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
        public string generarArchivo(decimal docum)
        {
            TAT001Entities db = new TAT001Entities();
            try
            {
                DOCUMENTO doc = db.DOCUMENTOes.Where(x => x.NUM_DOC == docum).Single();
                CONPOSAPH tab = db.CONPOSAPHs.Where(x => x.TIPO_SOL == doc.TSOL_ID
                && x.SOCIEDAD == doc.SOCIEDAD_ID
                && x.FECHA_FINVIG >= doc.FECHAF_VIG
                ).Single();
                doc.GALL_ID = db.GALLs.Where(x => x.ID == doc.GALL_ID).Select(x => x.GRUPO_ALL).Single();
                string txt = "";
                string msj = "";
                string[] cc;
                var ppd = doc.GetType().GetProperties();
                if (String.IsNullOrEmpty(tab.HEADER_TEXT) == false)
                {
                    cc = tab.HEADER_TEXT.Trim().Split('+');
                    foreach (string c in cc)
                    {
                        txt += ppd.Where(x => x.Name == c).Single().GetValue(doc).ToString();
                    }
                }
                else
                {
                    return "Agrege comando para generar texto de encabezado";
                }
                tab.HEADER_TEXT = txt;
                txt = "";
                if (String.IsNullOrEmpty(tab.REFERENCIA) == false)
                {
                    cc = tab.REFERENCIA.Trim().Split('+');
                    foreach (string c in cc)
                    {
                        txt += ppd.Where(x => x.Name == c).Single().GetValue(doc).ToString();
                    }
                }
                else
                {
                    return "Agrege comando para generar referencia";
                }
                tab.REFERENCIA = txt;
                if (String.IsNullOrEmpty(tab.MONEDA))
                {
                    doc.MONEDA_ID = "";
                }
                if (tab.FECHA_DOCU != "D")
                {
                    doc.FECHAD = Fecha(tab.FECHA_DOCU);
                }
                //if (tab.FECHA_CONTAB != "D")
                //{
                doc.FECHAC = Fecha(tab.FECHA_CONTAB);
                //}
                List<DetalleContab> det = new List<DetalleContab>();
                msj = Detalle(doc, ref det, tab);
                if (msj != "1")
                {
                    return msj;
                }
                string url = ConfigurationManager.AppSettings["URL_SAVE"] + @"POSTING\INBOUND_" + docum.ToString() + ".txt";

                using (StreamWriter sw = new StreamWriter(url))
                {
                    CONPOSAPH dir = tab;
                    sw.WriteLine(tab.TIPO_DOC + "|" +
                        dir.SOCIEDAD.Trim() + "|" + String.Format("{0:MM.dd.yyyy}", doc.FECHAC).Replace(".", "") //+ "|"
                                                                                                                 //+ String.Format("{0:dd.MM.yyyy}", doc.FECHAD) 
                        + "|" + doc.MONEDA_ID.Trim() + "|" + dir.HEADER_TEXT.Trim() + "|"
                        //+ String.Format("{0:dd.MM.yyyy}", dir.FECHA_INIVIG) + "|" + String.Format("{0:dd.MM.yyyy}", dir.FECHA_FINVIG) + "|" 
                        + dir.REFERENCIA.Trim() + "|" + dir.PAIS.Trim() + "|" + dir.NOTA.Trim() + "|" + dir.CORRESPONDENCIA.Trim() + "|" + dir.CALC_TAXT.Trim() + "|");
                    sw.WriteLine("");
                    for (int i = 0; i < det.Count; i++)
                    {
                        sw.WriteLine(
                            det[i].G + "|" +
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
                            det[i].SALES_OFF + "|" +
                            det[i].SALES_GR + "|" +
                            det[i].PRICE_GR + "|" +
                            det[i].CORP_CAT + "|" +
                            det[i].CORP_BRAND + "|" +
                            det[i].INV_REF + "|" +
                            det[i].PAY_TERM + "|" +
                            det[i].JURIS_CODE + "|" +
                            det[i].SALES_DIST + "|" +
                            det[i].CUSTOMER + "|" +
                            det[i].PRODUCT + "|" +
                            det[i].TAX_CODE + "|" +
                            det[i].PLANT + "|" +
                            det[i].REF_KEY1 + "|" +
                            det[i].REF_KEY3 + "|" +
                            det[i].ASSIGNMENT + "|" +
                            det[i].QTY + "|" +
                            det[i].BASE_UNIT + "|" +
                            det[i].AMOUNT_LC
                            );
                    }
                    sw.Close();
                }
                return "correcto";
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
                for (int i = 0; i < conp.Count; i++)
                {
                    if (conp[i].POSICION == 1)
                    {
                        DetalleContab conta = new DetalleContab();
                        //conta.G = enca.CLASE1;
                        conta.ACCOUNT = cuent.ABONO.ToString();
                        conta.BALANCE = doc.MONTO_DOC_MD.ToString();
                        conta.COMP_CODE = doc.SOCIEDAD_ID;
                        //conta.BUS_AREA = enca.CLASE2;
                        conta.POST_KEY = conp[i].POSTING_KEY;
                        conta.TEXT = doc.CONCEPTO;
                        contas.Add(conta);
                    }
                    else
                    {
                        List<DOCUMENTOP> docp = db.DOCUMENTOPs.Where(x => x.NUM_DOC == doc.NUM_DOC).ToList();
                        if (cuent.CARGO != null)
                        {
                            if (cuent.LIMITE < cuent.ABONO)
                            {
                                decimal total = Convert.ToDecimal(cuent.ABONO) - Convert.ToDecimal(cuent.LIMITE);
                                DetalleContab conta = new DetalleContab();
                                //conta.G = enca.CLASE1;
                                conta.COMP_CODE = doc.SOCIEDAD_ID;
                                //conta.G = enca.CLASE2;
                                conta.POST_KEY = conp[i].POSTING_KEY;
                                conta.ACCOUNT = cuent.CARGO.ToString();
                                conta.BALANCE = cuent.LIMITE.ToString();
                                conta.TEXT = doc.CONCEPTO;
                                conta.SALES_ORG = clien.VKORG;
                                conta.DIST_CHANEL = clien.VTWEG;
                                conta.DIVISION = clien.SPART;
                                conta.SALES_OFF = clien.VKBUR;
                                conta.SALES_GR = clien.VKGRP;
                                conta.PRICE_GR = clien.KONDA;
                                conta.SALES_DIST = clien.BZIRK;
                                conta.CUSTOMER = doc.PAYER_ID;
                                contas.Add(conta);
                                DetalleContab conta2 = new DetalleContab();
                                //conta.G = enca.CLASE1;
                                conta2.COMP_CODE = doc.SOCIEDAD_ID;
                                //conta.G = enca.CLASE2;
                                conta.POST_KEY = conp[i].POSTING_KEY;
                                conta2.ACCOUNT = cuent.CARGO.ToString();
                                conta2.BALANCE = cuent.LIMITE.ToString();
                                conta2.TEXT = doc.CONCEPTO;
                                conta2.SALES_ORG = clien.VKORG;
                                conta2.DIST_CHANEL = clien.VTWEG;
                                conta2.DIVISION = clien.SPART;
                                conta2.SALES_OFF = clien.VKBUR;
                                conta2.SALES_GR = clien.VKGRP;
                                conta2.PRICE_GR = clien.KONDA;
                                conta2.SALES_DIST = clien.BZIRK;
                                conta2.CUSTOMER = doc.PAYER_ID;
                                contas.Add(conta2);
                            }
                            else
                            {
                                DetalleContab conta = new DetalleContab();
                                //conta.G = enca.CLASE1;
                                conta.COMP_CODE = doc.SOCIEDAD_ID;
                               // conta.G = enca.CLASE2;
                                conta.POST_KEY = conp[i].POSTING_KEY;
                                conta.ACCOUNT = cuent.CARGO.ToString();
                                conta.BALANCE = cuent.ABONO.ToString();
                                conta.TEXT = doc.CONCEPTO;
                                conta.SALES_ORG = clien.VKORG;
                                conta.DIST_CHANEL = clien.VTWEG;
                                conta.DIVISION = clien.SPART;
                                conta.SALES_OFF = clien.VKBUR;
                                conta.SALES_GR = clien.VKGRP;
                                conta.PRICE_GR = clien.KONDA;
                                conta.SALES_DIST = clien.BZIRK;
                                conta.CUSTOMER = doc.PAYER_ID;
                                contas.Add(conta);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < docp.Count; j++)
                            {
                                DetalleContab conta = new DetalleContab();
                                //conta.G = enca.CLASE1;
                                conta.COMP_CODE = doc.SOCIEDAD_ID;
                                //conta.G = enca.CLASE2;
                                conta.POST_KEY = conp[i].POSTING_KEY;
                                conta.ACCOUNT = cuent.CARGO.ToString();
                                conta.BALANCE = (docp[j].MONTO_APOYO * docp[j].VOLUMEN_EST).ToString();
                                conta.TEXT = doc.CONCEPTO;
                                conta.SALES_ORG = clien.VKORG;
                                conta.DIST_CHANEL = clien.VTWEG;
                                conta.DIVISION = clien.SPART;
                                conta.SALES_OFF = clien.VKBUR;
                                conta.SALES_GR = clien.VKGRP;
                                conta.PRICE_GR = clien.KONDA;
                                conta.SALES_DIST = clien.BZIRK;
                                conta.CUSTOMER = doc.PAYER_ID;
                                conta.PRODUCT = docp[j].MATNR;
                                contas.Add(conta);
                            }
                        }
                    }
                }
                return "1";
            }
            catch (Exception e)
            {
                return "Error al obtener detalle contable";
            }
        }
    }
    public class DetalleContab
    {
        public string G;
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
        public string SALES_OFF;
        public string SALES_GR;
        public string PRICE_GR;
        public string CORP_CAT;
        public string CORP_BRAND;
        public string INV_REF;
        public string PAY_TERM;
        public string JURIS_CODE;
        public string SALES_DIST;
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