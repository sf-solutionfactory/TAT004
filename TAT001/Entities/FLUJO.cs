//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TAT001.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class FLUJO
    {
        public string WORKF_ID { get; set; }
        public int WF_VERSION { get; set; }
        public int WF_POS { get; set; }
        public decimal NUM_DOC { get; set; }
        public int POS { get; set; }
        public int DETPOS { get; set; }
        public Nullable<int> DETVER { get; set; }
        public Nullable<int> LOOP { get; set; }
        public string USUARIOA_ID { get; set; }
        public string USUARIOD_ID { get; set; }
        public string ESTATUS { get; set; }
        public Nullable<System.DateTime> FECHAC { get; set; }
        public Nullable<System.DateTime> FECHAM { get; set; }
        public string COMENTARIO { get; set; }
    
        public virtual DOCUMENTO DOCUMENTO { get; set; }
        public virtual USUARIO USUARIO { get; set; }
        public virtual USUARIO USUARIO1 { get; set; }
        public virtual WORKFP WORKFP { get; set; }
    }
}
