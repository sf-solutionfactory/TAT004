using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAT001.Models
{
    public class Documento
    {
        public decimal NUM_DOC { get; set; }
        public string SOCIEDAD_ID { get; set; }
        public string PAIS_ID { get; set; }
        public DateTime FECHAD { get; set; }
        public TimeSpan HORAC { get; set; }
        public int MyProperty { get; set; }
        public string PERIODO { get; set; }
        public string ESTATUS { get; set; }
        public string PAYER_ID { get; set; }
        public string CLIENTE { get; set; }
        public string CANAL { get; set; }
        public string TSOL { get; set; }
        public string TALL { get; set; }
        public string CUENTAS { get; set; }
        public string CONCEPTO { get; set; }
        public string MONTO_DOC_ML { get; set; }
        public string FACTURA { get; set; }
        public string FACTURAK { get; set; }
        public string USUARIOC_ID { get; set; }
        public string USUARIOM_ID { get; set; }
        public string NC { get; set; }
        public string NUM_PRO { get; set; }
        public string NUM_NC { get; set; }
        public string NUM_AP { get; set; }
        public string NUM_REV { get; set; }
        public string NUM_TIPO { get; set; }
        public string NUM_PAYER { get; set; }
        public string NUM_CLIENTE { get; set; }
        public string NUM_IMPORTE { get; set; }
        public string NUM_GASTO { get; set; }
    }
}