using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPC_Coopenae.UI.Models.ObjsReporte
{
    public class RCDPs
    {
        public string NombreTipo { get; set; }
        public string Moneda { get; set; }
        public decimal? MaxComision { get; set; }
        public decimal PCTComision { get; set; }
        public decimal? SumaColocaciones { get; set; }
        public decimal IDPNecesario { get; set; }
        public decimal TotalComision { get; set; }

    }
}