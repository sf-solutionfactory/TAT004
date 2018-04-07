using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TAT001.Entities;

namespace TAT001.Services
{
    public class ProcesaFlujo
    {
        public int procesa(FLUJO f)
        {
            int correcto = 0;
            TAT001Entities db = new TAT001Entities();
            FLUJO actual = db.FLUJOes.Where(a => a.NUM_DOC.Equals(f.NUM_DOC)).OrderByDescending(a => a.POS).FirstOrDefault();
            if (!actual.ESTATUS.Equals("P"))
                return 1;//Ya fue procesada
            else
            {
                var wf = actual.WORKFP;
                actual.ESTATUS = f.ESTATUS;
                actual.FECHAM = f.FECHAM;
                actual.COMENTARIO = f.COMENTARIO;

                db.Entry(actual).State = EntityState.Modified;
                //db.SaveChanges();

                WORKFP paso_a = db.WORKFPs.Where(a => a.ID.Equals(actual.WORKF_ID) & a.VERSION.Equals(actual.WF_VERSION) & a.POS.Equals(actual.WF_POS)).FirstOrDefault();
                int next_step_a = 0;
                int next_step_r = 0;
                if (paso_a.NEXT_STEP != null)
                    next_step_a = (int)paso_a.NEXT_STEP;
                if (paso_a.NS_ACCEPT != null)
                    next_step_a = (int)paso_a.NS_ACCEPT;
                if (paso_a.NS_ACCEPT != null)
                    next_step_r = (int)paso_a.NS_REJECT;

                if (f.ESTATUS.Equals("A"))//Proceso aprobado
                {
                    WORKFP next = db.WORKFPs.Where(a => a.ID.Equals(actual.WORKF_ID) & a.VERSION.Equals(actual.WF_VERSION) & a.POS == next_step_a).FirstOrDefault();

                    if (next.NEXT_STEP.Equals(99))//--------FIN DEL WORKFLOW
                    {
                        DOCUMENTO d = db.DOCUMENTOes.Find(actual.NUM_DOC);
                        d.ESTATUS_WF = "A";
                        if (paso_a.EMAIL.Equals("X"))
                            correcto = 2;
                    }
                    else
                    {
                        FLUJO nuevo = new FLUJO();
                        nuevo.WORKF_ID = next.ID;
                        nuevo.WF_VERSION = next.VERSION;
                        nuevo.WF_POS = next.POS;
                        nuevo.NUM_DOC = actual.NUM_DOC;
                        nuevo.POS = actual.POS + 1;
                        nuevo.LOOP = 1;//-----------------------------------
                        nuevo.USUARIOA_ID = actual.USUARIO.MANAGER;
                        //nuevo.USUARIOD_ID
                        nuevo.ESTATUS = "P";
                        nuevo.FECHAC = DateTime.Now;
                        nuevo.FECHAM = DateTime.Now;

                        db.FLUJOes.Add(nuevo);
                        if (paso_a.EMAIL.Equals("X"))
                            correcto = 1;
                    }
                    db.SaveChanges();
                }
                else if (f.ESTATUS.Equals("R"))//Rechazada
                {
                    correcto = 3;
                    actual.ESTATUS = f.ESTATUS;
                    actual.FECHAM = f.FECHAM;
                    actual.COMENTARIO = f.COMENTARIO;

                    FLUJO nuevo = new FLUJO();
                    WORKFP next = db.WORKFPs.Where(a => a.ID.Equals(actual.WORKF_ID) & a.VERSION.Equals(actual.WF_VERSION) & a.POS == next_step_r).FirstOrDefault();
                    nuevo.WORKF_ID = next.ID;
                    nuevo.WF_VERSION = next.VERSION;
                    nuevo.WF_POS = next.POS;
                    nuevo.NUM_DOC = actual.NUM_DOC;
                    nuevo.POS = actual.POS + 1;
                    nuevo.LOOP = 2;//-----------------------------------
                    DOCUMENTO d = db.DOCUMENTOes.Find(actual.NUM_DOC);
                    nuevo.USUARIOA_ID = d.USUARIOC_ID;
                    //nuevo.USUARIOD_ID
                    nuevo.ESTATUS = "P";
                    nuevo.FECHAC = DateTime.Now;
                    nuevo.FECHAM = DateTime.Now;

                    db.FLUJOes.Add(nuevo);

                    db.Entry(actual).State = EntityState.Modified;

                    //DOCUMENTO d = db.DOCUMENTOes.Find(actual.NUM_DOC);
                    d.ESTATUS_WF = "R";

                    db.SaveChanges();
                }
            }
            return correcto;
        }
    }
}