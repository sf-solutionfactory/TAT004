using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAT001.Models
{
    public class CategoriaMaterial
    {
        public string ID { get; set; }
        public string DESCRIPCION { get; set; }
        public List<DOCUMENTOM_MOD> MATERIALES { get; set; }

    }
}