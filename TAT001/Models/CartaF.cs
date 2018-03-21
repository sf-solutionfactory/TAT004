using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAT001.Models
{
    public class CartaF
    {
        public decimal num_doc { get; set; }

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

        public string lugar { get; set; }
        public bool lugar_x { get; set; }

        public string payer { get; set; }
        public bool payer_x { get; set; }

        public string estimado { get; set; }
        public bool estimado_x { get; set; }

        public string mecanica { get; set; }
        public bool mecanica_x { get; set; }

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

    }
}