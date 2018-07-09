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
    
    public partial class TSOL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TSOL()
        {
            this.CONSOPORTEs = new HashSet<CONSOPORTE>();
            this.DOCUMENTOes = new HashSet<DOCUMENTO>();
            this.TSOL1 = new HashSet<TSOL>();
            this.TSOLTs = new HashSet<TSOLT>();
        }
    
        public string ID { get; set; }
        public string DESCRIPCION { get; set; }
        public string TSOLR { get; set; }
        public string RANGO_ID { get; set; }
        public string ESTATUS { get; set; }
        public bool FACTURA { get; set; }
        public bool PADRE { get; set; }
        public bool ADICIONA { get; set; }
        public string TSOLM { get; set; }
        public string TSOLC { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONSOPORTE> CONSOPORTEs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DOCUMENTO> DOCUMENTOes { get; set; }
        public virtual RANGO RANGO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TSOL> TSOL1 { get; set; }
        public virtual TSOL TSOL2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TSOLT> TSOLTs { get; set; }
    }
}
