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
    
    public partial class WARNING
    {
        public int PAGINA_ID { get; set; }
        public string CAMPO_ID { get; set; }
        public string SPRAS_ID { get; set; }
        public string WARNING1 { get; set; }
        public string POSICION { get; set; }
        public Nullable<bool> ACTIVO { get; set; }
    
        public virtual CAMPOS CAMPOS { get; set; }
        public virtual POSICION POSICION1 { get; set; }
        public virtual SPRA SPRA { get; set; }
    }
}
