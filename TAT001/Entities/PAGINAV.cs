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
    
    public partial class PAGINAV
    {
        public string ID { get; set; }
        public int PAGINA_ID { get; set; }
        public string PAGINA_URL { get; set; }
        public string TXT50 { get; set; }
        public int CARPETA_ID { get; set; }
        public string CARPETA_URL { get; set; }
        public string ICON { get; set; }
        public Nullable<bool> PERMISO { get; set; }
        public string SPRAS_ID { get; set; }
        public string USUARIO_SPRAS { get; set; }
        public Nullable<int> Mantenimiento { get; set; }
        public string ManttoText { get; set; }
        public Nullable<int> Grupo { get; set; }
        public string GrupoText { get; set; }
        public Nullable<int> Orden { get; set; }
    }
}
