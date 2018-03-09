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
    
    public partial class DOCUMENTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DOCUMENTO()
        {
            this.DOCUMENTOPs = new HashSet<DOCUMENTOP>();
        }
    
        public decimal NUM_DOC { get; set; }
        public string TSOL_ID { get; set; }
        public string TALL_ID { get; set; }
        public string SOCIEDAD_ID { get; set; }
        public string PAIS_ID { get; set; }
        public string PERIODO { get; set; }
        public string EJERCICIO { get; set; }
        public string TIPO_TECNICO { get; set; }
        public string TIPO_RECURRENTE { get; set; }
        public Nullable<decimal> CANTIDAD_EV { get; set; }
        public string USUARIOC_ID { get; set; }
        public Nullable<System.DateTime> FECHAD { get; set; }
        public Nullable<System.DateTime> FECHAC { get; set; }
        public string ESTATUS { get; set; }
        public string ESTATUS_C { get; set; }
        public string ESTATUS_SAP { get; set; }
        public string ESTATUS_WF { get; set; }
        public Nullable<decimal> DOCUMENTO_REF { get; set; }
        public string NOTAS { get; set; }
        public Nullable<decimal> MONTO_DOC_MD { get; set; }
        public Nullable<decimal> MONTO_FIJO_MD { get; set; }
        public Nullable<decimal> MONTO_BASE_GS_PCT_MD { get; set; }
        public Nullable<decimal> MONTO_BASE_NS_PCT_MD { get; set; }
        public Nullable<decimal> MONTO_DOC_ML { get; set; }
        public Nullable<decimal> MONTO_FIJO_ML { get; set; }
        public Nullable<decimal> MONTO_BASE_GS_PCT_ML { get; set; }
        public Nullable<decimal> MONTO_BASE_NS_PCT_ML { get; set; }
        public Nullable<decimal> MONTO_DOC_ML2 { get; set; }
        public Nullable<decimal> MONTO_FIJO_ML2 { get; set; }
        public Nullable<decimal> MONTO_BASE_GS_PCT_ML2 { get; set; }
        public Nullable<decimal> MONTO_BASE_NS_PCT_ML2 { get; set; }
        public string IMPUESTO { get; set; }
        public Nullable<System.DateTime> FECHAI_VIG { get; set; }
        public Nullable<System.DateTime> FECHAF_VIG { get; set; }
        public string ESTATUS_EXT { get; set; }
        public string SOLD_TO_ID { get; set; }
        public string PAYER_ID { get; set; }
        public string GRUPO_CTE_ID { get; set; }
        public string CANAL_ID { get; set; }
        public string MONEDA_ID { get; set; }
        public Nullable<decimal> TIPO_CAMBIO { get; set; }
        public string NO_FACTURA { get; set; }
        public Nullable<System.DateTime> FECHAD_SOPORTE { get; set; }
        public string METODO_PAGO { get; set; }
        public string NO_PROVEEDOR { get; set; }
        public Nullable<int> PASO_ACTUAL { get; set; }
        public string AGENTE_ACTUAL { get; set; }
        public Nullable<System.DateTime> FECHA_PASO_ACTUAL { get; set; }
    
        public virtual TALL TALL { get; set; }
        public virtual TSOL TSOL { get; set; }
        public virtual USUARIO USUARIO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DOCUMENTOP> DOCUMENTOPs { get; set; }
    }
}
