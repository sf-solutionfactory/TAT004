
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
    
public partial class CATEGORIA
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public CATEGORIA()
    {

        this.CATEGORIATs = new HashSet<CATEGORIAT>();

        this.MATERIALs = new HashSet<MATERIAL>();

    }


    public string ID { get; set; }

    public string DESCRIPCION { get; set; }

    public Nullable<bool> ACTIVO { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<CATEGORIAT> CATEGORIATs { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<MATERIAL> MATERIALs { get; set; }

}

}
