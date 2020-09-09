using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPC_Coopenae.UI.Models.ObjsReporte
{
    public class R_IDP
    {
        public decimal[] IDP_Creditos { get; set; }
        public decimal[] IDP_CDPs { get; set; }
        public decimal[] Metas_Creditos { get; set; }
        public decimal[] Metas_CDPs { get; set; }
        public List<RTProducto_IDP> TipoProductos { get; set; }
        public decimal TotalIDP { get; set; }

    }
}