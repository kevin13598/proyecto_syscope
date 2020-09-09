using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DATA
{
    public class MetaTipoProducto
    {
        [Key]
        public int IdMetaTipoProducto { get; set; }

        public int MetaCantidad { get; set; }

        public decimal ValorIDP { get; set; }

        public int Meta { get; set; }

    }
}
