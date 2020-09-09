using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DATA.ObjReportes
{
    public class RTProducto_IDP
    {
        public int Id { get; set; }
        public List<string> TipoProductos { get; set; }
        public int CantVendida { get; set; }
        public int CantMeta { get; set; }
        public decimal IDPGanado { get; set; }
        public decimal IDPMeta { get; set; }

        public RTProducto_IDP()
        {
            TipoProductos = new List<string>();
        }

    }
}
