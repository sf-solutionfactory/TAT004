using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TAT001.Models
{
    public class CartaV
    {
        [Key, Column(Order = 0)]
        public decimal num_doc { get; set; }
        [Key, Column(Order = 1)]
        public int pos { get; set; }
        public string company { get; set; }
        public bool company_x { get; set; }

        public string taxid { get; set; }
        public bool taxid_x { get; set; }

        public string concepto { get; set; }
        public bool concepto_x { get; set; }

        public string cliente { get; set; }
        public bool cliente_x { get; set; }

        public string puesto { get; set; }
        public bool puesto_x { get; set; }

        public string direccion { get; set; }
        public bool direccion_x { get; set; }

        public string folio { get; set; }
        public bool folio_x { get; set; }
        public string lugarFech { get; set; }
        public bool lugarFech_x { get; set; }
        public string lugar { get; set; }
        public bool lugar_x { get; set; }
        public string payerNom { get; set; }
        public bool payerNom_x { get; set; }
        public string payerId { get; set; }
        public bool payerId_x { get; set; }
        public string estimado { get; set; }
        public bool estimado_x { get; set; }

        public string mecanica { get; set; }
        public bool mecanica_x { get; set; }
        public string monto { get; set; }
        public bool monto_x { get; set; }

        public string nombreE { get; set; }
        public bool nombreE_x { get; set; }

        public string puestoE { get; set; }
        public bool puestoE_x { get; set; }

        public string companyC { get; set; }
        public bool companyC_x { get; set; }

        public string nombreC { get; set; }
        public bool nombreC_x { get; set; }

        public string puestoC { get; set; }
        public bool puestoC_x { get; set; }

        public string companyCC { get; set; }
        public bool companyCC_x { get; set; }

        public string legal { get; set; }
        public bool legal_x { get; set; }

        public string mail { get; set; }
        public bool mail_x { get; set; }

        public string comentarios { get; set; }
        public bool comentarios_x { get; set; }

        public string compromisoK { get; set; }
        public bool compromisoK_x { get; set; }

        public string compromisoC { get; set; }
        public bool compromisoC_x { get; set; }
        public string moneda { get; set; }
        //ENCABEZADO
        public string material { get; set; }
        public string categoria { get; set; }
        public string descripcion { get; set; }
        public string item { get; set; }
        public bool item_x { get; set; }
        public string costoun { get; set; }
        public bool costoun_x { get; set; }
        public string apoyo { get; set; }
        public bool apoyo_x { get; set; }
        public string apoyop { get; set; }
        public bool apoyop_x { get; set; }
        public string costoap { get; set; }
        public bool costoap_x { get; set; }
        public string precio { get; set; }
        public bool precio_x { get; set; }
        public string apoyoEst { get; set; }
        public bool apoyoEst_x { get; set; }
        public string apoyoRea { get; set; }
        public bool apoyoRea_x { get; set; }

        //TABLA DE MATERIALES O CATEGORÍAS
        [Key, Column(Order = 2)]

        //ARMADO DE LA CABECERA DE CADA TABLA INDIVIDUAL
        public List<string> listaFechas { get; set; }
        public List<string> listaEncabezado { get; set; }
        //ARMADO DEL CUERPO CADA TABLA INDIVIDUAL
        public List<int> numfilasTabla { get; set; }
        public List<string> listaCuerpo { get; set; }
    }
}