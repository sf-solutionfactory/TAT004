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
    
    public partial class DOCUMENTOREC
    {
        public decimal NUM_DOC { get; set; }
        public int POS { get; set; }
        public Nullable<System.DateTime> FECHAF { get; set; }
        public Nullable<int> PERIODO { get; set; }
        public Nullable<int> EJERCICIO { get; set; }
        public Nullable<decimal> MONTO_BASE { get; set; }
        public Nullable<decimal> MONTO_FIJO { get; set; }
        public Nullable<decimal> MONTO_GRS { get; set; }
        public Nullable<decimal> MONTO_NET { get; set; }
        public string ESTATUS { get; set; }
        public Nullable<decimal> PORC { get; set; }
        public Nullable<decimal> DOC_REF { get; set; }
        public Nullable<System.DateTime> FECHAV { get; set; }
    
        public virtual DOCUMENTO DOCUMENTO { get; set; }
    }
}
