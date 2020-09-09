using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPC_Coopenae.UI.Models.ObjsReporte
{
    public class RTipoCreditos
    {
        public string NombreTipo { get; set; }
        public decimal? MaxComision { get; set; }
        public decimal? SumaColocaciones { get; set; }
        public decimal? PCTComisionGanada { get; set; }
        public decimal TotalComision { get; set; }
    }
}