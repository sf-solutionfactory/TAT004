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
        public Nullable<decimal> IMPORTE_FAC { get; set; }
        public string PAYER { get; set; }
        public string DESCRIPCION { get; set; }
        public string SOCIEDAD { get; set; }

        public virtual DOCUMENTO DOCUMENTO { get; set; }
    }
}
