using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPC_Coopenae.DATA.ObjReportes
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
