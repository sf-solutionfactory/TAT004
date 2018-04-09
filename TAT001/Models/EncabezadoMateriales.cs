using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TAT001.Models
{
    public class EncabezadoMateriales
    {
        [Key, Column(Order = 0)]
        public decimal num_doc { get; set; }
        [Key, Column(Order = 1)]
        public int pos { get; set; }
        public string item { get; set; }
        public string material { get; set; }
        public string categoria { get; set; }
        public string descripcion { get; set; }
        public string costoun { get; set; }
        public string apoyo { get; set; }
        public string apoyop { get; set; }
        public string costoap { get; set; }
        public string precio { get; set; }
    }
}