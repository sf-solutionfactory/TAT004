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
    
    public partial class MONEDA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MONEDA()
        {
            this.SOCIEDADs = new HashSet<SOCIEDAD>();
        }
    
        public string WAERS { get; set; }
        public string ISOCD { get; set; }
        public string ALTWR { get; set; }
        public string LTEXT { get; set; }
        public string KTEXT { get; set; }
        public bool ACTIVO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SOCIEDAD> SOCIEDADs { get; set; }
    }
}
