//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TAT001.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class DET_APROBP
    {
        public string SOCIEDAD_ID { get; set; }
        public int PUESTOC_ID { get; set; }
        public int VERSION { get; set; }
        public int POS { get; set; }
        public Nullable<int> PUESTOA_ID { get; set; }
        public Nullable<decimal> MONTO { get; set; }
        public Nullable<bool> PRESUPUESTO { get; set; }
        public bool ACTIVO { get; set; }
    
        public virtual DET_APROBH DET_APROBH { get; set; }
    }
}
