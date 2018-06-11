using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAT001.Entities;

namespace TAT001.Services
{
    public class Calendario445
    {
        public int getPeriodo(DateTime fecha)
        {
            TAT001Entities db = new TAT001Entities();
            int periodo = 0;
            List<PERIODO445> pp = db.PERIODO445.Where(a => a.EJERCICIO == fecha.Year).ToList();
            PERIODO445 p = pp.Where(a => a.MES_NATURAL == fecha.Month).FirstOrDefault();
            if (fecha.Day > p.DIA_NATURAL)
            {
                periodo = p.PERIODO + 1;
            }
            else
            {
                periodo = p.PERIODO;
            }

            return periodo;
        }
        public DateTime getPrimerDia(int ejercicio, int periodo)
        {
            TAT001Entities db = new TAT001Entities();
            DateTime dia = new DateTime();
            List<PERIODO445> pp = db.PERIODO445.Where(a => a.EJERCICIO == ejercicio).ToList();
            PERIODO445 p = pp.Where(a => a.MES_NATURAL == periodo - 1).FirstOrDefault();
            if (p == null)
            {
                dia = new DateTime(ejercicio, 1, 1);
            }
            else
            {
                dia = new DateTime(ejercicio, p.PERIODO, p.DIA_NATURAL);
                dia = dia.AddDays(1);
            }

            return dia;
        }

        public DateTime getUltimoDia(int ejercicio, int periodo)
        {
            TAT001Entities db = new TAT001Entities();
            DateTime dia = new DateTime();
            List<PERIODO445> pp = db.PERIODO445.Where(a => a.EJERCICIO == ejercicio).ToList();
            PERIODO445 p = pp.Where(a => a.MES_NATURAL == periodo).FirstOrDefault();
            if (p == null)
            {
                dia = new DateTime(ejercicio, 1, 1);
            }
            else
            {
                dia = new DateTime(ejercicio, p.PERIODO, p.DIA_NATURAL);
            }

            return dia;
        }
    }
}