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
    
    public partial class MATERIAL
    {
        public string ID { get; set; }
        public string MTART { get; set; }
        public string MATKL_ID { get; set; }
        public string MAKTX { get; set; }
        public string MAKTG { get; set; }
        public string MEINS { get; set; }
        public Nullable<decimal> PUNIT { get; set; }
        public Nullable<bool> ACTIVO { get; set; }
        public string CTGR { get; set; }
        public string BRAND { get; set; }
        public string MATERIALGP_ID { get; set; }
    
        public virtual ZCTGR ZCTGR { get; set; }
        public virtual ZBRAND ZBRAND { get; set; }
        public virtual MATERIALGP MATERIALGP { get; set; }
        public virtual UMEDIDA UMEDIDA { get; set; }
    }
}
