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
    
    public partial class TSOPORTET
    {
        public string SPRAS_ID { get; set; }
        public string TSOPORTE_ID { get; set; }
        public string TXT50 { get; set; }
    
        public virtual SPRA SPRA { get; set; }
        public virtual TSOPORTE TSOPORTE { get; set; }
    }
}
