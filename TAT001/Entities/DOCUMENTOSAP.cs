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
    
    public partial class DOCUMENTOSAP
    {
        public decimal EJER_DOC { get; set; }
        public decimal NUM_DOC { get; set; }
        public string BUKRS { get; set; }
        public Nullable<int> EJERCICIO { get; set; }
        public string CUENTA_C { get; set; }
        public string CUENTA_A { get; set; }
        public string REGISTRO_PR { get; set; }
        public string REGISTRO_NO { get; set; }
        public string REGISTRO_RE { get; set; }
        public string REGISTRO_AP { get; set; }
        public string BLART { get; set; }
        public string LIFNR { get; set; }
        public string KUNNR { get; set; }
        public Nullable<decimal> IMPORTE { get; set; }
    
        public virtual DOCUMENTO DOCUMENTO { get; set; }
    }
}
