using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPC_Coopenae.UI.Areas.Mantenimientos.Models
{
    public class TipoCredito
    {
        public int IdTipoCredito { get; set; }

        public string Nombre { get; set; }

        [Display(Name = "Comisión Distinta")]
        public decimal? ComisionDistinta { get; set; }

        [Display(Name = "Comisión Máxima")]
        public decimal? MaximoComision { get; set; }

        [Display(Name = "¿Cuenta para IDP?")]
        public bool SumaIDP { get; set; }

        public bool Estado { get; set; } = true;

    }
}