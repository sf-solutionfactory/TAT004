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
    
    public partial class DOCUMENTOF
    {
        public decimal NUM_DOC { get; set; }
        public int POS { get; set; }
        public string FACTURA { get; set; }
        public Nullable<System.DateTime> FECHA { get; set; }
        public string PROVEEDOR { get; set; }
        public string CONTROL { get; set; }
        public string AUTORIZACION { get; set; }
        public Nullable<System.DateTime> VENCIMIENTO { get; set; }
        public string FACTURAK { get; set; }
        public string EJERCICIOK { get; set; }
        public string BILL_DOC { get; set; }
        public string BELNR { get; set; }
    
        public virtual DOCUMENTO DOCUMENTO { get; set; }
    }
}
