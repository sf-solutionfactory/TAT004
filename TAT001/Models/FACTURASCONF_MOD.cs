using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAT001.Models
{
    public class FACTURASCONF_MOD
    {
        public decimal? NUM_DOC { get; set; }
        public bool? POS { get; set; }
        public string SOCIEDAD_ID { get; set; }
        public string PAIS_ID { get; set; }
        public string TSOL { get; set; }
        public bool FACTURA { get; set; }
        public bool FECHA { get; set; }
        public bool PROVEEDOR { get; set; }
        public bool? PROVEEDOR_TXT { get; set; }
        public bool CONTROL { get; set; }
        public bool AUTORIZACION { get; set; }
        public bool VENCIMIENTO { get; set; }
        public bool FACTURAK { get; set; }
        public bool EJERCICIOK { get; set; }
        public bool BILL_DOC { get; set; }
        public bool BELNR { get; set; }
    }
}