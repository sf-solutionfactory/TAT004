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
    
    public partial class DOCUMENTON
    {
        public decimal NUM_DOC { get; set; }
        public int POS { get; set; }
        public Nullable<int> STEP { get; set; }
        public string USUARIO_ID { get; set; }
        public string TEXTO { get; set; }
    
        public virtual DOCUMENTO DOCUMENTO { get; set; }
    }
}
