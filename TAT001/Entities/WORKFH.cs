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
    
    public partial class WORKFH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WORKFH()
        {
            this.WORKFVs = new HashSet<WORKFV>();
        }
    
        public string ID { get; set; }
        public string DESCRIPCION { get; set; }
        public string TSOL_ID { get; set; }
        public string ESTATUS { get; set; }
        public string USUARIO_ID { get; set; }
        public Nullable<System.DateTime> FECHAC { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WORKFV> WORKFVs { get; set; }
    }
}
