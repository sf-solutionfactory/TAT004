
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
    
public partial class MIEMBRO
{

    public string USUARIO_ID { get; set; }

    public int ROL_ID { get; set; }

    public Nullable<bool> ACTIVO { get; set; }



    public virtual ROL ROL { get; set; }

    public virtual USUARIO USUARIO { get; set; }

}

}
