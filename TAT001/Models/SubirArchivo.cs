using TAT001.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace TAT001.Models
{
    public class SubirArchivo
    {
        public string subir(HttpPostedFileBase fileCPT, string rut)
        {
            string ms = "";
            try
            {
                fileCPT.SaveAs(rut);
                ms = "salvado";
            }
            catch (Exception e)
            {
                ms = e.Message;
            }
            return ms;
        }
        public string generarArchivo(decimal a)
        {
            TAT001Entities db = new TAT001Entities();
            //decimal a = 1000000207;
            DOCUMENTO doc = db.DOCUMENTOes.Where(x => x.NUM_DOC == a).Single();
            CONPOSAPH tab = db.CONPOSAPHs.Where(x => x.TIPO_SOL == doc.TSOL_ID
            && x.SOCIEDAD == doc.SOCIEDAD_ID
            && x.FECHA_FINVIG >= doc.FECHAF_VIG
            ).Single();
            string conf = "NUM_DOC+TALL_ID";//Tall:_ID num_doc
            string txt = "";
            string[] cc;
            var ppd = doc.GetType().GetProperties();
            if (String.IsNullOrEmpty(conf) == false)
            {
                cc = conf.Split('+');
                foreach (string c in cc)
                {
                    txt += ppd.Where(x => x.Name == c).Single().GetValue(doc).ToString();
                }
            }
            tab.REFERENCIA = txt;
            txt = "";
            conf = "TALL_ID+NUM_DOC";
            if (String.IsNullOrEmpty(conf) == false)
            {
                cc = conf.Split('+');
                foreach (string c in cc)
                {
                    txt += ppd.Where(x => x.Name == c).Single().GetValue(doc).ToString();
                }
            }
            tab.HEADER_TEXT = txt;
            if (String.IsNullOrEmpty(tab.MONEDA))
            {
                doc.MONEDA_ID = "";
            }
            if (tab.FECHA_DOCU != "D")
            {
                doc.FECHAD = fecha(tab.FECHA_DOCU);
            }
            if (tab.FECHA_CONTAB != "D")
            {
                doc.FECHAC = fecha(tab.FECHA_CONTAB);
            }
            List<DetalleContab> det = detalle(doc);
            string url = ConfigurationManager.AppSettings["URL_SAVE"] + @"POSTING\INBOUND_"+ a.ToString()+".txt";
            
            using (StreamWriter sw = new StreamWriter(url))
            {
                CONPOSAPH dir = tab;
                sw.WriteLine(dir.CONSECUTIVO + "|" + dir.TIPO_SOL + "|" + dir.TIPO_DOC + "|" + dir.SOCIEDAD + "|" + String.Format("{0:dd.MM.yyyy}", doc.FECHAC) + "|" + String.Format("{0:dd.MM.yyyy}", doc.FECHAD) + "|" + doc.MONEDA_ID + "|" + dir.HEADER_TEXT + "|" + String.Format("{0:dd.MM.yyyy}", dir.FECHA_INIVIG) + "|" + String.Format("{0:dd.MM.yyyy}", dir.FECHA_FINVIG) + "|" + dir.REFERENCIA + "|" + dir.PAIS + "|" + dir.NOTA + "|" + dir.CORRESPONDENCIA + "|" + dir.CALC_TAXT);
                sw.WriteLine("");
                sw.WriteLine("");
                for (int i = 0; i < det.Count; i++)
                {
                    sw.WriteLine(
                        det[i].G           + "|" +
                        det[i].COMP_CODE   + "|" +
                        det[i].BUS_AREA    + "|" +
                        det[i].POST_KEY    + "|" +
                        det[i].ACCOUNT     + "|" +
                        det[i].COST_CENTER + "|" +
                        det[i].BALANCE     + "|" +
                        det[i].TEXT        + "|" +
                        det[i].SALES_ORG   + "|" +
                        det[i].DIST_CHANEL + "|" +
                        det[i].DIVISION    + "|" +
                        det[i].SALES_OFF   + "|" +
                        det[i].SALES_GR    + "|" +
                        det[i].PRICE_GR    + "|" +
                        det[i].CORP_CAT    + "|" +
                        det[i].CORP_BRAND  + "|" +
                        det[i].INV_REF     + "|" +
                        det[i].PAY_TERM    + "|" +
                        det[i].JURIS_CODE  + "|" +
                        det[i].SALES_DIST  + "|" +
                        det[i].CUSTOMER    + "|" +
                        det[i].PRODUCT     + "|" +
                        det[i].TAX_CODE    + "|" +
                        det[i].PLANT       + "|" +
                        det[i].REF_KEY1    + "|" +
                        det[i].REF_KEY3    + "|" +
                        det[i].ASSIGNMENT  + "|" +
                        det[i].QTY         + "|" +
                        det[i].BASE_UNIT   + "|" +
                        det[i].AMOUNT_LC   
                        );     
                }              
                sw.Close();
            }
            return "";
        }
        private DateTime fecha(string id_fecha)
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
        private List<DetalleContab> detalle(DOCUMENTO doc)
        {            
            List<DetalleContab> contas = new List<DetalleContab>();
            TAT001Entities db = new TAT001Entities();
            List<CONPOSAPP> conp = db.CONPOSAPPs.Where(x=>x.CONSECUTIVO == doc.NUM_DOC).ToList();
            CLIENTE clien = db.CLIENTEs.Where(x => x.KUNNR == doc.PAYER_ID).Single();
            CUENTA cuent = db.CUENTAs.Where(x => x.PAIS_ID == doc.PAIS_ID && x.SOCIEDAD_ID == doc.SOCIEDAD_ID && x.GALL_ID == doc.GALL_ID).Single();
            for (int i = 0; i < conp.Count; i++)
            {
                if (conp[i].POSTING_KEY == "50")
                {
                    DetalleContab conta = new DetalleContab();                    
                    conta.G = "G";
                    conta.ACCOUNT = cuent.ABONO.ToString();
                    conta.BALANCE = doc.MONTO_DOC_MD.ToString();
                    conta.COMP_CODE = doc.SOCIEDAD_ID;
                    conta.BUS_AREA = "KLA";
                    conta.POST_KEY = "50";
                    conta.TEXT = doc.CONCEPTO;
                    contas.Add(conta);
                }
                else
                {
                    List<DOCUMENTOP> docp = db.DOCUMENTOPs.Where(x => x.NUM_DOC == doc.NUM_DOC).ToList();
                    if (cuent.CARGO_FV != null)
                    {
                        if (cuent.LIMITE < cuent.ABONO)
                        {
                            decimal total = Convert.ToDecimal(cuent.ABONO) - Convert.ToDecimal(cuent.LIMITE);
                            DetalleContab conta = new DetalleContab();
                            conta.G = "G";
                            conta.COMP_CODE = doc.SOCIEDAD_ID;
                            conta.BUS_AREA = "KLA";
                            conta.POST_KEY = "40";
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
                            conta2.G = "G";
                            conta2.COMP_CODE = doc.SOCIEDAD_ID;
                            conta2.BUS_AREA = "KLA";
                            conta2.POST_KEY = "40";
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
                            conta.G = "G";
                            conta.COMP_CODE = doc.SOCIEDAD_ID;
                            conta.BUS_AREA = "KLA";
                            conta.POST_KEY = "40";
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
                            conta.G = "G";
                            conta.COMP_CODE = doc.SOCIEDAD_ID;
                            conta.BUS_AREA = "KLA";
                            conta.POST_KEY = "40";
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
            return contas;
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