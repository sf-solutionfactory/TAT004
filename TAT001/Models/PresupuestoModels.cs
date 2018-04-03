using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using TAT001.Entities;
using System.Data.Entity;

namespace TAT001.Models
{
    public class PresupuestoModels
    {
        private TAT001Entities db = new TAT001Entities();
        public DatosPresupuesto consultSociedad(string sociedad)
        {
            DatosPresupuesto sociedades = new DatosPresupuesto();
            sociedades.sociedad = db.SOCIEDADs.Where(x => x.ACTIVO == true).ToList();
            if (String.IsNullOrEmpty(sociedad) == false)
            {
                sociedades.cambio = db.CSP_CAMBIO(sociedad).ToList();
            }
            return sociedades;
        }
        public DatosPresupuesto consultarDatos(string sociedad, string anio, string periodo, string cambio)
        {
            DatosPresupuesto sociedades = new DatosPresupuesto();
            string anioc = "", periodoc = "";
            sociedades = consultSociedad(sociedad);
            if (String.IsNullOrEmpty(anio) == false)
            {
                anioc = anio.Substring(2, 2);
            }
            if (String.IsNullOrEmpty(periodo) == false)
            {
                periodoc = mes(periodo);
            }
            if (String.IsNullOrEmpty(cambio) == false)
            {
                string[] moneda = cambio.Split('-');
                sociedades.presupuesto = db.CSP_CONSULTARPRESUPUESTO(sociedad, anioc, anio, periodoc, periodo, moneda[0], moneda[1]).ToList();
            }
            else
            {
                sociedades.presupuesto = db.CSP_CONSULTARPRESUPUESTO(sociedad, anioc, anio, periodoc, periodo, "", "").ToList();
            }
            sociedades.cambio = db.CSP_CAMBIO(sociedad).ToList();
            return sociedades;
        }
        public string consultarUCarga()
        {
            return db.PRESUPUESTOHs.Max(x => x.FECHAC).ToString();
        }
        public string consultaAnio()
        {
            return db.PRESUPUESTOPs.Min(x => x.ANIO);
        }
        private string mes(string mes)
        {
            string ms = "";
            switch (mes)
            {
                case "1":
                    ms = "Jan";
                    break;
                case "2":
                    ms = "Feb";
                    break;
                case "3":
                    ms = "Mar";
                    break;
                case "4":
                    ms = "Apr";
                    break;
                case "5":
                    ms = "May";
                    break;
                case "6":
                    ms = "Jun";
                    break;
                case "7":
                    ms = "Jul";
                    break;
                case "8":
                    ms = "Aug";
                    break;
                case "9":
                    ms = "Sep";
                    break;
                case "10":
                    ms = "Oct";
                    break;
                case "11":
                    ms = "Nov";
                    break;
                case "12":
                    ms = "Dec";
                    break;
            }
            return ms;
        }
    }
}