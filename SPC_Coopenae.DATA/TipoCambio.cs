using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DATA
{
    public class TipoCambio
    {
        [Key]
        public int IdTipoCambio { get; set; }

        public decimal Valor { get; set; }

        public DateTime Fecha { get; set; }

        public bool Estado { get; set; }

    }

}
