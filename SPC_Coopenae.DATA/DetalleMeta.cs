using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DATA
{
    public class DetalleMeta
    {
        //Meta
        public string Descripcion { get; set; }
        public int Escala { get; set; }
        public int Salario { get; set; }

        //Credito
        public decimal ColocacionCredito { get; set; }
        public decimal IDP_Credito { get; set; }

        //CDP
        public decimal ColocacionCDP { get; set; }
        public decimal IDP_CDP { get; set; }

        public List<DetalleMetaTipoProducto> TipoProductos { get; set; }

    }

    public class DetalleMetaTipoProducto
    {
        public int Id_MTP { get; set; }
        public int Cantidad { get; set; }
        public decimal IDP_TProductos { get; set; }
        public List<string> ListaTipoProductos { get; set; }

    }

}
