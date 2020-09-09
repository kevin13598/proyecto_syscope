using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DATA
{
    public class TipoCDP
    {
        [Key]
        public int IdTipoCDP { get; set; }

        public string Nombre { get; set; }

        public string Moneda { get; set; }

        public decimal ComisionMaxima { get; set; }

        public bool Estado { get; set; }

        public decimal IDPNecesario { get; set; }

        public decimal PCTComision { get; set; }

    }

}
