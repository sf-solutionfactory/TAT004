using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAT001.Entities;

namespace TAT001.Services
{
    public class ProcesaFlujo
    {
        public int procesa(decimal id, string accion)
        {
            int correcto = 0;
            TAT001Entities db = new TAT001Entities();
            FLUJO actual = db.FLUJOes.Where(a => a.NUM_DOC.Equals(id)).OrderByDescending(a => a.POS).FirstOrDefault();
            if (!actual.ESTATUS.Equals("P"))
                return 1;//Ya fue procesada
            else
            {
                var wf = actual.WORKFP;
                if (accion.Equals("A"))//Proceso aprobado
                {
                    actual.ESTATUS = accion;
                }
                else if (accion.Equals("R"))//Rechazada
                {
                    actual.ESTATUS = accion;
                }
            }
            return correcto;
        }
    }
}