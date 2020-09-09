using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPC_Coopenae.UI.Models.ObjsReporte
{
    public class RProductos
    {
        public string TipoProducto { get; set; }
        public decimal IDPNecesario { get; set; }
        public string Nombre { get; set; }
        public string Moneda { get; set; }
        public decimal ComisionProducto { get; set; }
        public int CantidadVendida { get; set; }
        public decimal TotalComision { get; set; }
    }
}