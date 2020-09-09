using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DATA
{
    public class MetaTipoProductoDetalle
    {
        [Key]
        [Column(Order=1)]
        public int MetaTipoProducto { get; set; }

        [Key]
        [Column(Order = 2)]
        public int TipoProducto { get; set; }
    }
}
