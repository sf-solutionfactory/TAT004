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
    
    public partial class TREVERSA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TREVERSA()
        {
            this.DOCUMENTORs = new HashSet<DOCUMENTOR>();
            this.TREVERSATs = new HashSet<TREVERSAT>();
        }
    
        public int ID { get; set; }
        public string DESCRIPCION { get; set; }
        public string USUARIOC_ID { get; set; }
        public Nullable<System.DateTime> FECHAC { get; set; }
        public Nullable<bool> ACTIVO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DOCUMENTOR> DOCUMENTORs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TREVERSAT> TREVERSATs { get; set; }
    }
}
