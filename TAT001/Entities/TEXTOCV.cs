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
    
    public partial class TEXTOCV
    {
        public int ID { get; set; }
        public string SPRAS_ID { get; set; }
        public string CAMPO { get; set; }
        public string TEXTO { get; set; }
    
        public virtual SPRA SPRA { get; set; }
    }
}
