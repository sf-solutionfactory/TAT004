
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
    
public partial class GALL
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public GALL()
    {

        this.GALLTs = new HashSet<GALLT>();

        this.TALLs = new HashSet<TALL>();

        this.CUENTAs = new HashSet<CUENTA>();

    }


    public string ID { get; set; }

    public string DESCRIPCION { get; set; }

    public Nullable<bool> ACTIVO { get; set; }

    public string GRUPO_ALL { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<GALLT> GALLTs { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<TALL> TALLs { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<CUENTA> CUENTAs { get; set; }

}

}
