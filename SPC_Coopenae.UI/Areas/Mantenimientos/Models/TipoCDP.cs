using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPC_Coopenae.UI.Areas.Mantenimientos.Models
{
    public class TipoCDP
    {
        public int IdTipoCDP { get; set; }

        public string Nombre { get; set; }

        public string Moneda { get; set; }

        [Display(Name = "Comisión Máxima (Colones)")]
        public decimal ComisionMaxima { get; set; }

        public bool Estado { get; set; } = true;

        [Display(Name = "IDP Necesario para comisión")]
        public decimal IDPNecesario { get; set; }

        [Display(Name = "Porcentaje de comisión sobre CDP")]
        public decimal PCTComision { get; set; }

    }
}