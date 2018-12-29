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
    
    public partial class CLIENTE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLIENTE()
        {
            this.CLIENTEFs = new HashSet<CLIENTEF>();
            this.CLIENTEIs = new HashSet<CLIENTEI>();
            this.CONTACTOCs = new HashSet<CONTACTOC>();
            this.DET_AGENTEC = new HashSet<DET_AGENTEC>();
            this.DOCUMENTOes = new HashSet<DOCUMENTO>();
            this.TAXEOHs = new HashSet<TAXEOH>();
            this.USUARIOFs = new HashSet<USUARIOF>();
        }
    
        public string VKORG { get; set; }
        public string VTWEG { get; set; }
        public string SPART { get; set; }
        public string KUNNR { get; set; }
        public string NAME1 { get; set; }
        public string STCD1 { get; set; }
        public string STCD2 { get; set; }
        public string LAND { get; set; }
        public string REGION { get; set; }
        public string SUBREGION { get; set; }
        public string REGIO { get; set; }
        public string ORT01 { get; set; }
        public string STRAS_GP { get; set; }
        public string PSTLZ { get; set; }
        public string CONTAC { get; set; }
        public string CONT_EMAIL { get; set; }
        public string PARVW { get; set; }
        public string PAYER { get; set; }
        public string GRUPO { get; set; }
        public string SPRAS { get; set; }
        public bool ACTIVO { get; set; }
        public string BDESCRIPCION { get; set; }
        public string BANNER { get; set; }
        public string CANAL { get; set; }
        public string BZIRK { get; set; }
        public string KONDA { get; set; }
        public string VKGRP { get; set; }
        public string VKBUR { get; set; }
        public string BANNERG { get; set; }
        public string PROVEEDOR_ID { get; set; }
        public string USUARIO_ID { get; set; }
        public string EXPORTACION { get; set; }
    
        public virtual PAI PAI { get; set; }
        public virtual PROVEEDOR PROVEEDOR { get; set; }
        public virtual TCLIENTE TCLIENTE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLIENTEF> CLIENTEFs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLIENTEI> CLIENTEIs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTACTOC> CONTACTOCs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DET_AGENTEC> DET_AGENTEC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DOCUMENTO> DOCUMENTOes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAXEOH> TAXEOHs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USUARIOF> USUARIOFs { get; set; }
    }
}
