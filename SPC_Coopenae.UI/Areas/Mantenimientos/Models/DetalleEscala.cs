using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPC_Coopenae.UI.Areas.Mantenimientos.Models
{
    public class DetalleEscala
    {
        public int IdDetalleEscala { get; set; }

        public int Escala { get; set; }

        public int PCTMinimo { get; set; }

        public int PCTMaximo { get; set; }

        public decimal PCTComision { get; set; }
    }
}