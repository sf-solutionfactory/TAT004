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
    
    public partial class PRESUPUESTOH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRESUPUESTOH()
        {
            this.PRESUPUESTOPs = new HashSet<PRESUPUESTOP>();
        }
    
        public int ID { get; set; }
        public string ANIO { get; set; }
        public string USUARIO_ID { get; set; }
        public Nullable<System.DateTime> FECHAC { get; set; }
    
        public virtual USUARIO USUARIO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESUPUESTOP> PRESUPUESTOPs { get; set; }
    }
}
