using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DATA
{
    public class DetalleEscala
    {
        [Key]
        public int IdDetalleEscala { get; set; }

        public int Escala { get; set; }

        public int PCTMinimo { get; set; }

        public int PCTMaximo { get; set; }

        public decimal PCTComision { get; set; }

    }
}
