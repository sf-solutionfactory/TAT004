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
    
    public partial class ESTATUST
    {
        public string SPRAS_ID { get; set; }
        public int ESTATUS_ID { get; set; }
        public string TXT050 { get; set; }
    
        public virtual ESTATU ESTATU { get; set; }
        public virtual SPRA SPRA { get; set; }
    }
}