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
            FLUJO actual = new FLUJO();
            if (f.ESTATUS.Equals("I"))//--------Nuevo registro
            {
                actual.COMENTARIO = f.COMENTARIO;
                actual.ESTATUS = f.ESTATUS;
                actual.FECHAC = f.FECHAC;
                actual.FECHAM = f.FECHAM;
                actual.LOOP = f.LOOP;
                actual.NUM_DOC = f.NUM_DOC;
                actual.POS = f.POS;
                actual.USUARIOA_ID = f.USUARIOA_ID;
                actual.USUARIOD_ID = f.USUARIOD_ID;
                actual.WF_POS = f.WF_POS;
                actual.WF_VERSION = f.WF_VERSION;
                actual.WORKF_ID = f.WORKF_ID;

                f.ESTATUS = "A";
            }
            else
            {
                actual = db.FLUJOes.Where(a => a.NUM_DOC.Equals(f.NUM_DOC)).OrderByDescending(a => a.POS).FirstOrDefault();
            }
            if (actual.ESTATUS.Equals("A"))
                return 1;//Ya fue procesada
            else
            {
                if (actual.ESTATUS.Equals("I"))
                {
                    actual.ESTATUS = f.ESTATUS;
                    db.FLUJOes.Add(actual);
                    db.SaveChanges();
                }
                else
                {
                    var wf = actual.WORKFP;
                    actual.ESTATUS = f.ESTATUS;
                    actual.FECHAM = f.FECHAM;
                    actual.COMENTARIO = f.COMENTARIO;

                    db.Entry(actual).State = EntityState.Modified;
                }

                WORKFP paso_a = db.WORKFPs.Where(a => a.ID.Equals(actual.WORKF_ID) & a.VERSION.Equals(actual.WF_VERSION) & a.POS.Equals(actual.WF_POS)).FirstOrDefault();

                bool ban = true;
                while (ban)
                {
                    int next_step_a = 0;
                    int next_step_r = 0;
                    if (paso_a.NEXT_STEP != null)
                        next_step_a = (int)paso_a.NEXT_STEP;
                    if (paso_a.NS_ACCEPT != null)
                        next_step_a = (int)paso_a.NS_ACCEPT;
                    if (paso_a.NS_REJECT != null)
                        next_step_r = (int)paso_a.NS_REJECT;

                    WORKFP next = new WORKFP();

                    if (f.ESTATUS.Equals("A") | f.ESTATUS.Equals("N"))//Proceso aprobado
                    {
                        next = db.WORKFPs.Where(a => a.ID.Equals(actual.WORKF_ID) & a.VERSION.Equals(actual.WF_VERSION) & a.POS == next_step_a).FirstOrDefault();
                        if (next.NEXT_STEP.Equals(99))//--------FIN DEL WORKFLOW
                        {
                            DOCUMENTO d = db.DOCUMENTOes.Find(actual.NUM_DOC);
                            d.ESTATUS_WF = "A";
                            if (paso_a.EMAIL.Equals("X"))
                                correcto = 2;
                        }
                        else
                        {
                            DOCUMENTO d = db.DOCUMENTOes.Find(actual.NUM_DOC);
                            FLUJO nuevo = new FLUJO();
                            nuevo.WORKF_ID = next.ID;
                            nuevo.WF_VERSION = next.VERSION;
                            nuevo.WF_POS = next.POS;
                            nuevo.NUM_DOC = actual.NUM_DOC;
                            nuevo.POS = actual.POS + 1;
                            /*nuevo.LOOP = 1;*///-----------------------------------
                            int loop = db.FLUJOes.Where(a => a.WORKF_ID.Equals(next.ID) & a.WF_VERSION.Equals(next.VERSION) & a.WF_POS == next.POS & a.NUM_DOC.Equals(actual.NUM_DOC) & a.ESTATUS.Equals("A")).Count();
                            if (loop >= next.LOOPS)
                            {
                                paso_a = next;
                                continue;
                            }
                            if (loop != 0)
                                nuevo.LOOP = loop + 1;
                            else
                                nuevo.LOOP = 1;

                            if (next.AGENTE_ID == 1)
                                nuevo.USUARIOA_ID = d.USUARIO.ID;
                            if (next.AGENTE_ID == 2)
                                nuevo.USUARIOA_ID = d.USUARIO.MANAGER;
                            if (next.AGENTE_ID == 3)
                                nuevo.USUARIOA_ID = d.USUARIO.BACKUP_ID;
                            //nuevo.USUARIOD_ID
                            nuevo.ESTATUS = "P";
                            nuevo.FECHAC = DateTime.Now;
                            nuevo.FECHAM = DateTime.Now;

                            db.FLUJOes.Add(nuevo);
                            if (paso_a.EMAIL.Equals("X"))
                                correcto = 1;
                        }
                        db.SaveChanges();
                        ban = false;
                    }
                    else if (f.ESTATUS.Equals("R"))//Rechazada
                    {
                        next = db.WORKFPs.Where(a => a.ID.Equals(actual.WORKF_ID) & a.VERSION.Equals(actual.WF_VERSION) & a.POS == next_step_r).FirstOrDefault();

                        correcto = 3;
                        actual.ESTATUS = f.ESTATUS;
                        actual.FECHAM = f.FECHAM;
                        actual.COMENTARIO = f.COMENTARIO;

                        FLUJO nuevo = new FLUJO();
                        nuevo.WORKF_ID = next.ID;
                        nuevo.WF_VERSION = next.VERSION;
                        nuevo.WF_POS = next.POS;
                        nuevo.NUM_DOC = actual.NUM_DOC;
                        nuevo.POS = actual.POS + 1;
                        /*nuevo.LOOP = 1;*///-----------------------------------
                        int loop = db.FLUJOes.Where(a => a.WORKF_ID.Equals(next.ID) & a.WF_VERSION.Equals(next.VERSION) & a.WF_POS == next.POS & a.NUM_DOC.Equals(actual.NUM_DOC) & a.ESTATUS.Equals("A")).Count();
                        if (loop != 0)
                            nuevo.LOOP = loop + 1;
                        else
                            nuevo.LOOP = 1;
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
                        ban = false;

                    }
                }
            }
            return correcto;
        }
    }
}