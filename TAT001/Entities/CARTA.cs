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
    
    public partial class CARTA
    {
        public decimal NUM_DOC { get; set; }
        public int POS { get; set; }
        public Nullable<System.DateTime> FECHAC { get; set; }
        public string USUARIO_ID { get; set; }
        public string TIPO { get; set; }
        public string COMPANY { get; set; }
        public Nullable<bool> COMPANYX { get; set; }
        public string TAXID { get; set; }
        public Nullable<bool> TAXIDX { get; set; }
        public string CONCEPTO { get; set; }
        public Nullable<bool> CONCEPTOX { get; set; }
        public string CLIENTE { get; set; }
        public Nullable<bool> CLIENTEX { get; set; }
        public string PUESTO { get; set; }
        public Nullable<bool> PUESTOX { get; set; }
        public string DIRECCION { get; set; }
        public Nullable<bool> DIRECCIONX { get; set; }
        public string FOLIO { get; set; }
        public Nullable<bool> FOLIOX { get; set; }
        public string LUGAR { get; set; }
        public Nullable<bool> LUGARX { get; set; }
        public string PAYER { get; set; }
        public Nullable<bool> PAYERX { get; set; }
        public string ESTIMADO { get; set; }
        public Nullable<bool> ESTIMADOX { get; set; }
        public string MECANICA { get; set; }
        public Nullable<bool> MECANICAX { get; set; }
        public string NOMBREE { get; set; }
        public Nullable<bool> NOMBREEX { get; set; }
        public string PUESTOE { get; set; }
        public Nullable<bool> PUESTOEX { get; set; }
        public string COMPANYC { get; set; }
        public Nullable<bool> COMPANYCX { get; set; }
        public string NOMBREC { get; set; }
        public Nullable<bool> NOMBRECX { get; set; }
        public string PUESTOC { get; set; }
        public Nullable<bool> PUESTOCX { get; set; }
        public string COMPANYCC { get; set; }
        public Nullable<bool> COMPANYCCX { get; set; }
        public string LEGAL { get; set; }
        public Nullable<bool> LEGALX { get; set; }
        public string MAIL { get; set; }
        public Nullable<bool> MAILX { get; set; }
        public string LUGARFECH { get; set; }
        public Nullable<bool> LUGARFECHX { get; set; }
        public string MONTO { get; set; }
        public string MONEDA { get; set; }
    
        public virtual DOCUMENTO DOCUMENTO { get; set; }
        public virtual USUARIO USUARIO { get; set; }
    }
}
