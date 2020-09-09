using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPC_Coopenae.UI.Models.ObjsReporte
{
    public class RTProducto_IDP
    {
        public int Id { get; set; }
        public List<string> TipoProductos { get; set; }
        public int CantVendida { get; set; }
        public int CantMeta { get; set; }
        public decimal IDPGanado { get; set; }
        public decimal IDPMeta { get; set; }
    }
}